using Android.Graphics;

namespace Xama.JTPorts.ShapedView.Interfaces
{
    public interface IClipPathCreator
    {
        Path CreateClipPath(int width, int height);
        bool RequiresBitmap();
    }
}