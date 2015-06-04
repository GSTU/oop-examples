using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace Game {
    public partial class Игрушка:Form {
        Player player;
        public List<Square> ToDelete = new List<Square>();
        List<Square> GameObjects = new List<Square>();
        private int timer=10;
        private int score=0;
        public bool gameOver=false;
        public Игрушка() {
            InitializeComponent();
        }
        public void GameOver() {
            this.timer3.Enabled=false;
            MessageBox.Show("Конец игры. Ваш результат - "+score+"");
            this.NewGame();

        }
        int sizeSqr=50;
        Color _player = Color.Lime;
        Color _earth = Color.Blue;
        Color _backcolor = Color.DarkMagenta;
        public void NewGame() {

            timer3.Enabled = true;
            timer=10;
            score=0;
            GameObjects=new List<Square>();
            ToDelete=new List<Square>();
            player = new Player(sizeSqr, sizeSqr, 130, 0, _player, this);
            Square earth = new Obstacle(1604, 50, 0, 400, _earth, this);
            GameObjects.Add(player);
            GameObjects.Add(earth);
            GameObjects.Add(new Destructor(20, 450, -10, 0, Color.Red, this));
        }
        private void button2_Click(object sender, EventArgs e)//кнопка старт
        {
            /*pictureBox1.Image = Image.FromFile("1.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;*/
            pictureBox1.BackColor = _backcolor;
            button2.Enabled = false;
            button2.Visible = false;
            this.NewGame();

        }
        private void Игрушка_KeyDown(object sender, KeyEventArgs e)//нажатие на пробел
        {
            if((e.KeyCode == Keys.Space)||(e.KeyCode==Keys.Up)) {
                player.Jump=true;
            }
            if(e.KeyCode==Keys.Down) {
                player.Down=true;
            }
        }
        private void timer3_Tick_1(object sender, EventArgs e)//таймер препятствия которое идет справа налево меняя координату икс
        {
            Refresh();
            timer--;
            if(timer<=0) {
                timer=50;
                GameObjects.Add(new Enemy(30, 40, 915, 0, Color.Blue, this));
            }
            Graphics g = pictureBox1.CreateGraphics();
            for(int i=0; i<this.GameObjects.Count; i++) {
                GameObjects[i].Tick();
            }
            for(int i=0; i<this.GameObjects.Count; i++) {
                for(int j=i+1; j<this.GameObjects.Count; j++) {
                    if(this.GameObjects[i].Collide(this.GameObjects[j])) {
                        GameObjects[i].React(GameObjects[j]);
                        GameObjects[j].React(GameObjects[i]);
                    }
                }
            }
            for(int i=0; i<this.ToDelete.Count; i++) {
                GameObjects.Remove(this.ToDelete[i]);
                score++;
            }
            this.ToDelete.Clear();
            for(int i=0; i<this.GameObjects.Count; i++) {
                GameObjects[i].Draw(g);
            }
            if(gameOver) {
                gameOver=false;
                GameOver();
            }
        }
        static bool valid = true;
        public static void ValidationHandler(object sender, ValidationEventArgs args)
        {
            MessageBox.Show(args.Severity + ":" + args.Message + "\nЗагружены стандартные параметры");
            valid = false;
        }

        private void Игрушка_Load(object sender, EventArgs e)
        {
            string c1="", c2="", c3="";
            XmlTextReader reader = new XmlTextReader("XMLFile1.xml");
            try
            {
           
            XmlValidatingReader validreader = new XmlValidatingReader(reader);
            validreader.Schemas.Add(null, "XMLSchema1.xsd");
            validreader.ValidationType = ValidationType.Schema;
            validreader.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);

                while (validreader.Read()) ;
            }
            catch(Exception ex)
            {
                valid = false;
                MessageBox.Show(ex.Message);
                MessageBox.Show("Файл конфигурации не найден\nЗагружены параметры по умолчанию");
            }
            //validreader.Close();

            if (valid)
            {
                reader = new XmlTextReader("XMLFile1.xml");
                while (reader.Read())
                {
                    if (reader.Name == "sizeSqr" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        sizeSqr = reader.ReadElementContentAsInt();
                    }

                    if (reader.Name == "c1" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        c1 = reader.ReadElementContentAsString();
                        _backcolor = ColorTranslator.FromHtml(c1);
                    }

                    if (reader.Name == "c2" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        c2 = reader.ReadElementContentAsString();
                        _earth = ColorTranslator.FromHtml(c2);
                    }
                    if (reader.Name == "c3" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        c3 = reader.ReadElementContentAsString();
                        _player = ColorTranslator.FromHtml(c3);
                    }
                     
                }
                reader.Close();
            }
            
        }
    }
}
