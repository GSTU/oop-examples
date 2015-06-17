using ImageProcessing;
using MinThantSin.OpenSourceGames.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
namespace MinThantSin.OpenSourceGames
{   
    public partial class MainForm : Form
    {
        private bool _victoryAnnounced;
        private bool _canMovePiece;
        private int _previousMouseX, _previousMouseY;
        private int _previousClientWidth, _previousClientHeight;
        private int _puzzlePictureWidth, _puzzlePictureHeight;
        private Bitmap _board;
        private Bitmap _backBuffer;
        private Bitmap _background;
        private Bitmap _sourcePicture;
        private PieceCluster _currentCluster;
        List<PieceCluster> _clusters = new List<PieceCluster>();
        public MainForm()
        {
            InitializeComponent();
        }
        string сartinka = "Summer";
        string Rasshirenie = "jpg";
        static bool valid = true;

        public static void ValidationHandler(object sender, ValidationEventArgs args)
        {
            MessageBox.Show(args.Severity + ":" + args.Message + "\nЗагружены стандартные параметры");
            valid = false;
        }


         






     

        int aaa;
        int bbb;
        int ccc;
        string Cartinka;
       


        private void MainForm_Load(object sender, EventArgs e)
        {
          
            _previousClientWidth = this.ClientSize.Width;
            _previousClientHeight = this.ClientSize.Height;
            _puzzlePictureWidth = this.ClientSize.Width;
            _puzzlePictureHeight = this.ClientSize.Height - menuStrip1.Height;


         
            //Загрузка компонентов
            aaa = 0;
            bbb = 100;
            ccc = 200;


            XmlTextReader reader = new XmlTextReader("XMLFile1.xml");
            XmlValidatingReader validreader = new XmlValidatingReader(reader);
            validreader.Schemas.Add(null, "XMLSchema1.xsd");
            validreader.ValidationType = ValidationType.Schema;
            validreader.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);

            try
            {
                while (validreader.Read()) ;
            }
            catch
            {
                valid = false;
                MessageBox.Show("Файл конфигурации не найден\nЗагружены параметры по умолчанию");
            }
            validreader.Close();

            if (valid)
            {
                reader = new XmlTextReader("XMLFile1.xml");
                while (reader.Read())
                {
                    if (reader.Name == "aaa" && reader.NodeType != XmlNodeType.EndElement)
                        aaa = reader.ReadElementContentAsInt();
                 
                    
                //    SolidBrush sbr = new SolidBrush(Color.FromName(fieldColor));



                    if (reader.Name == "bbb" && reader.NodeType != XmlNodeType.EndElement)
                        bbb = reader.ReadElementContentAsInt();
                    if (reader.Name == "ccc" && reader.NodeType != XmlNodeType.EndElement)
                        ccc = reader.ReadElementContentAsInt();

                    if (reader.Name == "cartinka" && reader.NodeType != XmlNodeType.EndElement)
                        Cartinka = reader.ReadElementContentAsString();

                    if (reader.Name == "rasshirenie" && reader.NodeType != XmlNodeType.EndElement)
                        Rasshirenie = reader.ReadElementContentAsString();


                }
                reader.Close();
            }




         





        }
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (_board != null)
            {
                using (Graphics gfx = this.CreateGraphics())
                {
                    gfx.DrawImageUnscaled(_board, 0, 0);
                }
            }            
        }
        private void MainForm_ClientSizeChanged(object sender, EventArgs e)
        {
            if (_previousClientWidth != this.ClientSize.Width || _previousClientHeight != this.ClientSize.Height)
            {
                _previousClientWidth = this.ClientSize.Width;
                _previousClientHeight = this.ClientSize.Height;
                DisplayJigsawPuzzle(Settings.Default.ShowImageHint);                
            }
        }
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            int selectedIndex = -1;

            for (int index = (_clusters.Count - 1); index >= 0; index--)
            {
                if (_clusters[index].MovableFigure.IsVisible(e.X, e.Y))
                {
                    selectedIndex = index;
                    break;
                }
            }
            if (selectedIndex >= 0)
            {
                _currentCluster = _clusters[selectedIndex];
                _clusters.RemoveAt(selectedIndex);
                _clusters.Add(_currentCluster);
                using (Graphics gfx = Graphics.FromImage(_backBuffer))
                {
                    Rectangle currentClusterBoardLocation = _currentCluster.BoardLocation;
                    gfx.DrawImage(_background, currentClusterBoardLocation, currentClusterBoardLocation, GraphicsUnit.Pixel);          
                    Region currentClusterBoardLocationRegion = new Region(_currentCluster.BoardLocation);
                    foreach (PieceCluster cluster in _clusters)
                    {
                        if (cluster != _currentCluster)
                        {
                            Region clusterRegion = new Region(cluster.MovableFigure);
                            clusterRegion.Intersect(currentClusterBoardLocationRegion);

                            if (!clusterRegion.IsEmpty(gfx))
                            {                                
                                gfx.SetClip(clusterRegion, CombineMode.Replace);
                                gfx.DrawImageUnscaled(cluster.Picture, cluster.BoardLocation);
                            }
                        }
                    }              
                }
                Matrix matrix = new Matrix();
                SolidBrush shadowBrush = new SolidBrush(GameSettings.DROP_SHADOW_COLOR);
                using (Graphics gfx = Graphics.FromImage(_board))
                {
                    matrix.Reset();
                    matrix.Translate(GameSettings.DROP_SHADOW_DEPTH, GameSettings.DROP_SHADOW_DEPTH);
                    GraphicsPath shadowFigure = (GraphicsPath)_currentCluster.MovableFigure.Clone();
                    shadowFigure.Transform(matrix);
                    gfx.ResetClip();
                    gfx.SetClip(shadowFigure);
                    gfx.FillPath(shadowBrush, shadowFigure);
                    gfx.ResetClip();
                    gfx.SetClip(_currentCluster.MovableFigure);
                    gfx.DrawImageUnscaled(_currentCluster.Picture, _currentCluster.BoardLocation);         
                }
                using (Graphics gfx = this.CreateGraphics())
                {
                    gfx.DrawImageUnscaled(_board, 0, 0);
                }
                _previousMouseX = e.X;
                _previousMouseY = e.Y;
                _canMovePiece = true;
            }     
        }
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_canMovePiece)
            {
                // по гор дв част
                int offsetX = e.X - _previousMouseX;
                int offsetY = e.Y - _previousMouseY;

                Rectangle currentClusterBoardLocation = _currentCluster.BoardLocation;

                // тек пол зам на старое
                int clusterOldX = currentClusterBoardLocation.X;
                int clusterOldY = currentClusterBoardLocation.Y;

                // нов поз
                int clusterNewX = currentClusterBoardLocation.X + offsetX;
                int clusterNewY = currentClusterBoardLocation.Y + offsetY;

                // стар кластер
                Rectangle clusterOldBoardLocation = new Rectangle(clusterOldX, clusterOldY,
                                                        currentClusterBoardLocation.Width + GameSettings.DROP_SHADOW_DEPTH,
                                                        currentClusterBoardLocation.Height + GameSettings.DROP_SHADOW_DEPTH);

                // нов кластер
                Rectangle clusterNewBoardLocation = new Rectangle(clusterNewX, clusterNewY,
                                                        currentClusterBoardLocation.Width + GameSettings.DROP_SHADOW_DEPTH,
                                                        currentClusterBoardLocation.Height + GameSettings.DROP_SHADOW_DEPTH);

                Rectangle combinedClusterRect = Rectangle.Union(clusterOldBoardLocation, clusterNewBoardLocation);
                SolidBrush shadowBrush = new SolidBrush(GameSettings.DROP_SHADOW_COLOR);
                Matrix matrix = new Matrix();

                using (Graphics gfx = Graphics.FromImage(_board))
                {
                    gfx.DrawImage(_backBuffer, combinedClusterRect, combinedClusterRect, GraphicsUnit.Pixel);

                    matrix.Reset();
                    matrix.Translate(offsetX + GameSettings.DROP_SHADOW_DEPTH, offsetY + GameSettings.DROP_SHADOW_DEPTH);

                    GraphicsPath shadowFigure = (GraphicsPath)_currentCluster.MovableFigure.Clone();
                    shadowFigure.Transform(matrix);
                    gfx.FillPath(shadowBrush, shadowFigure);

                    // обновление доски
                    _currentCluster.BoardLocation = new Rectangle(clusterNewX, clusterNewY, _currentCluster.Width, _currentCluster.Height);


                    matrix.Reset();
                    matrix.Translate(offsetX, offsetY);
                    _currentCluster.MovableFigure.Transform(matrix);

                    // нарисовка картинки 
                    gfx.ResetClip();
                    gfx.SetClip(_currentCluster.MovableFigure);
                    gfx.DrawImageUnscaled(_currentCluster.Picture, _currentCluster.BoardLocation);


                    foreach (Piece piece in _currentCluster.Pieces)
                    {
                        int pieceNewX = piece.BoardLocation.X + offsetX;
                        int pieceNewY = piece.BoardLocation.Y + offsetY;
                        piece.BoardLocation = new Rectangle(pieceNewX, pieceNewY, piece.Width, piece.Height);

                        matrix.Reset();
                        matrix.Translate(offsetX, offsetY);
                        piece.MovableFigure.Transform(matrix);                        
                    }
                    

                }

                using (Graphics gfx = this.CreateGraphics())
                {
                    gfx.DrawImage(_board, combinedClusterRect, combinedClusterRect, GraphicsUnit.Pixel);
                }

                _previousMouseX = e.X;
                _previousMouseY = e.Y;
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (_canMovePiece)
            {
                _previousMouseX = e.X;
                _previousMouseY = e.Y;

                using (Graphics gfx = Graphics.FromImage(_backBuffer))
                {
                    gfx.ResetClip();
                    gfx.SetClip(_currentCluster.MovableFigure);
                    gfx.DrawImageUnscaled(_currentCluster.Picture, _currentCluster.BoardLocation);
                }

                using (Graphics gfx = Graphics.FromImage(_board))
                {
                    gfx.DrawImageUnscaled(_backBuffer, 0, 0);
                }

                using (Graphics gfx = this.CreateGraphics())
                {
                    gfx.DrawImageUnscaled(_backBuffer, 0, 0);
                }


                // Соед части маз

                Matrix matrix = new Matrix();                

                // соед смежных частией в один кластер 
                List<int> adjacentClusterIDs = new List<int>();

                // проверка если ли рядом нужный кусок
                for (int i = 0; i < _currentCluster.Pieces.Count; i++)
                {
                    Piece currentPiece = _currentCluster.Pieces[i];

                    foreach (int pieceID in currentPiece.AdjacentPieceIDs)
                    {
                        Piece adjacentPiece = GetPieceByID(pieceID);

                        if (adjacentPiece != null && (adjacentPiece.ClusterID != currentPiece.ClusterID))
                        {                                                        
                           // соед частей если нужный кусок находится справо 

                            Rectangle adjacentPieceMovableFigureBoardLocation = Rectangle.Truncate(adjacentPiece.MovableFigure.GetBounds());
                            Rectangle currentPieceMovableFigureBoardLocation = Rectangle.Truncate(currentPiece.MovableFigure.GetBounds());


                            // разбежка 2 пикселя соеденит две части 
                            if (Math.Abs(currentPiece.SourcePictureLocation.X - adjacentPiece.SourcePictureLocation.X) <= 2)
                            {
                                int figureYDifferenceSign = Math.Sign(currentPieceMovableFigureBoardLocation.Y - adjacentPieceMovableFigureBoardLocation.Y);
                                int sourcePictureYDifferenceSign = Math.Sign(currentPiece.SourcePictureLocation.Y - adjacentPiece.SourcePictureLocation.Y);

                                // если нужный кусок находится не с той стороный которой нужно то не соед 
                                if (figureYDifferenceSign != sourcePictureYDifferenceSign)
                                {
                                    continue;
                                }
                            }
                            else if (Math.Abs(currentPiece.SourcePictureLocation.Y - adjacentPiece.SourcePictureLocation.Y) <= 2)
                            {
                                int figureXDifferenceSign = Math.Sign(currentPieceMovableFigureBoardLocation.X - adjacentPieceMovableFigureBoardLocation.X);
                                int sourceImageXDifferenceSign = Math.Sign(currentPiece.SourcePictureLocation.X - adjacentPiece.SourcePictureLocation.X);

                                if (figureXDifferenceSign != sourceImageXDifferenceSign)
                                {
                                    continue;
                                }
                            }

                            GraphicsPath combinedMovableFigure = new GraphicsPath();
                            combinedMovableFigure.AddPath(adjacentPiece.MovableFigure, false);
                            combinedMovableFigure.AddPath(currentPiece.MovableFigure, false);

                            Rectangle combinedMovableFigureBoardLocation = Rectangle.Truncate(combinedMovableFigure.GetBounds());
                         
                            Rectangle combinedSourcePictureLocation = Rectangle.Union(adjacentPiece.SourcePictureLocation, currentPiece.SourcePictureLocation);

                            if (Math.Abs(combinedMovableFigureBoardLocation.Width - combinedSourcePictureLocation.Width) <= GameSettings.SNAP_TOLERANCE &&
                                Math.Abs(combinedMovableFigureBoardLocation.Height - combinedSourcePictureLocation.Height) <= GameSettings.SNAP_TOLERANCE)
                            {
                                PieceCluster adjacentPieceCluster = GetPieceClusterByID(adjacentPiece.ClusterID);

                                adjacentClusterIDs.Add(adjacentPieceCluster.ID);                                    

                                // обновление кластера для соед частей
                                foreach (Piece piece in adjacentPieceCluster.Pieces)
                                {
                                    piece.ClusterID = currentPiece.ClusterID;                                       
                                }
                            }

                                      
                        }
                    }
                }
                
                if (adjacentClusterIDs.Count > 0)
                {
                 

                    foreach (int clusterID in adjacentClusterIDs)
                    {
                        PieceCluster adjacentCluster = GetPieceClusterByID(clusterID);

                        foreach (Piece piece in adjacentCluster.Pieces)
                        {
                            _currentCluster.Pieces.Add(piece);
                        }
                        
                        RemovePieceGroupByID(clusterID);
                    }

                    GraphicsPath combinedStaticFigure = new GraphicsPath();                   
                    Rectangle combinedBoardLocation = _currentCluster.BoardLocation;
                    Rectangle combinedSourcePictureLocation = _currentCluster.SourcePictureLocation;

                    foreach (Piece piece in _currentCluster.Pieces)
                    {
                        combinedStaticFigure.AddPath(piece.StaticFigure, false);
                        combinedBoardLocation = Rectangle.Union(combinedBoardLocation, piece.BoardLocation);
                        combinedSourcePictureLocation = Rectangle.Union(combinedSourcePictureLocation, piece.SourcePictureLocation);                        
                    }

                    _currentCluster.BoardLocation = new Rectangle(combinedBoardLocation.X, combinedBoardLocation.Y,
                                                                        combinedSourcePictureLocation.Width,
                                                                        combinedSourcePictureLocation.Height);

                    _currentCluster.SourcePictureLocation = combinedSourcePictureLocation;
                    _currentCluster.Width = combinedSourcePictureLocation.Width;
                    _currentCluster.Height = combinedSourcePictureLocation.Height;
                    _currentCluster.StaticFigure = (GraphicsPath)combinedStaticFigure.Clone();
                    _currentCluster.MovableFigure = (GraphicsPath)combinedStaticFigure.Clone();

                    Rectangle combinedStaticFigureLocation = Rectangle.Truncate(combinedStaticFigure.GetBounds());
                                                            
                    matrix.Reset();
                    matrix.Translate(0 - combinedStaticFigureLocation.X, 0 - combinedStaticFigureLocation.Y);
                    _currentCluster.MovableFigure.Transform(matrix);

                    matrix.Reset();
                    matrix.Translate(combinedBoardLocation.X, combinedBoardLocation.Y);
                    _currentCluster.MovableFigure.Transform(matrix);                    
                    
                    // строим картику
                    
                    matrix.Reset();
                    matrix.Translate(0 - combinedStaticFigureLocation.X, 0 - combinedStaticFigureLocation.Y);
                    GraphicsPath translatedCombinedStaticFigure = (GraphicsPath)combinedStaticFigure.Clone();
                    translatedCombinedStaticFigure.Transform(matrix);                    
                    
                    Bitmap clusterPicture = new Bitmap(combinedSourcePictureLocation.Width, combinedSourcePictureLocation.Height);

                    using (Graphics gfx = Graphics.FromImage(clusterPicture))
                    {
                        gfx.FillRectangle(Brushes.White, 0, 0, clusterPicture.Width, clusterPicture.Height);

                        gfx.ResetClip();
                        gfx.SetClip(translatedCombinedStaticFigure);
                        gfx.DrawImage(_sourcePicture, new Rectangle(0, 0, clusterPicture.Width, clusterPicture.Height),
                                combinedStaticFigureLocation, GraphicsUnit.Pixel);

                        if (GameSettings.DRAW_PIECE_OUTLINE)
                        {
                            Pen outlinePen = new Pen(Color.Black)
                            {
                                Width = GameSettings.PIECE_OUTLINE_WIDTH,
                                Alignment = PenAlignment.Inset
                            };

                            gfx.SmoothingMode = SmoothingMode.AntiAlias;
                            gfx.DrawPath(outlinePen, translatedCombinedStaticFigure);
                        }
                    }

                    Bitmap modifiedClusterPicture = (Bitmap)clusterPicture.Clone();
                    ImageUtilities.EdgeDetectHorizontal(modifiedClusterPicture);
                    ImageUtilities.EdgeDetectVertical(modifiedClusterPicture);
                    clusterPicture = ImageUtilities.AlphaBlendMatrix(modifiedClusterPicture, clusterPicture, 200);

                 

                    _currentCluster.Picture = (Bitmap)clusterPicture.Clone();

           
                    foreach (Piece piece in _currentCluster.Pieces)
                    {
                        int offsetX = piece.SourcePictureLocation.X - combinedSourcePictureLocation.X;
                        int offsetY = piece.SourcePictureLocation.Y - combinedSourcePictureLocation.Y;

                        int newLocationX = combinedBoardLocation.X + offsetX;
                        int newLocationY = combinedBoardLocation.Y + offsetY;

                        piece.BoardLocation = new Rectangle(newLocationX, newLocationY, piece.Width, piece.Height);

                        Rectangle movableFigureBoardLocation = Rectangle.Truncate(piece.MovableFigure.GetBounds());

                        matrix.Reset();
                        matrix.Translate(0 - movableFigureBoardLocation.X, 0 - movableFigureBoardLocation.Y);
                        piece.MovableFigure.Transform(matrix);

                        matrix.Reset();
                        matrix.Translate(newLocationX, newLocationY);
                        piece.MovableFigure.Transform(matrix);
                    }

       
                    // перерисовка обновление всего 

                    Rectangle areaToClear = new Rectangle(combinedBoardLocation.X, combinedBoardLocation.Y,
                                                            combinedBoardLocation.Width + GameSettings.DROP_SHADOW_DEPTH,
                                                            combinedBoardLocation.Height + GameSettings.DROP_SHADOW_DEPTH);

                    using (Graphics gfx = Graphics.FromImage(_backBuffer))
                    {
                        // очистка фона
                        gfx.DrawImage(_background, areaToClear, areaToClear, GraphicsUnit.Pixel);                        

                      

                        Region regionToRedraw = new Region(areaToClear);

                        foreach (PieceCluster cluster in _clusters)
                        {
                            Region clusterRegion = new Region(cluster.MovableFigure);
                            clusterRegion.Intersect(regionToRedraw);

                            if (!clusterRegion.IsEmpty(gfx))
                            {
                                gfx.SetClip(clusterRegion, CombineMode.Replace);
                                gfx.DrawImageUnscaled(cluster.Picture, cluster.BoardLocation);
                            }
                        }

                    
                    }

          

                    using (Graphics gfx = Graphics.FromImage(_board))
                    {
                        gfx.DrawImageUnscaled(_backBuffer, 0, 0);
                    }


             

                    using (Graphics gfx = this.CreateGraphics())
                    {
                        gfx.DrawImageUnscaled(_backBuffer, 0, 0);
                    }


              
                }

               
                
                _canMovePiece = false;
                _currentCluster = null;

              // победка

                if (_clusters.Count == 1)
                {
                    if (_victoryAnnounced == false)
                    {
                        _victoryAnnounced = true;
                        MessageBox.Show("Победа!!!", "Победа!!!", MessageBoxButtons.OK);
                    }
                }

       
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OpenFileMenuItem_Click(object sender, EventArgs e)
        {
            string cartinka = "Summer";
           // MessageBox.Show(Application.StartupPath);

            string ttt = Application.StartupPath;
           // DialogResult result = openFileDialog1.ShowDialog(this);
            
            openFileDialog1.FileName = ""+ttt+"\\"+Cartinka+"."+Rasshirenie+"";

           // if (result == DialogResult.OK)
           // {
                if (!String.IsNullOrEmpty(openFileDialog1.FileName))
                {                    
                    if (_sourcePicture != null)
                    {
                        _sourcePicture.Dispose();
                    }




                    try
                    {
                        _sourcePicture = new Bitmap(openFileDialog1.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Картинки нету!!! Загружены стандартные параметры!!!");
                        openFileDialog1.FileName = "" + ttt + "\\" + cartinka + "." + Rasshirenie + "";
                        _sourcePicture = new Bitmap(openFileDialog1.FileName);
                    }






                    try
                    {                        
                        CreateJigsawPuzzle();
                        DisplayJigsawPuzzle(Settings.Default.ShowImageHint);
                        _victoryAnnounced = false;
               
                    }
                    catch (Exception ex)
                    {
                        _sourcePicture.Dispose();
                        MessageBox.Show(ex.Message);
                    }
                }
           // }
        }

 

       

        
        private void CreateJigsawPuzzle()
        {
          
           
            int pieceWidth = _sourcePicture.Width / GameSettings.NUM_COLUMNS;
            int pieceHeight = _sourcePicture.Height / GameSettings.NUM_ROWS;


            int lastColPieceWidth = pieceWidth + (_sourcePicture.Width % GameSettings.NUM_COLUMNS);
            int lastRowPieceHeight = pieceHeight + (_sourcePicture.Height % GameSettings.NUM_ROWS);



            int lastRow = (GameSettings.NUM_ROWS - 1);
            int lastCol = (GameSettings.NUM_COLUMNS - 1);

            _currentCluster = null;
            _clusters = new List<PieceCluster>();

            Matrix matrix = new Matrix();

            Pen outlinePen = new Pen(Color.Black)
            {
                Width = GameSettings.PIECE_OUTLINE_WIDTH,
                Alignment = PenAlignment.Inset
            };

            int pieceID = 0;            

            for (int row = 0; row < GameSettings.NUM_ROWS; row++)
            {
             
                bool topCurveFlipVertical = (row % 2 == 0);
                bool bottomCurveFlipVertical = (row % 2 != 0);
                
                for (int col = 0; col < GameSettings.NUM_COLUMNS; col++)
                {
             
                    bool leftCurveFlipHorizontal = (col % 2 != 0);
                    bool rightCurveFlipHorizontal = (col % 2 == 0);

               
                    if (row % 2 == 0)
                    {
                        leftCurveFlipHorizontal = (col % 2 == 0);
                        rightCurveFlipHorizontal = (col % 2 != 0);
                    }

                
                    topCurveFlipVertical = !topCurveFlipVertical;
                    bottomCurveFlipVertical = !bottomCurveFlipVertical;

                    GraphicsPath figure = new GraphicsPath();

                    int offsetX = (col * pieceWidth);
                    int offsetY = (row * pieceHeight);
                    int horizontalCurveLength = (col == lastCol ? lastColPieceWidth : pieceWidth);
                    int verticalCurveLength = (row == lastRow ? lastRowPieceHeight : pieceHeight);

             // Top

                    if (row == 0)
                    {
                        int startX = offsetX;
                        int startY = offsetY;
                        int endX = offsetX + horizontalCurveLength;
                        int endY = offsetY;

                        figure.AddLine(startX, startY, endX, endY);
                    }
                    else
                    {
                        BezierCurve topCurve = BezierCurve.CreateHorizontal(horizontalCurveLength);

                        if (topCurveFlipVertical)
                        {
                            topCurve.FlipVertical();
                        }

                        topCurve.Translate(offsetX, offsetY);
                        figure.AddBeziers(topCurve.Points);
                    }

        

               //    Right

                    if (col == lastCol)
                    {
                        int startX = offsetX + lastColPieceWidth;
                        int startY = offsetY;
                        int endX = offsetX + lastColPieceWidth;
                        int endY = offsetY + verticalCurveLength;

                        figure.AddLine(startX, startY, endX, endY);
                    }
                    else
                    {
                        BezierCurve verticalCurve = BezierCurve.CreateVertical(verticalCurveLength);

                        if (rightCurveFlipHorizontal)
                        {
                            verticalCurve.FlipHorizontal();
                        }

                        verticalCurve.Translate(offsetX + pieceWidth, offsetY);
                        figure.AddBeziers(verticalCurve.Points);
                    }

            

                //    Bottom

                    if (row == lastRow)
                    {
                        int startX = offsetX;
                        int startY = offsetY + lastRowPieceHeight;
                        int endX = offsetX + horizontalCurveLength;
                        int endY = offsetY + lastRowPieceHeight;

                        figure.AddLine(endX, endY, startX, startY);
                    }
                    else
                    {
                        BezierCurve bottomCurve = BezierCurve.CreateHorizontal(horizontalCurveLength);
                        bottomCurve.FlipHorizontal();

                        if (bottomCurveFlipVertical)
                        {
                            bottomCurve.FlipVertical();
                        }

                        bottomCurve.Translate(offsetX + horizontalCurveLength, offsetY + pieceHeight);
                        figure.AddBeziers(bottomCurve.Points);
                    }

             

              //     Left

                    if (col == 0)
                    {
                        int startX = offsetX;
                        int startY = offsetY;
                        int endX = offsetX;
                        int endY = offsetY + verticalCurveLength;

                        figure.AddLine(endX, endY, startX, startY);
                    }
                    else
                    {
                        BezierCurve verticalCurve = BezierCurve.CreateVertical(verticalCurveLength);
                        verticalCurve.FlipVertical();

                        if (leftCurveFlipHorizontal)
                        {
                            verticalCurve.FlipHorizontal();
                        }

                        verticalCurve.Translate(offsetX, offsetY + verticalCurveLength);
                        figure.AddBeziers(verticalCurve.Points);
                    }

      

                    List<Coordinate> adjacentCoords = new List<Coordinate>
                    {
                        new Coordinate(col, row - 1),
                        new Coordinate(col + 1, row),
                        new Coordinate(col, row + 1),
                        new Coordinate(col - 1, row)
                    };

                    List<int> adjacentPieceIDs = DetermineAdjacentPieceIDs(adjacentCoords, GameSettings.NUM_COLUMNS);

     

                //   Постр картинку

                    Rectangle figureLocation = Rectangle.Truncate(figure.GetBounds());

                
                    matrix.Reset();
                    matrix.Translate(0 - figureLocation.X, 0 - figureLocation.Y);
                    GraphicsPath translatedFigure = (GraphicsPath)figure.Clone();
                    translatedFigure.Transform(matrix);

                    Rectangle translatedFigureLocation = Rectangle.Truncate(translatedFigure.GetBounds());

                    Bitmap piecePicture = new Bitmap(figureLocation.Width, figureLocation.Height);

                    using (Graphics gfx = Graphics.FromImage(piecePicture))
                    {
                        gfx.FillRectangle(Brushes.White, 0, 0, piecePicture.Width, piecePicture.Height);
                        gfx.ResetClip();
                        gfx.SetClip(translatedFigure);
                        gfx.DrawImage(_sourcePicture, new Rectangle(0, 0, piecePicture.Width, piecePicture.Height),
                                figureLocation, GraphicsUnit.Pixel);

                        if (GameSettings.DRAW_PIECE_OUTLINE)
                        {
                            gfx.SmoothingMode = SmoothingMode.AntiAlias;
                            gfx.DrawPath(outlinePen, translatedFigure);
                        }
                    }

                    
                    Bitmap modifiedPiecePicture = (Bitmap)piecePicture.Clone();
                    ImageUtilities.EdgeDetectHorizontal(modifiedPiecePicture);
                    ImageUtilities.EdgeDetectVertical(modifiedPiecePicture);
                    piecePicture = ImageUtilities.AlphaBlendMatrix(modifiedPiecePicture, piecePicture, 200);

         

                    Piece piece = new Piece
                    {
                        ID = pieceID,
                        ClusterID = pieceID,
                        Width = figureLocation.Width,
                        Height = figureLocation.Height,
                        BoardLocation = translatedFigureLocation,
                        SourcePictureLocation = figureLocation,
                        MovableFigure = (GraphicsPath)translatedFigure.Clone(),
                        StaticFigure = (GraphicsPath)figure.Clone(),
                        Picture = (Bitmap)piecePicture.Clone(),
                        AdjacentPieceIDs = adjacentPieceIDs
                    };

                    PieceCluster cluster = new PieceCluster
                    {
                        ID = pieceID,
                        Width = figureLocation.Width,
                        Height = figureLocation.Height,
                        BoardLocation = translatedFigureLocation,
                        SourcePictureLocation = figureLocation,
                        MovableFigure = (GraphicsPath)translatedFigure.Clone(),
                        StaticFigure = (GraphicsPath)figure.Clone(),
                        Picture = (Bitmap)piecePicture.Clone(),
                        Pieces = new List<Piece> { piece }
                    };

        

                    _clusters.Add(cluster);

          
                   
                    pieceID++;
                }
            }

    

    

            Random random = new Random();

            int boardWidth = this.ClientSize.Width;
            int boardHeight = this.ClientSize.Height;

            foreach (PieceCluster cluster in _clusters)
            {
                int locationX = random.Next(1, boardWidth);
                int locationY = random.Next((menuStrip1.Height + 1), boardHeight);

     
                if ((locationX + cluster.Width) > boardWidth)
                {
                    locationX = locationX - ((locationX + cluster.Width) - boardWidth);
                }

                if ((locationY + cluster.Height) > boardHeight)
                {
                    locationY = locationY - ((locationY + cluster.Height) - boardHeight);
                }

         

                for (int index = 0; index < cluster.Pieces.Count; index++)
                {
                    Piece piece = cluster.Pieces[index];
                    piece.BoardLocation = new Rectangle(locationX, locationY, piece.Width, piece.Height);

                    matrix.Reset();
                    matrix.Translate(locationX, locationY);
                    piece.MovableFigure.Transform(matrix);
                }

             
                cluster.BoardLocation = new Rectangle(locationX, locationY, cluster.Width, cluster.Height);

                matrix.Reset();
                matrix.Translate(locationX, locationY);
                cluster.MovableFigure.Transform(matrix);
            }

         
        }
       
                     

        private ResponseMessage DisplayJigsawPuzzle(bool showGhostPicture)
        {




           



                     

            int boardWidth = this.ClientSize.Width;
            int boardHeight = this.ClientSize.Height;

            _board = new Bitmap(boardWidth, boardHeight);
            _backBuffer = new Bitmap(boardWidth, boardHeight);
            _background = new Bitmap(boardWidth, boardHeight);

      //    фон

           using (Graphics gfx = Graphics.FromImage(_background))
            {
                if (File.Exists(GameSettings.BACKGROUND_PICTURE_NAME))
                {
                   // SolidBrush sbr = new SolidBrush(color123);
                    Bitmap tileImage = new Bitmap("background_tile.bmp");
                    TextureBrush tileBrush = new TextureBrush(tileImage);

                    gfx.FillRectangle(tileBrush, 0, 0, _background.Width, _background.Height);
                }
                else
                {
                  //  string br = Convert.ToString(color123);
               

                    //  SolidBrush sbr = new SolidBrush(Color.FromName(fieldColor));
                   
                        SolidBrush sbr = new SolidBrush(Color.FromArgb(aaa,bbb,ccc));

                    gfx.FillRectangle(sbr, 0, 0, _background.Width, _background.Height);
                }
            }
            // тут к хуям всё крашиться на новых версиях на старых норм пашит без краша 
          /*  if (showGhostPicture)            
            {
                _background = ImageUtilities.AlphaBlendMatrix(_background, _sourcePicture, GameSettings.GHOST_PICTURE_ALPHA);
            }
            */
      

            using (Graphics gfx = Graphics.FromImage(_board))
            {                
                gfx.DrawImageUnscaled(_background, 0, 0);
                
                foreach (PieceCluster cluster in _clusters)
                {
                    gfx.ResetClip();
                    gfx.SetClip(cluster.MovableFigure);
                    gfx.DrawImage(cluster.Picture, cluster.BoardLocation);                    
                }
            }

            using (Graphics gfx = Graphics.FromImage(_backBuffer))
            {
                gfx.DrawImageUnscaled(_board, 0, 0);
            }

            using (Graphics gfx = this.CreateGraphics())
            {
                gfx.DrawImageUnscaled(_board, 0, 0);
            }

       

            return new ResponseMessage
            {
                Okay = true
            };  
        }
   
        private List<int> DetermineAdjacentPieceIDs(List<Coordinate> coords, int numColumns)
        {
            List<int> pieceIDs = new List<int>();
            
            foreach (Coordinate coord in coords)
            {
                if (coord.Y >= 0 && coord.Y < GameSettings.NUM_ROWS)
                {
                    if (coord.X >= 0 && coord.X < GameSettings.NUM_COLUMNS)
                    {
                        int pieceID = (coord.Y * numColumns) + coord.X;
                        pieceIDs.Add(pieceID);
                    }
                }
            }

            return pieceIDs;
        }

        private Piece GetPieceByID(int pieceID)
        {
            foreach (PieceCluster cluster in _clusters)
            {
                foreach (Piece piece in cluster.Pieces)
                {
                    if (piece.ID == pieceID)
                    {
                        return piece;
                    }
                }
            }

            return null;
        }

        private PieceCluster GetPieceClusterByID(int groupID)
        {
            foreach (PieceCluster group in _clusters)
            {
                if (group.ID == groupID)
                {
                    return group;
                }
            }

            return null;
        }

        private bool RemovePieceGroupByID(int groupID)
        {
            for (int i = 0; i < _clusters.Count; i++)
            {
                if (_clusters[i].ID == groupID)
                {                    
                    _clusters.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

      

                                    
    }
}
