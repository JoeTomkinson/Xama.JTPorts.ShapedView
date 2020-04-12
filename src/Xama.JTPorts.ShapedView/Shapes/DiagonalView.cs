using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xama.JTPorts.ShapedView.Models;
using Xama.JTPorts.ShapedView.PathCreators;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class DiagonalView : ViewShape
    {
        public float HeightPx
        {
            get => HeightPx;
            set { HeightPx = value; RequiresShapeUpdate(); }
        }

        public float HeightDp
        {
            get => PxToDp(HeightPx);
            set => HeightPx = PxToDp(HeightDp);
        }

        public BasePosition ClipPosition
        {
            get => ClipPosition;
            set { ClipPosition = value; RequiresShapeUpdate(); }
        }

        public CropDirection CropDirection
        {
            get => HeightPx > 0 ? CropDirection.Outside : CropDirection.Inside;
            set => CropDirection = value;
        }

        public DiagonalDirection DiagonalDirection
        {
            get { return DiagonalDirection; }
            set { DiagonalDirection = value; }
        }

        public BasePosition DiagonalPosition
        {
            get { return DiagonalAngle > 0 ? BasePosition.Left : BasePosition.Right; }
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

        private void Init(Context context, IAttributeSet attrs)
        {
            DiagonalPosition = BasePosition.Top;
            DiagonalAngle = 0f;

            if (attrs != null)
            {
                TypedArray attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.DiagonalView);
                DiagonalAngle = attributes.GetFloat(Resource.Styleable.DiagonalView_shape_diagonal_angle, DiagonalAngle);
                DiagonalPosition = (BasePosition)attributes.GetInteger(Resource.Styleable.DiagonalView_shape_diagonal_position, (int)DiagonalPosition);
                attributes.Recycle();
            }
            SetClipPathCreator(new DiagonalClipPathCreator(ClipPosition, CropDirection, HeightPx, DiagonalDirection, DiagonalPosition,DiagonalAngle, PaddingLeft, PaddingRight, PaddingTop, PaddingBottom));
        }
    }
}