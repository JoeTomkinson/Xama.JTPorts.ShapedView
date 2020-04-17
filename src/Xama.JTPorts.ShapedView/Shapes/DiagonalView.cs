using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using System;
using Xama.JTPorts.ShapedView.Interfaces;
using Xama.JTPorts.ShapedView.Models;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class DiagonalView : ViewShape, IClipPathCreator
    {
        private DiagonalDirection diagonalDirection;
        private DiagonalPosition diagonalPosition;
        private float diagonalAngle;

        public DiagonalDirection DiagonalDirection
        {
            get { return diagonalDirection; }
            set { diagonalDirection = value; }
        }

        public DiagonalPosition DiagonalPosition
        {
            get { return DiagonalAngle > 0 ? DiagonalPosition.Left : DiagonalPosition.Right; }
            set { diagonalPosition = value; RequiresShapeUpdate(); }
        }

        public float DiagonalAngle
        {
            get { return diagonalAngle; }
            set { diagonalAngle = value; RequiresShapeUpdate(); }
        }

        public DiagonalView(Context context) : base(context)
        {
            Init(context, null);
        }

        public DiagonalView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public DiagonalView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        public DiagonalView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public DiagonalView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context, attrs);
        }

        private void Init(Context context, IAttributeSet attrs)
        {
            DiagonalPosition = DiagonalPosition.Top;
            DiagonalDirection = DiagonalDirection.Right;
            DiagonalAngle = 0f;

            if (attrs != null)
            {
                TypedArray attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.DiagonalView);
                DiagonalAngle = attributes.GetFloat(Resource.Styleable.DiagonalView_shape_diagonal_angle, DiagonalAngle);
                DiagonalPosition = (DiagonalPosition)attributes.GetInteger(Resource.Styleable.DiagonalView_shape_diagonal_position, (int)DiagonalPosition);
                DiagonalDirection = (DiagonalDirection)attributes.GetInteger(Resource.Styleable.DiagonalView_shape_diagonal_direction, (int)DiagonalDirection);
                attributes.Recycle();
            }
            SetClipPathCreator(this);
        }

        public Path CreateClipPath(int width, int height)
        {
            Path path = new Path();

            float diagonalAngleAbs = Java.Lang.Math.Abs(DiagonalAngle);
            bool isDirectionLeft = DiagonalDirection == DiagonalDirection.Left;
            float perpendicularHeight = (float)(width * Java.Lang.Math.Tan(Java.Lang.Math.ToRadians(diagonalAngleAbs)));

            switch (DiagonalPosition)
            {
                case DiagonalPosition.Bottom:
                    if (isDirectionLeft)
                    {
                        path.MoveTo(PaddingLeft, PaddingRight);
                        path.LineTo(width - PaddingRight, PaddingTop);
                        path.LineTo(width - PaddingRight, height - perpendicularHeight - PaddingBottom);
                        path.LineTo(PaddingLeft, height - PaddingBottom);
                        path.Close();
                    }
                    else
                    {
                        path.MoveTo(width - PaddingRight, height - PaddingBottom);
                        path.LineTo(PaddingLeft, height - perpendicularHeight - PaddingBottom);
                        path.LineTo(PaddingLeft, PaddingTop);
                        path.LineTo(width - PaddingRight, PaddingTop);
                        path.Close();
                    }
                    break;
                case DiagonalPosition.Top:
                    if (isDirectionLeft)
                    {
                        path.MoveTo(width - PaddingRight, height - PaddingBottom);
                        path.LineTo(width - PaddingRight, PaddingTop + perpendicularHeight);
                        path.LineTo(PaddingLeft, PaddingTop);
                        path.LineTo(PaddingLeft, height - PaddingBottom);
                        path.Close();
                    }
                    else
                    {
                        path.MoveTo(width - PaddingRight, height - PaddingBottom);
                        path.LineTo(width - PaddingRight, PaddingTop);
                        path.LineTo(PaddingLeft, PaddingTop + perpendicularHeight);
                        path.LineTo(PaddingLeft, height - PaddingBottom);
                        path.Close();
                    }
                    break;
                case DiagonalPosition.Right:
                    if (isDirectionLeft)
                    {
                        path.MoveTo(PaddingLeft, PaddingTop);
                        path.LineTo(width - PaddingRight, PaddingTop);
                        path.LineTo(width - PaddingRight - perpendicularHeight, height - PaddingBottom);
                        path.LineTo(PaddingLeft, height - PaddingBottom);
                        path.Close();
                    }
                    else
                    {
                        path.MoveTo(PaddingLeft, PaddingTop);
                        path.LineTo(width - PaddingRight - perpendicularHeight, PaddingTop);
                        path.LineTo(width - PaddingRight, height - PaddingBottom);
                        path.LineTo(PaddingLeft, height - PaddingBottom);
                        path.Close();
                    }
                    break;
                case DiagonalPosition.Left:
                    if (isDirectionLeft)
                    {
                        path.MoveTo(PaddingLeft + perpendicularHeight, PaddingTop);
                        path.LineTo(width - PaddingRight, PaddingTop);
                        path.LineTo(width - PaddingRight, height - PaddingBottom);
                        path.LineTo(PaddingLeft, height - PaddingBottom);
                        path.Close();
                    }
                    else
                    {
                        path.MoveTo(PaddingLeft, PaddingTop);
                        path.LineTo(width - PaddingRight, PaddingTop);
                        path.LineTo(width - PaddingRight, height - PaddingBottom);
                        path.LineTo(PaddingLeft + perpendicularHeight, height - PaddingBottom);
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