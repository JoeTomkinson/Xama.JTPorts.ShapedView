using Android.Graphics;

namespace Xama.JTPorts.ShapedView.Managers
{
    public interface IClipManager
    {
        Path CreateMask(int width, int height);
#nullable enable
        Path? GetShadowConvexPath();
#nullable disable
        void SetupClipLayout(int width, int height);
        Paint GetPaint();
        bool RequiresBitmap();
    }
}