using Android.Graphics;
using Java.Lang;
using Xama.JTPorts.ShapedView.Managers;
using Xama.JTPorts.ShapedView.Models;

namespace Xama.JTPorts.ShapedView.PathCreators
{
    public class DiagonalClipPathCreator : Java.Lang.Object, IClipPathCreator
    {
        private DiagonalDirection _diagonalDirection;
        private DiagonalPosition _diagonalPosition;
        private float _diagonalAngle;
        private float _paddingLeft;
        private float _paddingRight;
        private float _paddingTop;
        private float _paddingBottom;

        public DiagonalClipPathCreator(
            int diagonalDirection,
            int diagonalPosition,
            float diagonalAngle,
            float paddingLeft,
             float paddingRight,
             float paddingTop,
             float paddingBottom
            )
        {
            _diagonalDirection = (DiagonalDirection)diagonalDirection;
            _diagonalPosition = (DiagonalPosition)diagonalPosition;
            _diagonalAngle = diagonalAngle;
            _paddingLeft = paddingLeft;
            _paddingRight = paddingRight;
            _paddingTop = paddingTop;
            _paddingBottom = paddingBottom;
        }

        public Path CreateClipPath(int width, int height)
        {
            Path path = new Path();

            float diagonalAngleAbs = Math.Abs(_diagonalAngle);
            bool isDirectionLeft = _diagonalDirection == DiagonalDirection.Left;
            float perpendicularHeight = (float)(width * Math.Tan(Math.ToRadians(diagonalAngleAbs)));

            switch (_diagonalPosition)
            {
                case DiagonalPosition.Bottom:
                    if (isDirectionLeft)
                    {
                        path.MoveTo(_paddingLeft, _paddingRight);
                        path.LineTo(width - _paddingRight, _paddingTop);
                        path.LineTo(width - _paddingRight, height - perpendicularHeight - _paddingBottom);
                        path.LineTo(_paddingLeft, height - _paddingBottom);
                        path.Close();
                    }
                    else
                    {
                        path.MoveTo(width - _paddingRight, height - _paddingBottom);
                        path.LineTo(_paddingLeft, height - perpendicularHeight - _paddingBottom);
                        path.LineTo(_paddingLeft, _paddingTop);
                        path.LineTo(width - _paddingRight, _paddingTop);
                        path.Close();
                    }
                    break;
                case DiagonalPosition.Top:
                    if (isDirectionLeft)
                    {
                        path.MoveTo(width - _paddingRight, height - _paddingBottom);
                        path.LineTo(width - _paddingRight, _paddingTop + perpendicularHeight);
                        path.LineTo(_paddingLeft, _paddingTop);
                        path.LineTo(_paddingLeft, height - _paddingBottom);
                        path.Close();
                    }
                    else
                    {
                        path.MoveTo(width - _paddingRight, height - _paddingBottom);
                        path.LineTo(width - _paddingRight, _paddingTop);
                        path.LineTo(_paddingLeft, _paddingTop + perpendicularHeight);
                        path.LineTo(_paddingLeft, height - _paddingBottom);
                        path.Close();
                    }
                    break;
                case DiagonalPosition.Right:
                    if (isDirectionLeft)
                    {
                        path.MoveTo(_paddingLeft, _paddingTop);
                        path.LineTo(width - _paddingRight, _paddingTop);
                        path.LineTo(width - _paddingRight - perpendicularHeight, height - _paddingBottom);
                        path.LineTo(_paddingLeft, height - _paddingBottom);
                        path.Close();
                    }
                    else
                    {
                        path.MoveTo(_paddingLeft, _paddingTop);
                        path.LineTo(width - _paddingRight - perpendicularHeight, _paddingTop);
                        path.LineTo(width - _paddingRight, height - _paddingBottom);
                        path.LineTo(_paddingLeft, height - _paddingBottom);
                        path.Close();
                    }
                    break;
                case DiagonalPosition.Left:
                    if (isDirectionLeft)
                    {
                        path.MoveTo(_paddingLeft + perpendicularHeight, _paddingTop);
                        path.LineTo(width - _paddingRight, _paddingTop);
                        path.LineTo(width - _paddingRight, height - _paddingBottom);
                        path.LineTo(_paddingLeft, height - _paddingBottom);
                        path.Close();
                    }
                    else
                    {
                        path.MoveTo(_paddingLeft, _paddingTop);
                        path.LineTo(width - _paddingRight, _paddingTop);
                        path.LineTo(width - _paddingRight, height - _paddingBottom);
                        path.LineTo(_paddingLeft + perpendicularHeight, height - _paddingBottom);
                        path.Close();
                    }
                    break;
            }
            return path;
        }

        public bool RequiresBitmap()
        {
            return false;
        }
    }
}