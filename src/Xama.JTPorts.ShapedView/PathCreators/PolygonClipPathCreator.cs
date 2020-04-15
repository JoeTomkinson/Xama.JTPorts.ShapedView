using Android.Graphics;
using Java.Lang;
using Xama.JTPorts.ShapedView.Managers;

namespace Xama.JTPorts.ShapedView.PathCreators
{
    public class PolygonClipPathCreator : Java.Lang.Object, IClipPathCreator
    {
        private float _numberOfSides;

        public PolygonClipPathCreator(float numberOfSides)
        {
            _numberOfSides = numberOfSides;
        }

        public Path CreateClipPath(int width, int height)
        {
            float section = (float)(2.0 * Math.Pi / _numberOfSides);
            int polygonSize = Math.Min(width, height);
            int radius = polygonSize / 2;
            int centerX = width / 2;
            int centerY = height / 2;

            Path polygonPath = new Path();
            polygonPath.MoveTo((centerX + radius * (float)Math.Cos(0)), (centerY + radius * (float)Math.Sin(0)));

            for (int i = 1; i < _numberOfSides; i++)
            {
                polygonPath.LineTo((centerX + radius * (float)Math.Cos(section * i)),
                        (centerY + radius * (float)Math.Sin(section * i)));
            }

            polygonPath.Close();
            return polygonPath;
        }

        public bool RequiresBitmap()
        {
            return true;
        }
    }
}