using Android.Graphics;

namespace Xama.JTPorts.ShapedView.Interfaces
{
    public interface IClipManager
    {
        Path CreateMask(int width, int height);
        Path GetShadowConvexPath();
        void SetupClipLayout(int width, int height);
        Paint GetPaint();
        bool RequiresBitmap();
    }
}