using Android.Content;
using Android.Content.Res;
using Android.Util;
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
            SetClipPathCreator(new DiagonalClipPathCreator(DiagonalDirection, DiagonalPosition,DiagonalAngle, PaddingLeft, PaddingRight, PaddingTop, PaddingBottom));
        }
    }
}