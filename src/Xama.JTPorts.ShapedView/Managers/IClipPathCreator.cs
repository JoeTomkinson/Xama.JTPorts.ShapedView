using Android.Graphics;

namespace Xama.JTPorts.ShapedView.Managers
{
    public interface IClipPathCreator
    {
        Path CreateClipPath(int width, int height);
        bool RequiresBitmap();
    }
}