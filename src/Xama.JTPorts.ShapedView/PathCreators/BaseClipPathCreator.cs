using Android.Graphics;
using Xama.JTPorts.ShapedView.Managers;
using Xama.JTPorts.ShapedView.Models;

namespace Xama.JTPorts.ShapedView.PathCreators
{
    public abstract class BaseClipPathCreator : IClipPathCreator
    {
        public CropDirection CropPosition { get; private set; }

        public BasePosition ClipPosition { get; private set; }

        public float HeightPx { get; private set; }

        public BaseClipPathCreator(BasePosition clipPosition, CropDirection cropPosition, float heightPx)
        {
            CropPosition = cropPosition;
            ClipPosition = clipPosition;
            HeightPx = heightPx;
        }

        public abstract Path CreateClipPath(int width, int height);

        public abstract bool RequiresBitmap();
    }
}