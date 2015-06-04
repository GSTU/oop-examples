using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Flappy_Bird
{
    public class WindowSettings
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public WindowSettings()
        {
            this.Width = 616;
            this.Height = 839;
        }
    }
    public class PlayerSettings
    {
        public Point StartPosition { get; private set; }
        public int Size { get; private set; }
        public double Speed { get; private set; }
        public double MaxVSpeed { get; private set; }
        public double JumpHeight { get; private set; }

        public PlayerSettings(int ps)
        {
            this.StartPosition = new Point(200, 300);
            this.Size = ps;
            this.Speed = 2.8;
            this.MaxVSpeed = 10;
            this.JumpHeight = -3.2;
        }
    }
    public class WallsSettings
    {
        public int MinHeight { get; private set; }
        public int Thickness { get; private set; }
        public int Step { get; private set; }
        public int Count { get; private set; }
        public int HoleSize { get; private set; }

        public WallsSettings(Settings settings, int hs)
        {
            this.MinHeight = 100;
            this.Thickness = 80;
            this.Step = this.Thickness * 2;
            this.Count = (settings.WindowSettings.Width / (this.Thickness + this.Step)) + 1;
            this.HoleSize = hs;
        }
    }
    public class Settings
    {
        public WindowSettings WindowSettings { get; private set; }
        public PlayerSettings PlayerSettings { get; private set; }
        public WallsSettings WallsSettings { get; private set; }

        public int GroundLevel { get; private set; }
        public double Gravity { get; private set; }

        public Settings(int gl, int hs, int ps)
        {
            this.WindowSettings = new WindowSettings();
            this.PlayerSettings = new PlayerSettings(ps);
            this.WallsSettings = new WallsSettings(this, hs);

            this.GroundLevel = gl;
            this.Gravity = 0.09;
        }
    }

    public class Player
    {
        public Grid Body { get; private set; }
        public Thickness Margin;
        public SolidColorBrush Brush { get; private set; }
        public double VSpeed;

        private Random Random;

        public Player(Settings settings, int seed)
        {
            this.Random = new Random(seed);

            this.Body = new Grid();
            this.Margin = new Thickness();
            this.Brush = new SolidColorBrush();
            this.VSpeed = 0;

            this.Body.Width = this.Body.Height = settings.PlayerSettings.Size;
            this.Body.Margin = this.Margin;
            this.Body.Background = this.Brush;
            this.Body.HorizontalAlignment = HorizontalAlignment.Left;
            this.Body.VerticalAlignment = VerticalAlignment.Top;

            Reset(settings);
        }

        public void Move(Settings settings)
        {
            this.VSpeed += settings.Gravity;
            this.Margin.Top += this.VSpeed;

            this.Body.Margin = this.Margin;

            if (this.VSpeed > settings.PlayerSettings.MaxVSpeed)
                this.VSpeed = settings.PlayerSettings.MaxVSpeed;
        }
        public void Reset(Settings settings)
        {
            this.VSpeed = 0;

            this.Margin.Left = settings.PlayerSettings.StartPosition.X;
            this.Margin.Top = settings.PlayerSettings.StartPosition.Y;
            this.Margin.Right = 0;
            this.Margin.Bottom = 0;

            this.Body.Margin = this.Margin;

            this.SetColor();
        }
        private void SetColor()
        {
            this.Brush.Color = Color.FromRgb((byte)Random.Next(0, 256), (byte)Random.Next(0, 256), (byte)Random.Next(0, 256));
        }
    }
    public class WallBody
    {
        public Grid UpperBody { get; private set; }
        public Grid LowerBody { get; private set; }
        public Thickness UpperMargin;
        public Thickness LowerMargin;
        public SolidColorBrush Brush { get; private set; }

        private int Number;
        private Random Random;

        public WallBody(Settings settings, int n, int seed)
        {
            this.Number = n;
            this.Random = new Random(seed);

            this.UpperBody = new Grid();
            this.LowerBody = new Grid();
            this.UpperMargin = new Thickness();
            this.LowerMargin = new Thickness();
            this.Brush = new SolidColorBrush();

            this.UpperBody.Width = this.LowerBody.Width = settings.WallsSettings.Thickness;
            this.UpperBody.Background = this.LowerBody.Background = this.Brush;
            this.UpperBody.HorizontalAlignment = this.LowerBody.HorizontalAlignment = HorizontalAlignment.Left;
            this.UpperBody.VerticalAlignment = VerticalAlignment.Stretch;
            this.LowerBody.VerticalAlignment = VerticalAlignment.Stretch;

            this.UpperMargin.Top = 0;
            this.LowerMargin.Bottom = settings.GroundLevel;
        }

        public void SetColor()
        {
            this.Brush.Color = Color.FromRgb((byte)Random.Next(0, 256), (byte)Random.Next(0, 256), (byte)Random.Next(0, 256));
        }
        public void SetPosition(Settings settings)
        {
            this.UpperMargin.Left = this.LowerMargin.Left = (settings.WindowSettings.Width - 16) + this.Number * (settings.WallsSettings.Thickness + settings.WallsSettings.Step);
            this.UpperMargin.Right = this.LowerMargin.Right = 0;
        }
        public void SetHole(Settings settings)
        {
            this.UpperMargin.Bottom = this.Random.Next(settings.WallsSettings.MinHeight + settings.GroundLevel + settings.WallsSettings.HoleSize, (settings.WindowSettings.Height - 39) - settings.WallsSettings.MinHeight + 1);
            this.LowerMargin.Top = (settings.WindowSettings.Height - 39) - this.UpperMargin.Bottom + settings.WallsSettings.HoleSize;
        }
        public bool IsVisible()
        {
            if (this.UpperBody.Margin.Left + this.UpperBody.Width < 0)
                return false;

            return true;
        }
        public bool Compare(Player player, Settings settings, bool passed)
        {
            if (!passed && (player.Body.Margin.Left + settings.PlayerSettings.Size >= this.UpperBody.Margin.Left))
            {
                if ((player.Body.Margin.Top <= (settings.WindowSettings.Height - 39) - this.UpperBody.Margin.Bottom)
                    ||
                    (player.Body.Margin.Top + settings.PlayerSettings.Size >= this.LowerBody.Margin.Top))
                {
                    return true;
                }
            }

            return false;
        }
    }
    public class Wall
    {
        public WallBody Body { get; private set; }
        public bool Passed;

        public Wall(Settings settings, int n, int seed)
        {
            this.Body = new WallBody(settings, n, seed);
            this.Passed = false;

            Reset(settings);
        }

        public void Move(Settings settings)
        {
            this.Body.UpperMargin.Left -= settings.PlayerSettings.Speed;
            this.Body.UpperMargin.Right -= settings.PlayerSettings.Speed;

            this.Body.LowerMargin.Left -= settings.PlayerSettings.Speed;
            this.Body.LowerMargin.Right -= settings.PlayerSettings.Speed;

            this.Body.UpperBody.Margin = this.Body.UpperMargin;
            this.Body.LowerBody.Margin = this.Body.LowerMargin;
        }
        public void ShiftToEnd(Settings settings)
        {
            if (!this.Body.IsVisible())
            {
                this.Passed = false;

                this.Body.UpperMargin.Left += settings.WallsSettings.Count * (settings.WallsSettings.Thickness + settings.WallsSettings.Step);
                this.Body.UpperMargin.Right += settings.WallsSettings.Count * (settings.WallsSettings.Thickness + settings.WallsSettings.Step);

                this.Body.LowerMargin.Left += settings.WallsSettings.Count * (settings.WallsSettings.Thickness + settings.WallsSettings.Step);
                this.Body.LowerMargin.Right += settings.WallsSettings.Count * (settings.WallsSettings.Thickness + settings.WallsSettings.Step);

                this.Body.UpperBody.Margin = this.Body.UpperMargin;
                this.Body.LowerBody.Margin = this.Body.LowerMargin;

                this.Body.SetHole(settings);
                this.Body.SetColor();
            }
        }
        public bool IsCollide(Player player, Settings settings)
        {
            return this.Body.Compare(player, settings, this.Passed);
        }
        public void Reset(Settings settings)
        {
            this.Passed = false;

            this.Body.SetHole(settings);
            this.Body.SetPosition(settings);
            this.Body.SetColor();

            this.Body.UpperBody.Margin = this.Body.UpperMargin;
            this.Body.LowerBody.Margin = this.Body.LowerMargin;
        }
    }
}