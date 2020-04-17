using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Java.Lang;
using Xama.JTPorts.ShapedView.Interfaces;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class StarView : ViewShape, IClipPathCreator
    {
        private float numberOfPoints;

        public float NumberOfPoints
        {
            get => numberOfPoints;
            set
            {
                this.numberOfPoints = value;
                RequiresShapeUpdate();
            }
        }

        public StarView(Context context) : base(context)
        {
            Init(context, null);
        }

        public StarView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public StarView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        private void Init(Context context, IAttributeSet attrs)
        {
            NumberOfPoints = 5;

            if (attrs != null)
            {
                TypedArray attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.StarView);
                int points = attributes.GetInteger(Resource.Styleable.StarView_shape_star_noOfPoints, (int)NumberOfPoints);
                NumberOfPoints = points > 2 ? points : NumberOfPoints;
                attributes.Recycle();
            }
            SetClipPathCreator(this);
        }

        public Path CreateClipPath(int width, int height)
        {
            int vertices = (int)NumberOfPoints * 2;
            float alpha = (float)(2 * Math.Pi) / vertices;
            int radius = (height <= width ? height : width) / 2;
            float centerX = width / 2;
            float centerY = height / 2;

            Path path = new Path();
            for (int i = vertices + 1; i != 0; i--)
            {
                float r = radius * (i % 2 + 1) / 2;
                double omega = alpha * i;
                path.LineTo((float)(r * Math.Sin(omega)) + centerX, (float)(r * Math.Cos(omega)) + centerY);
            }
            path.Close();
            return path;
        }

        public bool RequiresBitmap()
        {
            return true;
        }
    }
}