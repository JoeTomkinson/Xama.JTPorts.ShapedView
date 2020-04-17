using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Xama.JTPorts.ShapedView.Interfaces;
using Xama.JTPorts.ShapedView.Models;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class ArcView : ViewShape, IClipPathCreator
    {
        private float heightPx;
        private ArcPosition clipPosition;
        private CropDirection cropDirection;

        public float HeightPx
        {
            get => heightPx;
            set { heightPx = value; RequiresShapeUpdate(); }
        }

        public float ArcHeightDp
        {
            get => PxToDp(HeightPx);
            set => HeightPx = PxToDp(ArcHeightDp);
        }

        public ArcPosition ClipPosition
        {
            get => clipPosition;
            set { clipPosition = value; RequiresShapeUpdate(); }
        }

        public CropDirection CropDirection
        {
            get => HeightPx > 0 ? CropDirection.Outside : CropDirection.Inside;
            set => cropDirection = value;
        }

        public ArcView(Context context) : base(context)
        {
            Init(context, null);
        }

        public ArcView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public ArcView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        private void Init(Context context, IAttributeSet attrs)
        {
            HeightPx = 0f;

            if (attrs != null)
            {
                TypedArray attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.ArcView);
                HeightPx = attributes.GetDimensionPixelSize(Resource.Styleable.ArcView_shape_arc_height, (int)HeightPx);
                ClipPosition = (ArcPosition)attributes.GetInteger(Resource.Styleable.ArcView_shape_arc_position, (int)ClipPosition);
                attributes.Recycle();
            }
            SetClipPathCreator(this);
        }

        public Path CreateClipPath(int width, int height)
        {
            Path path = new Path();
            bool isCropInside = CropDirection == CropDirection.Inside;
            float arcHeightAbs = Java.Lang.Math.Abs(HeightPx);

            switch (clipPosition)
            {
                case ArcPosition.Bottom:
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
                case ArcPosition.Top:
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
                case ArcPosition.Left:
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
                case ArcPosition.Right:
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
            }

            return path;
        }

        public bool RequiresBitmap()
        {
            return false;
        }
    }
}