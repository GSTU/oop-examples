using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MinThantSin.OpenSourceGames
{
    /// <summary>
    ///Предстовляет часть мозайки
    /// </summary>
    public class Piece
    {
        /// <summary>
        /// ид части
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// ид кластера части 
        /// </summary>
        public int ClusterID { get; set; }

        /// <summary>
        /// определяет какие части соеденить и соединяет 
        /// </summary>
        public List<int> AdjacentPieceIDs { get; set; }

      
        public int Width { get; set; }

      
        public int Height { get; set; }

        /// <summary>
        /// Расположение части мазайки на поле 
        /// </summary>
        public Rectangle BoardLocation { get; set; }

        /// <summary>
        /// расположение изначальной картинки
        /// </summary>
        public Rectangle SourcePictureLocation { get; set; }

        /// <summary>
        /// фигура которая перемещается по доске (кусок или собраные уже части )
        /// </summary>
        public GraphicsPath MovableFigure { get; set; }
        public GraphicsPath StaticFigure { get; set; }

        /// <summary>
        /// хранилище для картники после построения 
        /// </summary>
        public Bitmap Picture { get; set; }                          
    }
}
