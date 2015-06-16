using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MinThantSin.OpenSourceGames
{
    /// <summary>
    /// Предтовляет часть мазайки или обьединёные части
    /// </summary>
    public class PieceCluster
    {
        public int ID { get; set; }


        // Свойства теже но могут изменять если части собраны
        public int Width { get; set; }
        public int Height { get; set; }
        public Rectangle BoardLocation { get; set; }
        public Rectangle SourcePictureLocation { get; set; }
        public GraphicsPath MovableFigure { get; set; }
        public GraphicsPath StaticFigure { get; set; }
        public Bitmap Picture { get; set; }
        public List<Piece> Pieces { get; set; }
    }
}
