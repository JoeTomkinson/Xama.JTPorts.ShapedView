using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xama.JTPorts.ShapedView.Models;

namespace Xama.JTPorts.ShapedView.PathCreators
{
    public class DottedEdgeClipPathCreator : BaseClipPathCreator
    {
        private RectF _rectF = new RectF();

        private float _topLeftCutSize = 0f;
        private float _topRightCutSize = 0f;
        private float _bottomRightCutSize = 0f;
        private float _bottomLeftCutSize = 0f;
        private BasePosition _dotEdgePosition;
        private float _dotRadius = 0f;
        private float _dotSpacing = 0f;

        public DottedEdgeClipPathCreator(
            BasePosition clipPosition,
            CropDirection cropPosition,
            float heightPx, 
            float topLeftCutSize,
            float topRightCutSize,
            float bottomRightCutSize,
            float bottomLeftCutSize,
            BasePosition dotEdgePosition,
            float dotRadius,
            float dotSpacing
            ) : base(clipPosition, cropPosition, heightPx)
        {
            _topLeftCutSize = topLeftCutSize;
            _topRightCutSize = topRightCutSize;
            _bottomRightCutSize = bottomRightCutSize;
            _bottomLeftCutSize = bottomLeftCutSize;
            _dotEdgePosition = dotEdgePosition;
            _dotRadius = dotRadius;
            _dotSpacing = dotSpacing;
        }

        public override Path CreateClipPath(int width, int height)
        {
            _rectF.Set(0, 0, width, height);
            return generatePath(_rectF, _topLeftCutSize, _topRightCutSize, _bottomRightCutSize, _bottomLeftCutSize);
        }

        public override bool RequiresBitmap()
        {
            return false;
        }

        private Path generatePath(RectF rect, float topLeftDiameter, float topRightDiameter, float bottomRightDiameter, float bottomLeftDiameter)
        {
            Path path = new Path();

            topLeftDiameter = topLeftDiameter < 0 ? 0 : topLeftDiameter;
            topRightDiameter = topRightDiameter < 0 ? 0 : topRightDiameter;
            bottomLeftDiameter = bottomLeftDiameter < 0 ? 0 : bottomLeftDiameter;
            bottomRightDiameter = bottomRightDiameter < 0 ? 0 : bottomRightDiameter;

            path.MoveTo(rect.Left + topLeftDiameter, rect.Top);
            if (containsFlag(BasePosition.Top))
            {
                int count = 1;
                int x = (int)(rect.Left + topLeftDiameter + _dotSpacing * count + _dotRadius * 2 * (count - 1));
                while (x + _dotSpacing + _dotRadius * 2 <= rect.Right - topRightDiameter)
                {
                    x = (int)(rect.Left + topLeftDiameter + _dotSpacing * count + _dotRadius * 2 * (count - 1));
                    path.LineTo(x, rect.Top);
                    path.QuadTo(x + _dotRadius, rect.Top + _dotRadius, x + _dotRadius * 2, rect.Top);
                    count++;
                }
                path.LineTo(rect.Right - topRightDiameter, rect.Top);
            }
            else
            {
                path.LineTo(rect.Right - topRightDiameter, rect.Top);
            }

            path.LineTo(rect.Right, rect.Top + topRightDiameter);
            if (containsFlag(BasePosition.Right))
            {
                //drawing dots starts from the bottom just like the LEFT side so that when using two
                //widgets side by side, their dots positions will match each other
                path.LineTo(rect.Right - _dotRadius, rect.Top + topRightDiameter);
                path.LineTo(rect.Right - _dotRadius, rect.Bottom - bottomRightDiameter);
                path.LineTo(rect.Right, rect.Bottom - bottomRightDiameter);

                int count = 1;
                int y = (int)(rect.Bottom - bottomRightDiameter - _dotSpacing * count - _dotRadius * 2 * (count - 1));
                while (y - _dotSpacing - _dotRadius * 2 >= rect.Top + topRightDiameter)
                {
                    y = (int)(rect.Bottom - bottomRightDiameter - _dotSpacing * count - _dotRadius * 2 * (count - 1));
                    path.LineTo(rect.Right, y);
                    path.QuadTo(rect.Right - _dotRadius, y - _dotRadius, rect.Right, y - _dotRadius * 2);
                    count++;
                }
                path.LineTo(rect.Right, rect.Top + topRightDiameter);
                path.LineTo(rect.Right - _dotRadius, rect.Top + topRightDiameter);
                path.LineTo(rect.Right - _dotRadius, rect.Bottom - bottomRightDiameter);
                path.LineTo(rect.Right, rect.Bottom - bottomRightDiameter);
            }
            else
            {
                path.LineTo(rect.Right, rect.Bottom - bottomRightDiameter);
            }

            path.LineTo(rect.Right - bottomRightDiameter, rect.Bottom);
            if (containsFlag(BasePosition.Bottom))
            {
                int count = 1;
                int x = (int)(rect.Right - bottomRightDiameter - _dotSpacing * count - _dotRadius * 2 * (count - 1));
                while (x - _dotSpacing - _dotRadius * 2 >= rect.Left + bottomLeftDiameter)
                {
                    x = (int)(rect.Right - bottomRightDiameter - _dotSpacing * count - _dotRadius * 2 * (count - 1));
                    path.LineTo(x, rect.Bottom);
                    path.QuadTo(x - _dotRadius, rect.Bottom - _dotRadius, x - _dotRadius * 2, rect.Bottom);
                    count++;
                }
                path.LineTo(rect.Left + bottomLeftDiameter, rect.Bottom);
            }
            else
            {
                path.LineTo(rect.Left + bottomLeftDiameter, rect.Bottom);
            }

            path.LineTo(rect.Left, rect.Bottom - bottomLeftDiameter);
            if (containsFlag(BasePosition.Left))
            {
                int count = 1;
                int y = (int)(rect.Bottom - bottomLeftDiameter - _dotSpacing * count - _dotRadius * 2 * (count - 1));
                while (y - _dotSpacing - _dotRadius * 2 >= rect.Top + topLeftDiameter)
                {
                    y = (int)(rect.Bottom - bottomLeftDiameter - _dotSpacing * count - _dotRadius * 2 * (count - 1));
                    path.LineTo(rect.Left, y);
                    path.QuadTo(rect.Left + _dotRadius, y - _dotRadius, rect.Left, y - _dotRadius * 2);
                    count++;
                }
                path.LineTo(rect.Left, rect.Top + topLeftDiameter);
            }
            else
            {
                path.LineTo(rect.Left, rect.Top + topLeftDiameter);
            }
            path.LineTo(rect.Left + topLeftDiameter, rect.Top);
            path.Close();

            return path;
        }

        private bool containsFlag(BasePosition positionFlag)
        {
            return (_dotEdgePosition | positionFlag) == _dotEdgePosition;
        }
    }
}