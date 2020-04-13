using Android.Graphics;
using Xama.JTPorts.ShapedView.Managers;

namespace Xama.JTPorts.ShapedView.PathCreators
{
    public class CurCornerPathCreator : IClipPathCreator
    {
        float _topLeftCutSizePx, _topRightCutSizePx, _bottomRightCutSizePx, _bottomLeftCutSizePx;

        public CurCornerPathCreator(
            float topLeftCutSizePx, 
            float topRightCutSizePx, 
            float bottomRightCutSizePx, 
            float bottomLeftCutSizePx)
        {
            _topLeftCutSizePx = topLeftCutSizePx;
            _topRightCutSizePx = topRightCutSizePx;
            _bottomRightCutSizePx = bottomRightCutSizePx;
            _bottomLeftCutSizePx = bottomLeftCutSizePx;
        }

        public Path CreateClipPath(int width, int height)
        {
            var rectF = new RectF(0, 0, width, height);
            return GeneratePath(rectF, _topLeftCutSizePx, _topRightCutSizePx, _bottomRightCutSizePx, _bottomLeftCutSizePx);
        }

        public bool RequiresBitmap()
        {
            return false;
        }

        private Path GeneratePath(RectF rect, float topLeftDiameter, float topRightDiameter, float bottomRightDiameter, float bottomLeftDiameter)
        {
            Path path = new Path();

            topLeftDiameter = topLeftDiameter < 0 ? 0 : topLeftDiameter;
            topRightDiameter = topRightDiameter < 0 ? 0 : topRightDiameter;
            bottomLeftDiameter = bottomLeftDiameter < 0 ? 0 : bottomLeftDiameter;
            bottomRightDiameter = bottomRightDiameter < 0 ? 0 : bottomRightDiameter;

            path.MoveTo(rect.Left + topLeftDiameter, rect.Top);
            path.LineTo(rect.Right - topRightDiameter, rect.Top);
            path.LineTo(rect.Right, rect.Top + topRightDiameter);
            path.LineTo(rect.Right, rect.Bottom - bottomRightDiameter);
            path.LineTo(rect.Right - bottomRightDiameter, rect.Bottom);
            path.LineTo(rect.Left + bottomLeftDiameter, rect.Bottom);
            path.LineTo(rect.Left, rect.Bottom - bottomLeftDiameter);
            path.LineTo(rect.Left, rect.Top + topLeftDiameter);
            path.LineTo(rect.Left + topLeftDiameter, rect.Top);
            path.Close();

            return path;
        }
    }
}