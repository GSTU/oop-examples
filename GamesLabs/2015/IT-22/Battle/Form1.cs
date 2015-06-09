using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Xml;

namespace battle
{
    public partial class Form1 : Form
    {

        private bool flag = true;
        private int protectStat = 0;
        private int attackStat = 0;
        Game currentGame;
        Player user;
        Player ai;
        Config config;
        Config standart = new Config(new GameConfig(10, 100), new GameConfig(10, 100),Color.CadetBlue);
        public Form1()
        {
            InitializeComponent();
            

            try
            {
                
                if ((System.IO.File.Exists("scheme.xsd")))
                {
                    
                    XmlReaderSettings xmlreadersettings = new XmlReaderSettings();
                    //MessageBox.Show("1");
                    xmlreadersettings.Schemas.Add(null, "scheme.xsd");
                    //MessageBox.Show("2");
                    xmlreadersettings.ValidationType = ValidationType.Schema;
                    xmlreadersettings.ValidationEventHandler += new ValidationEventHandler(ValidateXML);
                    //MessageBox.Show("3");
                    XmlReader xmlreader = XmlReader.Create("xml.xml", xmlreadersettings);
                    while (xmlreader.Read())
                    {  }
                }
                else
                {
                    throw new System.IO.FileNotFoundException();
                    
                }


                if (flag)
                {
                    config = new XMLWithDOM("xml.xml").GetConfig();
                    //MessageBox.Show("h");
                }
                else
                {
                    
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                config = standart;
            }
        }

        public PictureBox PlayerFotoBox
        {
            get { return pictureBox1; }
        }
        public PictureBox AIFotoBox
        {
            get { return pictureBox2; }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            NewGame();
            
        }


        
        private void ValidateXML(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                MessageBox.Show("Warning: " + e.Message);

                flag = false;
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                MessageBox.Show("Error: " + e.Message);

                flag = false;
            }
        }

        private void NewGame()
        {
            user = new Player(pictureBox1, 100);
            ai = new Player(pictureBox2, 100);
            currentGame = new Game(user, ai, richTextBox1);
            currentGame.NewGame();
            //checkConfig();
            ai.Health = config.AiConfig.Health;
            ai.Attack = config.AiConfig.Attack;
            user.Health = config.PlayerConfig.Health;
            user.Attack = config.PlayerConfig.Attack;
            BackColor = config.Bgcolor;
            pictureBox1 = currentGame.PlayerFotoBox;
            pictureBox2 = currentGame.AIFotoBox;
            HealthEnemy.Text = ai.Health.ToString();
            HealthPlayer.Text = user.Health.ToString();
        }

       
       

        private void btnEnable(bool value)
        {
            ProtectHead.Enabled = value;
            ProtectBody.Enabled = value;
            ProtectBody.Enabled = value;
            AttackHead.Enabled = value;
            AttackBody.Enabled = value;
            AttackLegs.Enabled = value;
        }

        private void newGame_btn_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void ProtectHead_Click(object sender, EventArgs e)
        {
            protectStat = 1;
            currentGame.Log.Text = "Защита головы\n\n" + currentGame.Log.Text;
        }

        private void ProtectBody_Click(object sender, EventArgs e)
        {
            protectStat = 2;
            currentGame.Log.Text = "Защита тела\n\n" + currentGame.Log.Text;
        }

        private void ProtectLegs_Click(object sender, EventArgs e)
        {
            protectStat = 3;
            currentGame.Log.Text = "Защита ног\n\n" + currentGame.Log.Text;
        }

        private void AttackHead_Click(object sender, EventArgs e)
        {
            attackStat = 1;
            currentGame.Log.Text = "Атака головы\n" + currentGame.Log.Text;
        }

        private void AttackBody_Click(object sender, EventArgs e)
        {
            attackStat = 2;
            currentGame.Log.Text = "Атака тела\n" + currentGame.Log.Text;
        }

        private void AttackLegs_Click(object sender, EventArgs e)
        {
            attackStat = 3;
            currentGame.Log.Text = "Атака ног\n" + currentGame.Log.Text;
        }



        private void MakeStep_Click(object sender, EventArgs e)
        {

            if (protectStat == 0)
            {
                MessageBox.Show("Вы не выбрали, что защищать. Будет установлено значение " + ProtectHead.Text+".", "Внимание",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                protectStat = 1;
            }
            if (attackStat == 0)
            {
                MessageBox.Show("Вы не выбрали, что атаковать. Будет установлено значение " + AttackHead.Text+".", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                attackStat = 1;
            }
            btnEnable(false);
            
            currentGame.aiStep(protectStat, attackStat, user, ai);
            
            protectStat = 0;
            attackStat = 0;
            btnEnable(true);
            HealthPlayer.Text = user.HealthLabel.Text;
            HealthEnemy.Text = ai.HealthLabel.Text;
            if (user.Health <= 0)
            {
                MessageBox.Show("Ты проиграл");
                NewGame();
            }
            if (ai.Health <= 0)
            {
                MessageBox.Show("Ты выиграл!");
                NewGame();
            }

            
        }
        //public void checkConfig()
        //{
        //    if (config.AiConfig.Attack >= int.MaxValue || config.AiConfig.Health >= int.MaxValue || config.PlayerConfig.Attack >= int.MaxValue || config.PlayerConfig.Health>=int.MaxValue)
        //    {
        //        config=standart;
        //    }
        //    if (config.AiConfig.Attack <= 0 || config.AiConfig.Health <= 0 || config.PlayerConfig.Attack <= 0 || config.PlayerConfig.Health <= 0)
        //    {
        //        config = standart;
        //    }
        //}


    }


}
