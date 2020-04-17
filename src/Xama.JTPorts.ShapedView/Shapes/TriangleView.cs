using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Xama.JTPorts.ShapedView.Interfaces;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class TriangleView : ViewShape, IClipPathCreator
    {
        private float percentBottom;
        private float percentLeft;
        private float percentRight;

        public float PercentBottom
        {
            get => percentBottom;
            set
            {
                this.percentBottom = value;
                RequiresShapeUpdate();
            }
        }

        public float PercentLeft
        {
            get => percentLeft;
            set
            {
                this.percentLeft = value;
                RequiresShapeUpdate();
            }
        }

        public float PercentRight
        {
            get => percentRight;
            set
            {
                this.percentRight = value;
                RequiresShapeUpdate();
            }
        }

        public TriangleView(Context context) : base(context)
        {
            Init(context, null);
        }

        public TriangleView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public TriangleView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        private void Init(Context context, IAttributeSet attrs)
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
            SetClipPathCreator(this);
        }

        public Path CreateClipPath(int width, int height)
        {
            Path path = new Path();
            path.MoveTo(0, PercentLeft * height);
            path.LineTo(PercentBottom * width, height);
            path.LineTo(width, PercentRight * height);
            path.Close();

            return path;
        }

        public bool RequiresBitmap()
        {
            return false;
        }
    }
}