using System;
using System.Drawing;

namespace MinThantSin.OpenSourceGames
{
    public class BezierCurve
    {
        // Позиции x i y измеряются от левого нижнего 
        private const double X2RATIO = 0;//0.760869565217391;
        private const double Y2RATIO = 0;//0.183946488294314;

        private const double X3RATIO = 0;//0.0802675585284281;
        private const double Y3RATIO = 0;//0.150501672240803;

        private const double X4RATIO = 0;
        private const double Y4RATIO = Y2RATIO;

        private const double X6RATIO = X2RATIO;
        private const double Y6RATIO = Y2RATIO;

        private const double X5RATIO = X3RATIO;
        private const double Y5RATIO = Y3RATIO;

        private const double CURVE_TENSION = 1.2;

        public Point[] Points { get; set; }

        public static BezierCurve CreateHorizontal(int length)
        {
            double curvature = length * CURVE_TENSION;

            int x1, y1;
            int x2, y2;
            int x3, y3;
            int x4, y4;
            int x5, y5;
            int x6, y6;
            int x7, y7;


            x1 = 0;
            y1 = 0;

            x2 = x1 + (int)(length * X2RATIO);
            y2 = y1 + (int)(curvature * Y2RATIO);

            x3 = x1 + (int)(length * X3RATIO);
            y3 = y1 - (int)(curvature * Y3RATIO);

            x4 = x1 + (int)(length * X4RATIO);
            y4 = y1 - (int)(curvature * Y4RATIO);


            x7 = x1 + length;
            y7 = y1;

            x6 = x7 - (int)(length * X6RATIO);
            y6 = y7 + (int)(curvature * Y6RATIO);

            x5 = x7 - (int)(length * X5RATIO);
            y5 = y7 - (int)(curvature * Y5RATIO);

            BezierCurve curve = new BezierCurve()
            {
                Points = new Point[] 
                {
                    new Point(x1, y1), 
                    new Point(x2, y2),
                    new Point(x3, y3), 
                    new Point(x4, y4),
                    new Point(x5, y5), 
                    new Point(x6, y6),
                    new Point(x7, y7)
                }
            };

            return curve;
        }

        public static BezierCurve CreateVertical(int length)
        {
            BezierCurve curve = CreateHorizontal(length);
            curve.Rotate(90);

            int offsetX = 0 - curve.Points[0].X;
            int offsetY = 0 - curve.Points[0].Y;

            return curve.Translate(offsetX, offsetY);
        }

        public BezierCurve Translate(int transX, int transY)
        {
            for (int i = 0; i < this.Points.Length; i++)
            {
                this.Points[i].X += transX;
                this.Points[i].Y += transY;
            }

            return this;
        }

        public BezierCurve FlipHorizontal()
        {
            for (int i = 0; i < this.Points.Length; i++)
            {
                this.Points[i].X *= -1;
            }

            return this;
        }

        public BezierCurve FlipVertical()
        {
            for (int i = 0; i < this.Points.Length; i++)
            {
                this.Points[i].Y *= -1;
            }

            return this;
        }

        public BezierCurve Rotate(int degrees)
        {
            double radians = 6.283185308 / (360 / degrees);
            double cosine = Math.Cos(radians);
            double sine = Math.Sin(radians);

            for (int i = 0; i < this.Points.Length; i++)
            {
                int rotatedX = (int)(this.Points[i].X * cosine - this.Points[i].Y * sine);
                int rotatedY = (int)(this.Points[i].Y * cosine + this.Points[i].X * sine);

                this.Points[i].X = rotatedX;
                this.Points[i].Y = rotatedY;
            }

            return this;
        }
    }
}
