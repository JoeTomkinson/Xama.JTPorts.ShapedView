using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Util;
using System;
using Xama.JTPorts.ShapedView.Models;
using Xama.JTPorts.ShapedView.PathCreators;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class DiagonalView : ViewShape
    {
        public DiagonalDirection DiagonalDirection
        {
            get { return DiagonalDirection; }
            set { DiagonalDirection = value; }
        }

        public DiagonalPosition DiagonalPosition
        {
            get { return DiagonalAngle > 0 ? DiagonalPosition.Left : DiagonalPosition.Right; }
            set { DiagonalPosition = value; RequiresShapeUpdate(); }
        }

        public float DiagonalAngle
        {
            get { return DiagonalAngle; }
            set { DiagonalAngle = value; RequiresShapeUpdate(); }
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
            SetClipPathCreator(new DiagonalClipPathCreator((int)DiagonalDirection, (int)DiagonalPosition, DiagonalAngle, PaddingLeft, PaddingRight, PaddingTop, PaddingBottom));
        }
    }
}