using Android.Graphics;
using System;
using Xama.JTPorts.ShapedView.Managers;
using Xama.JTPorts.ShapedView.Models;

namespace Xama.JTPorts.ShapedView.PathCreators
{
    public class BubbleClipPathCreator : Java.Lang.Object, IClipPathCreator
    {
        public float _topLeftDiameter;
        public float _topRightDiameter;
        public float _bottomRightDiameter;
        public float _bottomLeftDiameter;
        private float _positionPer;
        private float _arrowHeightPx;
        private float _arrowWidthPx;
        private BubblePosition _clipPosition;

        public BubbleClipPathCreator(
            BubblePosition clipPosition,
            float heightPx, float arrowHeightPx, float arrowWidthPx,
            float topLeftDiameter, float topRightDiameter,
            float bottomRightDiameter, float bottomLeftDiameter, float positionPer)
        {
            this._clipPosition = clipPosition;
            this._topLeftDiameter = topLeftDiameter;
            this._topRightDiameter = topRightDiameter;
            this._bottomRightDiameter = bottomRightDiameter;
            this._bottomLeftDiameter = bottomLeftDiameter;
            this._arrowHeightPx = arrowHeightPx;
            this._arrowWidthPx = arrowWidthPx;
            this._positionPer = positionPer;
        }

        public Path CreateClipPath(int width, int height)
        {
            RectF myRect = new RectF(0, 0, width, height);
            return DrawBubble(myRect, _topLeftDiameter, _topRightDiameter, _bottomRightDiameter, _bottomLeftDiameter);
        }

        public bool RequiresBitmap()
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

            float spacingLeft = this._clipPosition == BubblePosition.Left ? _arrowHeightPx : 0;
            float spacingTop = this._clipPosition == BubblePosition.Top ? _arrowHeightPx : 0;
            float spacingRight = this._clipPosition == BubblePosition.Right ? _arrowHeightPx : 0;
            float spacingBottom = this._clipPosition == BubblePosition.Bottom ? _arrowHeightPx : 0;

            float left = spacingLeft + myRect.Left;
            float top = spacingTop + myRect.Top;
            float right = myRect.Right - spacingRight;
            float bottom = myRect.Bottom - spacingBottom;

            float centerX = (myRect.Left + myRect.Right) * _positionPer;

            path.MoveTo(left + topLeftDiameter / 2f, top);
            //LEFT, TOP

            if (_clipPosition == BubblePosition.Top)
            {
                path.LineTo(centerX - _arrowWidthPx, top);
                path.LineTo(centerX, myRect.Top);
                path.LineTo(centerX + _arrowWidthPx, top);
            }

            path.LineTo(right - topRightDiameter / 2f, top);

            path.QuadTo(right, top, right, top + topRightDiameter / 2);
            //RIGHT, TOP

            if (_clipPosition == BubblePosition.Right)
            {
                path.LineTo(right, bottom - (bottom * (1 - _positionPer)) - _arrowWidthPx);
                path.LineTo(myRect.Right, bottom - (bottom * (1 - _positionPer)));
                path.LineTo(right, bottom - (bottom * (1 - _positionPer)) + _arrowWidthPx);
            }
            path.LineTo(right, bottom - bottomRightDiameter / 2);

            path.QuadTo(right, bottom, right - bottomRightDiameter / 2, bottom);
            //RIGHT, BOTTOM

            if (_clipPosition == BubblePosition.Bottom)
            {
                path.LineTo(centerX + _arrowWidthPx, bottom);
                path.LineTo(centerX, myRect.Bottom);
                path.LineTo(centerX - _arrowWidthPx, bottom);
            }
            path.LineTo(left + bottomLeftDiameter / 2, bottom);

            path.QuadTo(left, bottom, left, bottom - bottomLeftDiameter / 2);
            //LEFT, BOTTOM

            if (_clipPosition == BubblePosition.Left)
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