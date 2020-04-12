using Android.Graphics;
using Java.Lang;
using Xama.JTPorts.ShapedView.Models;

namespace Xama.JTPorts.ShapedView.PathCreators
{
    public class CircleClipPathCreator : BaseClipPathCreator
    {
        public CircleClipPathCreator(BasePosition clipPosition, CropDirection cropPosition, float heightPx) : base(clipPosition, cropPosition, heightPx)
        {
        }

        public override Path CreateClipPath(int width, int height)
        {
            Path path = new Path();
            path.AddCircle(width / 2f, height / 2f, Math.Min(width / 2f, height / 2f), Path.Direction.Cw);
            return path;
        }

        public override bool RequiresBitmap()
        {
            return false;
        }
    }
}