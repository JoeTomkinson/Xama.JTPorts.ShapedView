using Android.Graphics;
using Xama.JTPorts.ShapedView.Managers;

namespace Xama.JTPorts.ShapedView.PathCreators
{
    public class TriangleClipPathCreator : IClipPathCreator
    {
        private float _percentBottom;
        private float _percentLeft;
        private float _percentRight;

        public TriangleClipPathCreator(float percentBottom, float percentLeft, float percentRight)
        {
            _percentBottom = percentBottom;
            _percentLeft = percentLeft;
            _percentRight = percentRight;
        }

        public Path CreateClipPath(int width, int height)
        {
            Path path = new Path();
            path.MoveTo(0, _percentLeft * height);
            path.LineTo(_percentBottom * width, height);
            path.LineTo(width, _percentRight * height);
            path.Close();

            return path;
        }

        public bool RequiresBitmap()
        {
            return false;
        }
    }
}