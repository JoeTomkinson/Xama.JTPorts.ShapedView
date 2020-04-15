using Android.Graphics;
using Java.Lang;
using Xama.JTPorts.ShapedView.Managers;

namespace Xama.JTPorts.ShapedView.PathCreators
{
    public class RoundedRectClipPathCreator : Java.Lang.Object, IClipPathCreator
    {
        private RectF rectF = new RectF();
        float _topLeftRadius, _topRightRadius, _bottomRightRadius, _bottomLeftRadius;

        public RoundedRectClipPathCreator(float topLeftRadius, float topRightRadius, float bottomRightRadius, float bottomLeftRadius)
        {
            _topLeftRadius = topLeftRadius;
            _topRightRadius = topRightRadius;
            _bottomRightRadius = bottomRightRadius;
            _bottomLeftRadius = bottomLeftRadius;
        }

        public Path CreateClipPath(int width, int height)
        {
            rectF.Set(0, 0, width, height);
            return GeneratePath(false, rectF,
                    LimitSize(_topLeftRadius, width, height),
                    LimitSize(_topRightRadius, width, height),
                    LimitSize(_bottomRightRadius, width, height),
                    LimitSize(_bottomLeftRadius, width, height));
        }

        public bool RequiresBitmap()
        {
            return false;
        }

        protected float LimitSize(float from, float width, float height)
        {
            return Math.Min(from, Math.Min(width, height));
        }

        private Path GeneratePath(bool useBezier, RectF rect, float topLeftRadius, float topRightRadius, float bottomRightRadius, float bottomLeftRadius)
        {
            Path path = new Path();

            float left = rect.Left;
            float top = rect.Top;
            float bottom = rect.Bottom;
            float right = rect.Right;

            float maxSize = Math.Min(rect.Width() / 2f, rect.Height() / 2f);

            float topLeftRadiusAbs = Math.Abs(topLeftRadius);
            float topRightRadiusAbs = Math.Abs(topRightRadius);
            float bottomLeftRadiusAbs = Math.Abs(bottomLeftRadius);
            float bottomRightRadiusAbs = Math.Abs(bottomRightRadius);

            if (topLeftRadiusAbs > maxSize)
            {
                topLeftRadiusAbs = maxSize;
            }
            if (topRightRadiusAbs > maxSize)
            {
                topRightRadiusAbs = maxSize;
            }
            if (bottomLeftRadiusAbs > maxSize)
            {
                bottomLeftRadiusAbs = maxSize;
            }
            if (bottomRightRadiusAbs > maxSize)
            {
                bottomRightRadiusAbs = maxSize;
            }

            path.MoveTo(left + topLeftRadiusAbs, top);
            path.LineTo(right - topRightRadiusAbs, top);

            //float left, float top, float right, float bottom, float startAngle, float sweepAngle, boolean forceMoveTo
            if (useBezier)
            {
                path.QuadTo(right, top, right, top + topRightRadiusAbs);
            }
            else
            {
                float arc = topRightRadius > 0 ? 90 : -270;
                path.ArcTo(new RectF(right - topRightRadiusAbs * 2f, top, right, top + topRightRadiusAbs * 2f), -90, arc);
            }
            path.LineTo(right, bottom - bottomRightRadiusAbs);
            if (useBezier)
            {
                path.QuadTo(right, bottom, right - bottomRightRadiusAbs, bottom);
            }
            else
            {
                float arc = bottomRightRadiusAbs > 0 ? 90 : -270;
                path.ArcTo(new RectF(right - bottomRightRadiusAbs * 2f, bottom - bottomRightRadiusAbs * 2f, right, bottom), 0, arc);
            }
            path.LineTo(left + bottomLeftRadiusAbs, bottom);
            if (useBezier)
            {
                path.QuadTo(left, bottom, left, bottom - bottomLeftRadiusAbs);
            }
            else
            {
                float arc = bottomLeftRadiusAbs > 0 ? 90 : -270;
                path.ArcTo(new RectF(left, bottom - bottomLeftRadiusAbs * 2f, left + bottomLeftRadiusAbs * 2f, bottom), 90, arc);
            }
            path.LineTo(left, top + topLeftRadiusAbs);
            if (useBezier)
            {
                path.QuadTo(left, top, left + topLeftRadiusAbs, top);
            }
            else
            {
                float arc = topLeftRadiusAbs > 0 ? 90 : -270;
                path.ArcTo(new RectF(left, top, left + topLeftRadiusAbs * 2f, top + topLeftRadiusAbs * 2f), 180, arc);
            }
            path.Close();

            return path;
        }
    }
}