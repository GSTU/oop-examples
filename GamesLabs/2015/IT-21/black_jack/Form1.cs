using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Schema;
namespace black_jack
{
    public partial class frm_BJ : Form
    {
        PictureBox [] pic_card = new PictureBox[10];
        int ind=0;
        Cards card = new Cards();
        Game game = new Game();
        DialogResult ans;
        int sash=105;
        bool flag = true;
        public static bool isValid;
        public static string font;
        public static int size;
        public static string img;
        public frm_BJ()
        {
            InitializeComponent();
            try
            {
                XmlReaderSettings XMLSettings = new XmlReaderSettings();
                XMLSettings.Schemas.Add(null, "XML.xsd");
                XMLSettings.ValidationType = ValidationType.Schema;
                XmlReader XMLReader = XmlReader.Create("XML.xml", XMLSettings);
                isValid = true;
                while (XMLReader.Read()) { }
                if (isValid == true)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("XML.xml");
                    size = Convert.ToInt32(doc["config"]["size"].InnerText);
                    font = doc["config"]["font"].InnerText;
                    img = doc["config"]["backgroundimg"].InnerText;
                    this.BackgroundImage = new Bitmap(img);
                    label1.Font = new Font(font, size);
                }
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Настройки не были установлены, загружены стандартные параметры ", "Blackjack", MessageBoxButtons.OK);
            }
            

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                int num;
                if (game._Name == null)
                    MessageBox.Show("Введите имя", "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    if (game._Bet == 0)
                        MessageBox.Show("Сделайте ставку", "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        if (flag == true)
                        {


                            num = card.Human();
                            pic_card[ind] = new PictureBox();
                            pic_card[ind].Image = Image.FromFile("img\\" + num.ToString() + ".jpg");
                            pic_card[ind].Top = pictureBox1.Top;
                            pic_card[ind].Left = pictureBox1.Left + sash;
                            pic_card[ind].Height = 150;
                            pic_card[ind].Width = 100;
                            pic_card[ind].SizeMode = PictureBoxSizeMode.StretchImage;
                            this.Controls.Add(pic_card[ind]);

                            game.Human(num);

                            lbl_HScore.Text = "Вы    " + game._HumanScore.ToString();

                            if (game._HumanScore == 21)
                            {
                                game._Money += game._Bet;
                                lbl_Money.Text = game._Money.ToString();
                                ans = MessageBox.Show("Вы победили \n Новая игра ?", "Blackjack", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                flag = false;
                            }
                            else if (game._HumanScore > 21)
                            {
                                game._Money -= game._Bet;
                                lbl_Money.Text = game._Money.ToString();
                                ans = MessageBox.Show("Вы проиграли \n Новая игра ?", "Blackjack", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                flag = false;
                            }

                            sash += 105;
                            ind++;
                        }

                if (ans == DialogResult.Yes)
                {
                    ans = DialogResult.Cancel;
                    NewGame();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
       }

        private void cmb_Stand_Click(object sender, EventArgs e)
        {
            try
            {
                int win;
                sash = 105;

                if (game._Name == null)
                    MessageBox.Show("Введите имя", "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    if (game._Bet == 0)
                        MessageBox.Show("Сделайте ставку", "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        if (flag == true)
                        {
                            int num;
                            do
                            {
                                num = card.Comp();
                                pic_card[ind] = new PictureBox();
                                pic_card[ind].Image = Image.FromFile("img\\" + num.ToString() + ".jpg");
                                pic_card[ind].Top = pictureBox1.Top + 200;
                                pic_card[ind].Left = pictureBox1.Left + sash;
                                pic_card[ind].Height = 150;
                                pic_card[ind].Width = 100;
                                pic_card[ind].SizeMode = PictureBoxSizeMode.StretchImage;
                                this.Controls.Add(pic_card[ind]);

                                game.Comp(num);
                                lbl_CScore.Text = "Бот  " + game._CompScore.ToString();
                                sash += 105;
                                ind++;
                            } while (game.AI() == true);

                            win = game.Winner();
                            if (game._Money < 0)
                            {
                                MessageBox.Show("Вы продули", "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                            lbl_Money.Text = game._Money.ToString();

                            if (win == 1)
                                ans = MessageBox.Show("Вы победили\n Новая игра ?", "Blackjack", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            else if (win == 2)
                                ans = MessageBox.Show("Ничья\n Новая игра?", "Blackjack", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            else
                                ans = MessageBox.Show("Вы продули\n Новая игра ?", "Blackjack", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        }
                if (ans == DialogResult.Yes)
                {
                    ans = DialogResult.Cancel;
                    NewGame();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void NewGame()
        {
            Cards card = new Cards();
            for (ind--; ind >= 0; ind--)
                this.Controls.Remove(pic_card[ind]);

            sash = 105;
            game._CompScore = 0;
            game._HumanScore = 0;
            flag = true;
            ind = 0;
            game._Bet = 0;
            lbl_Bet.Text = "0";
            lbl_CScore.Text = "Бот";
            lbl_HScore.Text = "Вы";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void cmb_ok_name_Click(object sender, EventArgs e)
        {
            try
            {
                game._Name = txt_name.Text;
                game._Money = 200;
                this.Controls.Remove(label1);
                this.Controls.Remove(txt_name);
                this.Controls.Remove(cmb_ok_name);
                this.Text += " " + game._Name;
                lbl_Money.Text = game._Money.ToString();
                lbl_Bet.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmb_BetPlus_Click(object sender, EventArgs e)
        {
            try
            {
                game._Bet += 10;
                if (game._Bet > game._Money)
                {
                    MessageBox.Show("Недостаточно средств", "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    game._Bet = 0;
                }

                lbl_Bet.Text = game._Bet.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmb_BetMinus_Click(object sender, EventArgs e)
        {
            try
            {
                if (game._Bet == 0)
                {
                    MessageBox.Show("Ставка ... должна быть >0", "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    game._Bet = 0;
                }
                game._Bet -= 10;

                lbl_Bet.Text = game._Bet.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmb_BetMax_Click(object sender, EventArgs e)
        {
            try
            {
                game._Bet = 50;
                if (game._Bet > game._Money)
                {
                    MessageBox.Show("Недостаточно денег", "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    game._Bet = 0;
                }
                lbl_Bet.Text = game._Bet.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Blackjack", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }
        
    }
}