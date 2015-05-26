using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;


namespace Реверси
{
    public partial class Form1 : Form
    {
        // Параметры границ //
        private int LeftSide;
        private int RightSide;
        private int TopSide;
        private int BottomSide;
        private static bool isValid;

        private int CurrentStepColor;       // цвет фишки на текущем ходу
        private int CurrentNumberOfSteps;   // текущее количество шагов

        private int Points_U1 = 0,          // очки первого ирока
                    Points_U2 = 0;          // очки второго игрока

        private int[,] mas;                 // матрица состояний
        
        Bitmap ClearBgImage,                // изображение для пустого фона
               WhiteCRImage,                // изображение для белой фишки
               BlackCRImage;                // изображение для чёрной фишки

        // ###############################################################//
        /* ---------------- Проверки по всем направлениям --------------- */
        // ###############################################################//
        /// <summary>
        /// Проверка горизонтали справа
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool CheckHoriz_Rigth(int i,int j)
        {
            j++;
            int count = 0;

            if (j < RightSide)
            {
                while (j < RightSide && mas[i, j] != -1 && mas[i, j] != CurrentStepColor)
                {
                    j++;
                    count++;
                }
                if (j < RightSide && mas[i, j] == CurrentStepColor && count >0) return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка горизонтали слева
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool CheckHoriz_Left(int i, int j)
        {
            j--;
            int count = 0;
            if (j > LeftSide)
            {
                while (j > LeftSide && mas[i, j] != -1 && mas[i, j] != CurrentStepColor)
                {
                    j--;
                    count++;
                }
                if (j > LeftSide && mas[i, j] == CurrentStepColor && count >0) return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка побочной диагонали сверху
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool CheckDiag_RightTop(int i, int j)
        {
            j++;
            i--;
            int count = 0;
            if (j < RightSide && i>BottomSide)
            {
                while (j < RightSide && mas[i, j] != -1 && mas[i, j] != CurrentStepColor && i>BottomSide)
                {
                    j++;
                    i--;
                    count++;
                }
                if (j < RightSide && mas[i, j] == CurrentStepColor && count > 0) return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка побочной диагонали снизу
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool CheckDiag_RightBot(int i, int j)
        {
            j--;
            i++;
            int count = 0;
            if (j > LeftSide && i<TopSide)
            {
                while (j > LeftSide && i < TopSide && mas[i, j] != -1 && mas[i, j] != CurrentStepColor  )
                {
                    j--;
                    i++;
                    count++;
                }
                if (j > LeftSide && i < TopSide && mas[i, j] == CurrentStepColor && count > 0) return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка вертикали сверху
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool CheckVert_Top(int i, int j)
        {
            i--;
            int count = 0;
            if (i > BottomSide)
            {
                while (i > BottomSide && mas[i, j] != -1 && mas[i, j] != CurrentStepColor)
                {
                    i--;
                    count++;
                }

                if (i > BottomSide && mas[i, j] == CurrentStepColor && count>0) return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка вертикали снизу
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool CheckVert_Bottom(int i, int j)
        {
            i++;
            int count = 0;
            if (i <TopSide)
            {
                while (i < TopSide && mas[i, j] != -1 && mas[i, j] != CurrentStepColor)
                {
                    i++;
                    count++;
                }
                if (i < TopSide && mas[i, j] == CurrentStepColor && count > 0) return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка главной диагонали сверху
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool CheckDiag_LeftTop(int i, int j)
        {
            j--;
            i--;
            int count = 0;
            if (j > LeftSide && i > BottomSide)
            {
                while (j > LeftSide && i > BottomSide && mas[i, j] != -1 && mas[i, j] != CurrentStepColor)
                {
                    j--;
                    i--;
                    count++;
                }
                if (j > LeftSide && i > BottomSide && mas[i, j] == CurrentStepColor && count > 0) return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка главной диагонали снизу
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool CheckDiag_LeftBot(int i, int j)
        {
            j++;
            i++;
            int count = 0;
            if (j < RightSide && i < TopSide)
            {
                while (j < RightSide && i < TopSide && mas[i, j] != -1 && mas[i, j] != CurrentStepColor)
                {
                    j++;
                    i++;
                    count++;
                }
                if (j < RightSide && i < TopSide && mas[i, j] == CurrentStepColor && count > 0) return true;
                else
                    return false;
            }
            else
                return false;
        }

        // ################################################################//
        /* ---------------- Изменение по всем направлениям --------------- */
        // ################################################################//
        /// <summary>
        /// Проверка правой половины горизонтали
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        private void ChangeHoriz_Right(int row, int col)
        {
            int k = col + 1;
            while (k != RightSide && mas[row, k] != -1  && mas[row, k] != CurrentStepColor)
            {
                if (mas[row, k] != CurrentStepColor)
                {
                    ChangePoints(CurrentStepColor);
                    ChangeCellState(CurrentStepColor, k, row);
                    mas[row, k] = CurrentStepColor;
                }
                k++;
            }
        }

        /// <summary>
        /// Проверка левой половины горизонтали
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        private void ChangeHoriz_Left(int row, int col)
        {
            int k = col - 1;
            while (k != LeftSide && mas[row, k] != -1 && mas[row, k] != CurrentStepColor)
            {
                if (mas[row, k] != CurrentStepColor)
                {
                    ChangePoints(CurrentStepColor);
                    ChangeCellState(CurrentStepColor, k, row);
                    mas[row, k] = CurrentStepColor;
                }
                k--;
            }
        }

        /// <summary>
        /// Проверка верхней половины побочной диагонали
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        private void ChangeDiag_RightTop(int row, int col)
        {
            col++;
            row--;
            while (col != RightSide && mas[row, col] != -1 && mas[row, col] != CurrentStepColor && row!=BottomSide)
            {
                if (mas[row, col] != CurrentStepColor)
                {
                    ChangePoints(CurrentStepColor);
                    ChangeCellState(CurrentStepColor, col, row);
                    mas[row, col] = CurrentStepColor;
                }
                col++;
                row--;
            }
        }

        /// <summary>
        /// Проверка нижней половины побочной диагонали
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        private void ChangeDiag_RightBot(int row, int col)
        {
            col--;
            row++;
            while (col != LeftSide && mas[row, col] != -1 && mas[row, col] != CurrentStepColor && row!=TopSide)
            {
                if (mas[row, col] != CurrentStepColor)
                {
                    ChangePoints(CurrentStepColor);
                    ChangeCellState(CurrentStepColor, col, row);
                    mas[row, col] = CurrentStepColor;
                }
                col--;
                row++;
            }
        }

        /// <summary>
        /// Проверка верхней половины вертикали
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        private void ChangeVert_Top(int row, int col)
        {
            row--;
            while (mas[row, col] != -1 && mas[row, col] != CurrentStepColor && row != BottomSide)
            {
                if (mas[row, col] != CurrentStepColor)
                {
                    ChangePoints(CurrentStepColor);
                    ChangeCellState(CurrentStepColor, col, row);
                    mas[row, col] = CurrentStepColor;
                }
                row--;
            }
        }

        /// <summary>
        /// Проверка нижней половины вертикали
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        private void ChangeVert_Bottom(int row, int col)
        {
            row++;
            while (mas[row, col] != -1 && mas[row, col] != CurrentStepColor && row != TopSide)
            {
                if (mas[row, col] != CurrentStepColor)
                {
                    ChangePoints(CurrentStepColor);
                    ChangeCellState(CurrentStepColor, col, row);
                    mas[row, col] = CurrentStepColor;
                }
                row++;
            }
        }

        /// <summary>
        /// Проверка верхней половины главной диагонали
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        private void ChangeDiag_LeftTop(int row, int col)
        {
            col--;
            row--;
            while (col != LeftSide && mas[row, col] != -1 && mas[row, col] != CurrentStepColor && row != BottomSide)
            {
                if (mas[row, col] != CurrentStepColor)
                {
                    ChangePoints(CurrentStepColor);
                    ChangeCellState(CurrentStepColor, col, row);
                    mas[row, col] = CurrentStepColor;
                }
                col--;
                row--;
            }
        }

        /// <summary>
        /// Проверка нижней половины главной диагонали
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        private void ChangeDiag_LeftBot(int row, int col)
        {
            col++;
            row++;
            while (col != RightSide && mas[row, col] != -1 && mas[row, col] != CurrentStepColor && row != TopSide)
            {
                if (mas[row, col] != CurrentStepColor)
                {
                    ChangePoints(CurrentStepColor);
                    ChangeCellState(CurrentStepColor, col, row);
                    mas[row, col] = CurrentStepColor;
                }
                col++;
                row++;
            }
        }
        // ###############################################################//
        /* ------------------ Объединённые методы --------------------*/
        // ###############################################################//

        /// <summary>
        /// Проверка линии справа по горизонтали от ячейки
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        /// <returns> true - изменения внесены, false - нет </returns>
        private bool CheckHR(int row, int col)
        {
            if (CheckHoriz_Rigth(row, col))
            {
                ChangeHoriz_Right(row, col);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка линии слева по горизонтали от ячейки
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        /// <returns> true - изменения внесены, false - нет </returns>
        private bool CheckHL(int row, int col)
        {
            if (CheckHoriz_Left(row, col))
            {
                ChangeHoriz_Left(row, col);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка линии в верхней половине побочной диагонали 
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        /// <returns> true - изменения внесены, false - нет </returns>
        private bool CheckDRT(int row, int col)
        {
            if (CheckDiag_RightTop(row, col))
            {
                ChangeDiag_RightTop(row, col);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка линии в нижней половине побочной диагонали 
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        /// <returns> true - изменения внесены, false - нет </returns>
        private bool CheckDRB(int row, int col)
        {
            if (CheckDiag_RightBot(row, col))
            {
                ChangeDiag_RightBot(row, col);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка линии в верхней половине вертикали
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        /// <returns> true - изменения внесены, false - нет </returns>
        private bool CheckVT(int row, int col)
        {
            if (CheckVert_Top(row, col))
            {
                ChangeVert_Top(row, col);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка линии в нижней половине вертикали
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        /// <returns> true - изменения внесены, false - нет </returns>
        private bool CheckVB(int row, int col)
        {
            if (CheckVert_Bottom(row, col))
            {
                ChangeVert_Bottom(row, col);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка линии в верхней половине главной диагонали
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        /// <returns> true - изменения внесены, false - нет </returns>
        private bool CheckDLT(int row, int col)
        {
            if (CheckDiag_LeftTop(row, col))
            {
                ChangeDiag_LeftTop(row, col);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка линии в нижней половине главной диагонали
        /// </summary>
        /// <param name="row"> строка </param>
        /// <param name="col"> столбец </param>
        /// <returns> true - изменения внесены, false - нет </returns>
        private bool CheckDLB(int row, int col)
        {
            if (CheckDiag_LeftBot(row, col))
            {
                ChangeDiag_LeftBot(row, col);
                return true;
            }
            else
                return false;
        }

        // ###############################################################//
        /* ------------------ Вспомогательные методы --------------------*/ 
        // ###############################################################//

        private bool CheckForEnd()
        {
            int i = 0;
            int j;
            bool f1, f2, f3, f4, f5, f6, f7, f8, fl;
            f1 = f2 = f3 = f4 = f5 = f6 = f7 = f8 = fl = false;


            while (i < TopSide && fl == false)
            {
                j = 0;
                while (j < RightSide && fl == false)
                {
                    if (mas[i, j] == -1)
                    {
                        f1 = CheckHoriz_Rigth(i, j);
                        f2 = CheckHoriz_Left(i, j);
                        f3 = CheckDiag_RightTop(i, j);
                        f4 = CheckDiag_RightBot(i, j);
                        f5 = CheckVert_Top(i, j);
                        f6 = CheckVert_Bottom(i, j);
                        f7 = CheckDiag_LeftTop(i, j);
                        f8 = CheckDiag_LeftBot(i, j);

                        if (f1 || f2 || f3 || f4 || f5 || f6 || f7 || f8)
                            fl = true;
                    }
                    j++;
                }
                i++;
            }
            if (fl == false)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Метод динамического изменения очков для каждого игрока
        /// </summary>
        /// <param name="CurrentStepColor"></param>
        private void ChangePoints(int CurrentStepColor)
        {
            if (CurrentStepColor == 0)
            {
                Points_U1--;
                Points_U2++;
            }
            else
            {
                Points_U2--;
                Points_U1++;
            }
        }

        /// <summary>
        /// Увеличение количества очков игрока
        /// </summary>
        /// <param name="CurrentStepColor"></param>
        private void IncrPoints(int CurrentStepColor)
        {
            if (CurrentStepColor == 0)
            {
                Points_U2++;
            }
            else
            {
                Points_U1++;
            }
        }
        
        /// <summary>
        /// Получение значений очков с формы
        /// </summary>
        private void RewritePoints()
        {
            U1_Points.Text = Points_U1.ToString();
            U2_Points.Text = Points_U2.ToString();
        }
        
        /// <summary>
        /// Очистка таблицы
        /// </summary>
        /// <param name="f"></param>
        private void CreateTableRows()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            DataGridViewColumn[] cols = new DataGridViewColumn[RightSide];
            for (int i = 0; i < RightSide; i++)
                cols[i] = new DataGridViewImageColumn();

            foreach (DataGridViewColumn col in cols)
            {
                    col.FillWeight = 100;
                    col.MinimumWidth = 5;
                    col.Width = 60;
            }

            dataGridView1.Columns.AddRange(cols);

            for (int i = LeftSide + 1; i < RightSide - 1; i++)
            {
                if (RightSide == 4)
                    dataGridView1.Rows.Add(ClearBgImage, ClearBgImage, ClearBgImage, ClearBgImage);
                else
                    dataGridView1.Rows.Add(ClearBgImage, ClearBgImage, ClearBgImage, ClearBgImage, ClearBgImage, ClearBgImage, ClearBgImage, ClearBgImage);
            }
        }
        
        /// <summary>
        /// Формирование матрицы состояний
        /// </summary>
        private void InitParams()
        {
            for (int i = LeftSide+1; i < RightSide; i++)
                for (int j = LeftSide+1; j < RightSide; j++)
                    mas[i, j] = -1;

            int ic = RightSide / 2;
            int jc = RightSide / 2;
            mas[ic-1, jc-1] = 0;
            mas[ic - 1, jc] = 1;
            mas[ic, jc - 1] = 1;
            mas[ic, jc] = 0;
        }

        /// <summary>
        /// Вставка изображений в таблицу
        /// </summary>
        private void EnterIms()
        {
            for (int i = LeftSide+1; i< RightSide; i++)
                for (int j = LeftSide+1; j < RightSide; j++)
                {
                    switch (mas[i, j])
                    {
                        case -1: dataGridView1[i, j].Value = ClearBgImage;
                            break;
                        case 0: dataGridView1[i, j].Value = WhiteCRImage;
                            break;
                        case 1: dataGridView1[i, j].Value = BlackCRImage;
                            break;
                    }
                }
        }

        /// <summary>
        /// Изменить состояние ячейки
        /// </summary>
        /// <param name="CurrentStepColor_"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        private void ChangeCellState(int CurrentStepColor_,int col, int row)
        {
            switch (CurrentStepColor_)
            {
                case 0:
                    {
                        dataGridView1[col, row].Value = WhiteCRImage;
                        break;
                    }
                case 1:
                    {
                        dataGridView1[col, row].Value = BlackCRImage;
                        break;
                    }
            }
            CurrentNumberOfSteps++;
        }

        /// <summary>
        /// Печать матрицы состояний
        /// </summary>
        private void PrintMas()
        {
            richTextBox1.Text += "-------------------------------------------\n";
            for (int i = LeftSide+1; i < RightSide; i++)
            {
                for (int j = LeftSide+1; j < RightSide; j++)
                    richTextBox1.Text += mas[i, j] + " ";
                richTextBox1.Text += "\n";
            }
            richTextBox1.Text += "-------------------------------------------\n";
        }

        /// <summary>
        /// Изменение цвета текущей фишки
        /// </summary>
        private void ChangeStepColor()
        {
            if (CurrentStepColor == 1)
            {
                CurrentStepColor = 0;
                CurrentStateImg.Image = WhiteCRImage;
            }
            else
            {
                CurrentStepColor = 1;
                CurrentStateImg.Image = BlackCRImage;
            }
        }

        private void NewGame()
        {
            InitParams();
            EnterIms();
            PrintMas();

            CurrentStateImg.Image = BlackCRImage;
            U1_Points.Text = "2";
            U2_Points.Text = "2";
            Points_U1 = 2;
            Points_U2 = 2;
            PrintMas();
        }
        // ###############################################################//
        /* -------------------- Методы реакции формы --------------------*/
        // ###############################################################//

        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Загрузка параметров конфигурации из XML
        /// </summary>
        /// <param name="conf"> строковый параметр для задания конфигурации </param>
        private void LoadConfig(string conf)
        {
            /*System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load("XmlConfig.xml");*/
            string ClearBgImagePath = "",
                   WhiteCRImagePath = "",
                   BlackCRImagePath = "";

                // my XmlDocument (in this case I will load from hardisk)
                //XmlDocument xml = new XmlDocument();
                // load the XSD schema.. is this right?
                //xml.Schemas.Add("http://www.w3.org/2001/XMLSchema", "XmlConfig.xsd");

                // Load my XML from hardisk
                //xml.Load("XmlConfig.xml");

                // event handler to manage the errors from XmlDocument object
                //ValidationEventHandler veh = new ValidationEventHandler(verifyErrors);

                // try to validate my XML.. and the event handler verifyError will show the error
               // xml.Validate(veh);

            XmlReaderSettings booksSettings = new XmlReaderSettings();
            booksSettings.Schemas.Add("http://www.w3.org/2001/XMLSchema", "..//..//XmlConfig.xsd");
            booksSettings.ValidationType = ValidationType.Schema;
            booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

            XmlReader books = XmlReader.Create("XmlConfig.xml", booksSettings);
            isValid = true;
            while (books.Read()) { } 

            // Wenn EventHandler nicht ausgelöst wird bleibt isValid: true.                

            if (isValid == true)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("XmlConfig.xml");
                foreach (System.Xml.XmlNode node in doc.DocumentElement)
                {
                    if (node.Attributes[0].Value == conf)
                    {
                        LeftSide = int.Parse(node["gameField"]["LeftSide"].InnerText);
                        RightSide = int.Parse(node["gameField"]["RightSide"].InnerText);
                        TopSide = int.Parse(node["gameField"]["TopSide"].InnerText);
                        BottomSide = int.Parse(node["gameField"]["BottomSide"].InnerText);
                        CurrentStepColor = int.Parse(node["CurrentStepColor"].InnerText);
                        CurrentNumberOfSteps = int.Parse(node["CurrentNumberOfSteps"].InnerText);
                        ClearBgImagePath = node["CLearBgImage"].InnerText;
                        WhiteCRImagePath = node["WhiteCRImage"].InnerText;
                        BlackCRImagePath = node["BlackCRImage"].InnerText;

                    }
                }
                ClearBgImage = new Bitmap(ClearBgImagePath);
                WhiteCRImage = new Bitmap(WhiteCRImagePath);
                BlackCRImage = new Bitmap(BlackCRImagePath);

                mas = new int[RightSide, TopSide];
            }
            else
            {
                LeftSide = -1;
                RightSide = 4;
                TopSide = 4;
                BottomSide = -1;
                CurrentStepColor = 1;
                CurrentNumberOfSteps = 1;
                ClearBgImagePath = "../../ClearBG.jpg";
                WhiteCRImagePath = "../../WhiteCR50.jpg";
                BlackCRImagePath = "../../BlackCR50.jpg";
                ClearBgImage = new Bitmap(ClearBgImagePath);
                WhiteCRImage = new Bitmap(WhiteCRImagePath);
                BlackCRImage = new Bitmap(BlackCRImagePath);
                mas = new int[RightSide, TopSide];
            }
          
        }
        static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                isValid = false;
                MessageBox.Show("WARNING: ");
                
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                isValid = false;
                MessageBox.Show("ERROR: ");
               
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfig("config1");
            CreateTableRows();
            NewGame();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;
            bool f1, f2, f3, f4, f5, f6, f7, f8, fl;
            f1 = f2 = f3 = f4 = f5 = f6 = f7 = f8 = fl = false;

            if (!CheckForEnd())
            {

                if (mas[row, col] == -1)
                {

                    f1 = CheckHR(row, col);
                    f2 = CheckHL(row, col);
                    f3 = CheckDRT(row, col);
                    f4 = CheckDRB(row, col);
                    f5 = CheckVT(row, col);
                    f6 = CheckVB(row, col);
                    f7 = CheckDLT(row, col);
                    f8 = CheckDLB(row, col);

                    if (f1 || f2 || f3 || f4 || f5 || f6 || f7 || f8)
                        fl = true;

                    if (fl)
                    {

                        mas[row, col] = CurrentStepColor;
                        ChangeCellState(CurrentStepColor, col, row);
                        IncrPoints(CurrentStepColor);
                        ChangeStepColor();
                        RewritePoints();
                        PrintMas();

                    }
                    else
                        MessageBox.Show(" Нерезультативный ход! Должна измениться хоть одна фишка!");
                }
                else
                    MessageBox.Show("Ставить фишку можно только в пустую клетку!");
            }
            else
            {
                if (Convert.ToInt32(U1_Points.Text) > Convert.ToInt32(U2_Points.Text))
                MessageBox.Show(" ПОБЕДА ЧЁРНЫХ! ХОДОВ БОЛЬШЕ НЕТ");
                else
                    MessageBox.Show(" ПОБЕДА БЕЛЫХ! ХОДОВ БОЛЬШЕ НЕТ");  
            }
        }

        private void x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadConfig("config1");
            CreateTableRows();
            NewGame();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            LoadConfig("config2");
            CreateTableRows();
            NewGame();
        }


    }
}
