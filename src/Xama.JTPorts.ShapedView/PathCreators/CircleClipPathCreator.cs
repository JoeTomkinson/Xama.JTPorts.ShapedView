using Android.Graphics;
using Java.Lang;
using Xama.JTPorts.ShapedView.Managers;

namespace Xama.JTPorts.ShapedView.PathCreators
{
    public class CircleClipPathCreator : IClipPathCreator
    {
        public CircleClipPathCreator() 
        {
        }

        public Path CreateClipPath(int width, int height)
        {
            Path path = new Path();
            path.AddCircle(width / 2f, height / 2f, Math.Min(width / 2f, height / 2f), Path.Direction.Cw);
            return path;
        }

        public bool RequiresBitmap()
        {
            return false;
        }
    }
}