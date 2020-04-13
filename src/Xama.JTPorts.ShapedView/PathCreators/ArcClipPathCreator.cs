using Android.Graphics;
using Java.Lang;
using Xama.JTPorts.ShapedView.Managers;
using Xama.JTPorts.ShapedView.Models;

namespace Xama.JTPorts.ShapedView.PathCreators
{
    public class ArcClipPathCreator : IClipPathCreator
    {
        private CropDirection _cropPosition;
        private BasePosition _clipPosition;
        private float _heightPx;

        public ArcClipPathCreator(BasePosition clipPosition, CropDirection cropPosition, float heightPx)
        {
            _cropPosition = cropPosition;
            _clipPosition = clipPosition;
            _heightPx = heightPx;
        }

        public Path CreateClipPath(int width, int height)
        {
            Path path = new Path();

            bool isCropInside = _cropPosition == CropDirection.Inside;

            float arcHeightAbs = Math.Abs(_heightPx);

            switch (_clipPosition)
            {
                case  BasePosition.Bottom:
                    {
                        if (isCropInside)
                        {
                            path.MoveTo(0, 0);
                            path.LineTo(0, height);
                            path.QuadTo(width / 2, height - 2 * arcHeightAbs, width, height);
                            path.LineTo(width, 0);
                            path.Close();
                        }
                        else
                        {
                            path.MoveTo(0, 0);
                            path.LineTo(0, height - arcHeightAbs);
                            path.QuadTo(width / 2, height + arcHeightAbs, width, height - arcHeightAbs);
                            path.LineTo(width, 0);
                            path.Close();
                        }
                        break;
                    }
                case  BasePosition.Top:
                    if (isCropInside)
                    {
                        path.MoveTo(0, height);
                        path.LineTo(0, 0);
                        path.QuadTo(width / 2, 2 * arcHeightAbs, width, 0);
                        path.LineTo(width, height);
                        path.Close();
                    }
                    else
                    {
                        path.MoveTo(0, arcHeightAbs);
                        path.QuadTo(width / 2, -arcHeightAbs, width, arcHeightAbs);
                        path.LineTo(width, height);
                        path.LineTo(0, height);
                        path.Close();
                    }
                    break;
                case  BasePosition.Left:
                    if (isCropInside)
                    {
                        path.MoveTo(width, 0);
                        path.LineTo(0, 0);
                        path.QuadTo(arcHeightAbs * 2, height / 2, 0, height);
                        path.LineTo(width, height);
                        path.Close();
                    }
                    else
                    {
                        path.MoveTo(width, 0);
                        path.LineTo(arcHeightAbs, 0);
                        path.QuadTo(-arcHeightAbs, height / 2, arcHeightAbs, height);
                        path.LineTo(width, height);
                        path.Close();
                    }
                    break;
                case  BasePosition.Right:
                    if (isCropInside)
                    {
                        path.MoveTo(0, 0);
                        path.LineTo(width, 0);
                        path.QuadTo(width - arcHeightAbs * 2, height / 2, width, height);
                        path.LineTo(0, height);
                        path.Close();
                    }
                    else
                    {
                        path.MoveTo(0, 0);
                        path.LineTo(width - arcHeightAbs, 0);
                        path.QuadTo(width + arcHeightAbs, height / 2, width - arcHeightAbs, height);
                        path.LineTo(0, height);
                        path.Close();
                    }
                    break;
                default:
                    return default;
            }

            return path;
        }

        public bool RequiresBitmap()
        {
            return false;
        }
    }
}