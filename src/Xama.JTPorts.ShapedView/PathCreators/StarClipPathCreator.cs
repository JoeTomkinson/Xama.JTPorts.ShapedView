using Android.Graphics;
using Java.Lang;
using Xama.JTPorts.ShapedView.Managers;

namespace Xama.JTPorts.ShapedView.PathCreators
{
    public class StarClipPathCreator : Java.Lang.Object, IClipPathCreator
    {
        private float _numberOfPoints;

        public StarClipPathCreator(float numberOfPoints)
        {
            _numberOfPoints = numberOfPoints;
        }

        public Path CreateClipPath(int width, int height)
        {
            int vertices = (int)_numberOfPoints * 2;
            float alpha = (float)(2 * Math.Pi) / vertices;
            int radius = (height <= width ? height : width) / 2;
            float centerX = width / 2;
            float centerY = height / 2;

            Path path = new Path();
            for (int i = vertices + 1; i != 0; i--)
            {
                float r = radius * (i % 2 + 1) / 2;
                double omega = alpha * i;
                path.LineTo((float)(r * Math.Sin(omega)) + centerX, (float)(r * Math.Cos(omega)) + centerY);
            }
            path.Close();
            return path;
        }

        public bool RequiresBitmap()
        {
            return true;
        }
    }
}