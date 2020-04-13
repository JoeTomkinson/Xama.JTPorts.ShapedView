using Android.Content;
using Android.Content.Res;
using Android.Util;
using Xama.JTPorts.ShapedView.Models;
using Xama.JTPorts.ShapedView.PathCreators;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class PolygonView : ViewShape
    {
        public int NumberOfSides
        {
            get => NumberOfSides;
            set { NumberOfSides = value; RequiresShapeUpdate(); }
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

            SetClipPathCreator(new PolygonClipPathCreator(NumberOfSides));
        }
    }
}