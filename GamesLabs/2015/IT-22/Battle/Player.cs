using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace battle
{
    public class Player
    {
        private int _health;
        private PictureBox _fotoBox;
        private Label _healthLabel;
        private int _attack;

        public Player(PictureBox fotoBox, int health)
        {
            _fotoBox = fotoBox;
            _health = health;
            _healthLabel = new Label();
            _healthLabel.Text = _health.ToString();
        }

        public PictureBox FotoBox
        {
            get { return _fotoBox; }

        }
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }
        public int Attack
        {
            get { return _attack; }
            set { _attack = value; }
        }

        public Label HealthLabel
        {
            get { return _healthLabel; }
            set { _healthLabel = value; }
        }
    }
}
