using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace battle
{
    public class Game
    {
        private PictureBox _playerFotoBox;
        private PictureBox _aIFotoBox;
        private RichTextBox _log;
        //private int DAMAGE = 10;

        public Game(Player user, Player ai, RichTextBox log)
        {
            _playerFotoBox = user.FotoBox;
            _aIFotoBox = ai.FotoBox;
            _log = log;
        }

        public void NewGame()
        {
            _playerFotoBox.Image = Image.FromFile("resources/1.png");
            _playerFotoBox.SizeMode = PictureBoxSizeMode.Zoom;
            _aIFotoBox.Image = Image.FromFile("resources/2.png");
            _aIFotoBox.SizeMode = PictureBoxSizeMode.Zoom;
            _log.Text = "Начало новой игры\n"+_log.Text;
        }

        public PictureBox PlayerFotoBox
        {
            get { return _playerFotoBox; }
        }

        public PictureBox AIFotoBox
        {
            get { return _aIFotoBox; }
        }

        public RichTextBox Log
        {
            get { return _log; }
            set { _log = value; }
        }

        public void userStep()
        {
        }
        public void aiStep(int userProtectStat, int userAttackStat, Player user, Player ai)
        {
            int attackStat;
            int protectStat;
            Random rnd = new Random();
            protectStat = 1 + rnd.Next(3);
            attackStat = 1 + rnd.Next(3);
            string temp="";
            switch (protectStat)
            { 
                case 1:
                    temp= "Враг защищает голову\n";
                    break;
                case 2:
                    temp = "Враг защищает тело\n";
                    break;
                case 3:
                    temp = "Враг защищает ноги\n";
                    break;
            }
            _log.Text = temp + _log.Text;
            switch (attackStat)
            {
                case 1:
                    temp = "Враг атакует голову\n";
                    break;
                case 2:
                    temp = "Враг атакует тело\n";
                    break;
                case 3:
                    temp= "Враг атакует ноги\n";
                    break;
            }
            _log.Text = temp + _log.Text;
            temp = "";

            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"resources/sound/beat1.wav");
             
            if (attackStat != userProtectStat)
            {
                player.Play();
                

                user.Health = user.Health - ai.Attack;
                user.HealthLabel.Text = user.Health.ToString();
                temp = "Вы получаете " + ai.Attack.ToString() + " урона\n";
            }
            _log.Text = temp + _log.Text;
            temp = "";
            if (protectStat != userAttackStat)
            {
                Thread.Sleep(100);
                player.Play();
                ai.Health = ai.Health - user.Attack;
                ai.HealthLabel.Text = ai.Health.ToString();
                temp = "Враг получает " + user.Attack.ToString() + " урона\n";
            }
            _log.Text = temp + _log.Text;
            temp = "";
        }


    }
}
