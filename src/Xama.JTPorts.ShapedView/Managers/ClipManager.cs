using Android.Graphics;

namespace Xama.JTPorts.ShapedView.Managers
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