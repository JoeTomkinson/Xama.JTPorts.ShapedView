using Android.Content;
using Android.Content.Res;
using Android.Util;
using Xama.JTPorts.ShapedView.PathCreators;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class StarView : ViewShape
    {
        public float NumberOfPoints
        {
            get => NumberOfPoints;
            set
            {
                this.NumberOfPoints = value;
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
            SetClipPathCreator(new StarClipPathCreator(NumberOfPoints));
        }
    }
}