using Android.Content;
using Android.Content.Res;
using Android.Util;
using Xama.JTPorts.ShapedView.PathCreators;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class CutCornerView : ViewShape
    {
        public float TopLeftCutSizePx
        {
            get => TopLeftCutSizePx;
            set { TopLeftCutSizePx = value; RequiresShapeUpdate(); }
        }

        public float TopLeftCutSizedP
        {
            get => PxToDp(TopLeftCutSizePx);
            set => TopLeftCutSizePx = DpToPx(value);
        }

        public float TopRightCutSizePx
        {
            get => TopRightCutSizePx;
            set { TopRightCutSizePx = value; RequiresShapeUpdate(); }
        }

        public float TopRightCutSizedP
        {
            get => PxToDp(TopRightCutSizePx);
            set => TopRightCutSizePx = DpToPx(value);
        }

        public float BottomRightCutSizePx
        {
            get => BottomRightCutSizePx;
            set
            {
                BottomRightCutSizePx = value; RequiresShapeUpdate();
            }
        }

        public float BottomRightCutSizedP
        {
            get => PxToDp(BottomRightCutSizePx);
            set => BottomRightCutSizePx = DpToPx(value);
        }

        public float BottomLeftCutSizePx
        {
            get => BottomLeftCutSizePx;
            set
            {
                BottomLeftCutSizePx = value; RequiresShapeUpdate();
            }
        }

        public float BottomLeftCutSizedP
        {
            get => PxToDp(BottomLeftCutSizePx);
            set => BottomLeftCutSizePx = DpToPx(value);
        }

        public CutCornerView(Context context) : base(context)
        {
            Init(context, null);
        }

        public CutCornerView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public CutCornerView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        private void Init(Context context, IAttributeSet attrs)
        {
            TopLeftCutSizePx = 0f;
            TopRightCutSizePx = 0f;
            BottomRightCutSizePx = 0f;
            BottomLeftCutSizePx = 0f;

            if (attrs != null)
            {
                TypedArray attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.CutCornerView);
                TopLeftCutSizePx = attributes.GetDimensionPixelSize(Resource.Styleable.CutCornerView_shape_cutCorner_topLeftSize, (int)TopLeftCutSizePx);
                TopRightCutSizePx = attributes.GetDimensionPixelSize(Resource.Styleable.CutCornerView_shape_cutCorner_topRightSize, (int)TopRightCutSizePx);
                BottomLeftCutSizePx = attributes.GetDimensionPixelSize(Resource.Styleable.CutCornerView_shape_cutCorner_bottomLeftSize, (int)BottomLeftCutSizePx);
                BottomRightCutSizePx = attributes.GetDimensionPixelSize(Resource.Styleable.CutCornerView_shape_cutCorner_bottomRightSize, (int)BottomRightCutSizePx);
                attributes.Recycle();
            }
            SetClipPathCreator(new CurCornerPathCreator(TopLeftCutSizePx, TopRightCutSizePx, BottomRightCutSizePx, BottomLeftCutSizePx));
        }
    }
}