using Android.Content;
using Android.Content.Res;
using Android.Util;
using Xama.JTPorts.ShapedView.PathCreators;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class TriangleView : ViewShape
    {
        public float PercentBottom
        {
            get => PercentBottom;
            set
            {
                this.PercentBottom = value;
                RequiresShapeUpdate();
            }
        }

        public float PercentLeft
        {
            get => PercentLeft;
            set
            {
                this.PercentLeft = value;
                RequiresShapeUpdate();
            }
        }

        public float PercentRight
        {
            get => PercentRight;
            set
            {
                this.PercentRight = value;
                RequiresShapeUpdate();
            }
        }

        public TriangleView(Context context) : base(context)
        {
            init(context, null);
        }

        public TriangleView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            init(context, attrs);
        }

        public TriangleView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            init(context, attrs);
        }

        private void init(Context context, IAttributeSet attrs)
        {
            PercentBottom = 0.5f;
            PercentLeft = 0f;
            PercentRight = 0f;

            if (attrs != null)
            {
                TypedArray attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.TriangleView);
                PercentBottom = attributes.GetFloat(Resource.Styleable.TriangleView_shape_triangle_percentBottom, PercentBottom);
                PercentLeft = attributes.GetFloat(Resource.Styleable.TriangleView_shape_triangle_percentLeft, PercentLeft);
                PercentRight = attributes.GetFloat(Resource.Styleable.TriangleView_shape_triangle_percentRight, PercentRight);
                attributes.Recycle();
            }
            SetClipPathCreator(new TriangleClipPathCreator(PercentBottom, PercentLeft, PercentRight));
        }
    }
}