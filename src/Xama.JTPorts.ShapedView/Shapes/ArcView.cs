using Android.Content;
using Android.Content.Res;
using Android.Util;
using Xama.JTPorts.ShapedView.Models;
using Xama.JTPorts.ShapedView.PathCreators;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class ArcView : ViewShape
    {
        public float HeightPx
        {
            get => HeightPx;
            set { HeightPx = value; RequiresShapeUpdate(); }
        }

        public float ArcHeightDp
        {
            get => PxToDp(HeightPx);
            set => HeightPx = PxToDp(ArcHeightDp);
        }

        public ArcPosition ClipPosition
        {
            get => ClipPosition;
            set { ClipPosition = value; RequiresShapeUpdate(); }
        }

        public CropDirection CropDirection
        {
            get => HeightPx > 0 ? CropDirection.Outside : CropDirection.Inside;
            set => CropDirection = value;
        }

        public ArcView(Context context) : base(context)
        {
            Init(context, null);
        }

        public ArcView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public ArcView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        private void Init(Context context, IAttributeSet attrs)
        {
            HeightPx = 0f;

            if (attrs != null)
            {
                TypedArray attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.ArcView);
                HeightPx = attributes.GetDimensionPixelSize(Resource.Styleable.ArcView_shape_arc_height, (int)HeightPx);
                ClipPosition = (ArcPosition)attributes.GetInteger(Resource.Styleable.ArcView_shape_arc_position, (int)ClipPosition);
                attributes.Recycle();
            }
            SetClipPathCreator(new ArcClipPathCreator(ClipPosition, CropDirection, HeightPx));
        }
    }
}