using Android.Graphics;
using System;
using Xama.JTPorts.ShapedView.Models;

namespace Xama.JTPorts.ShapedView.PathCreators
{
    public class BubbleClipPathCreator : BasePathCreator
    {
        public float _topLeftDiameter;
        public float _topRightDiameter;
        public float _bottomRightDiameter;
        public float _bottomLeftDiameter;
        private float _positionPer;
        private float _arrowHeightPx;
        private float _arrowWidthPx;

        public BubbleClipPathCreator(ClipPosition clipPosition,
            CropDirection cropPosition,
            float heightPx, float arrowHeightPx, float arrowWidthPx,
            float topLeftDiameter, float topRightDiameter,
            float bottomRightDiameter, float bottomLeftDiameter) : base(clipPosition, cropPosition, heightPx)
        {
            this._topLeftDiameter = topLeftDiameter;
            this._topRightDiameter = topRightDiameter;
            this._bottomRightDiameter = bottomRightDiameter;
            this._bottomLeftDiameter = bottomLeftDiameter;
            this._arrowHeightPx = arrowHeightPx;
            this._arrowWidthPx = arrowWidthPx;
        }

        public override Path CreateClipPath(int width, int height)
        {
            RectF myRect = new RectF(0, 0, width, height);
            return DrawBubble(myRect, _topLeftDiameter, _topRightDiameter, _bottomRightDiameter, _bottomLeftDiameter);
        }

        public override bool RequiresBitmap()
        {
            throw new NotImplementedException();
        }

        private Path DrawBubble(RectF myRect, float topLeftDiameter, float topRightDiameter, float bottomRightDiameter, float bottomLeftDiameter)
        {
            Path path = new Path();

            topLeftDiameter = topLeftDiameter < 0 ? 0 : topLeftDiameter;
            topRightDiameter = topRightDiameter < 0 ? 0 : topRightDiameter;
            bottomLeftDiameter = bottomLeftDiameter < 0 ? 0 : bottomLeftDiameter;
            bottomRightDiameter = bottomRightDiameter < 0 ? 0 : bottomRightDiameter;

            float spacingLeft = this.ClipPosition == ClipPosition.Left ? _arrowHeightPx : 0;
            float spacingTop = this.ClipPosition == ClipPosition.Top ? _arrowHeightPx : 0;
            float spacingRight = this.ClipPosition == ClipPosition.Right ? _arrowHeightPx : 0;
            float spacingBottom = this.ClipPosition == ClipPosition.Bottom ? _arrowHeightPx : 0;

            float left = spacingLeft + myRect.Left;
            float top = spacingTop + myRect.Top;
            float right = myRect.Right - spacingRight;
            float bottom = myRect.Bottom - spacingBottom;

            float centerX = (myRect.Left + myRect.Right) * _positionPer;

            path.MoveTo(left + topLeftDiameter / 2f, top);
            //LEFT, TOP

            if (ClipPosition == ClipPosition.Top)
            {
                path.LineTo(centerX - _arrowWidthPx, top);
                path.LineTo(centerX, myRect.Top);
                path.LineTo(centerX + _arrowWidthPx, top);
            }

            path.LineTo(right - topRightDiameter / 2f, top);

            path.QuadTo(right, top, right, top + topRightDiameter / 2);
            //RIGHT, TOP

            if (ClipPosition == ClipPosition.Right)
            {
                path.LineTo(right, bottom - (bottom * (1 - _positionPer)) - _arrowWidthPx);
                path.LineTo(myRect.Right, bottom - (bottom * (1 - _positionPer)));
                path.LineTo(right, bottom - (bottom * (1 - _positionPer)) + _arrowWidthPx);
            }
            path.LineTo(right, bottom - bottomRightDiameter / 2);

            path.QuadTo(right, bottom, right - bottomRightDiameter / 2, bottom);
            //RIGHT, BOTTOM

            if (ClipPosition == ClipPosition.Bottom)
            {
                path.LineTo(centerX + _arrowWidthPx, bottom);
                path.LineTo(centerX, myRect.Bottom);
                path.LineTo(centerX - _arrowWidthPx, bottom);
            }
            path.LineTo(left + bottomLeftDiameter / 2, bottom);

            path.QuadTo(left, bottom, left, bottom - bottomLeftDiameter / 2);
            //LEFT, BOTTOM

            if (ClipPosition == ClipPosition.Left)
            {
                path.LineTo(left, bottom - (bottom * (1 - _positionPer)) + _arrowWidthPx);
                path.LineTo(myRect.Left, bottom - (bottom * (1 - _positionPer)));
                path.LineTo(left, bottom - (bottom * (1 - _positionPer)) - _arrowWidthPx);
            }
            path.LineTo(left, top + topLeftDiameter / 2);

            path.QuadTo(left, top, left + topLeftDiameter / 2, top);

            path.Close();

            return path;
        }
    }
}