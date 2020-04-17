using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Java.Lang;
using Xama.JTPorts.ShapedView.Interfaces;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class PolygonView : ViewShape, IClipPathCreator
    {
        private int numberOfSides;

        public int NumberOfSides
        {
            get => numberOfSides;
            set { numberOfSides = value; RequiresShapeUpdate(); }
        }

        public PolygonView(Context context) : base(context)
        {
            Init(context, null);
        }

        public PolygonView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public PolygonView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        private void Init(Context context, IAttributeSet attrs)
        {
            NumberOfSides = 4;

            if (attrs != null)
            {
                TypedArray attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.PolygonView);
                int sides = attributes.GetInteger(Resource.Styleable.PolygonView_shape_polygon_noOfSides, NumberOfSides);

                NumberOfSides = sides > 3 ? sides : NumberOfSides;
                attributes.Recycle();
            }

            SetClipPathCreator(this);
        }

        public Path CreateClipPath(int width, int height)
        {
            float section = (float)(2.0 * Math.Pi / NumberOfSides);
            int polygonSize = Math.Min(width, height);
            int radius = polygonSize / 2;
            int centerX = width / 2;
            int centerY = height / 2;

            Path polygonPath = new Path();
            polygonPath.MoveTo((centerX + radius * (float)Math.Cos(0)), (centerY + radius * (float)Math.Sin(0)));

            for (int i = 1; i < NumberOfSides; i++)
            {
                polygonPath.LineTo((centerX + radius * (float)Math.Cos(section * i)),
                        (centerY + radius * (float)Math.Sin(section * i)));
            }

            polygonPath.Close();
            return polygonPath;
        }

        public bool RequiresBitmap()
        {
            return true;
        }
    }
}