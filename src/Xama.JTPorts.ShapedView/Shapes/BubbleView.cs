using Android.Content;
using Android.Content.Res;
using Android.Util;
using Xama.JTPorts.ShapedView.Models;
using Xama.JTPorts.ShapedView.PathCreators;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class BubbleView : ViewShape
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

        public float BorderRadiusPx
        {
            get
            {
                return BorderRadiusPx;
            }
            set
            {
                BorderRadiusPx = value;
                RequiresShapeUpdate();
            }
        }

        public float BorderRadiusDp
        {
            get
            {
                return PxToDp(BorderRadiusPx);
            }
            set
            {
                BorderRadiusPx = DpToPx(value);
            }
        }

        public float ArrowHeightPx
        {
            get
            {
                return ArrowHeightPx;
            }
            set
            {
                ArrowHeightPx = DpToPx(value);
                RequiresShapeUpdate();
            }
        }

        public float ArrowHeightdP
        {
            get
            {
                return PxToDp(ArrowHeightdP);
            }
            set
            {
                ArrowHeightPx = DpToPx(value);
            }
        }

        public float ArrowWidthPx
        {
            get
            {
                return ArrowWidthPx;
            }
            set
            {
                ArrowWidthPx = DpToPx(value);
                RequiresShapeUpdate();
            }
        }

        public float ArrowWidthdP
        {
            get
            {
                return PxToDp(ArrowWidthPx);
            }
            set
            {
                ArrowWidthPx = DpToPx(value);
            }
        }

        public float PositionPer
        {
            get
            {
                return PositionPer;
            }
            set
            {
                PositionPer = value;
                RequiresShapeUpdate();
            }
        }

        public BubbleView(Context context) : base(context)
        {
            Init(context, null);
        }

        public BubbleView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public BubbleView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        private void Init(Context context, IAttributeSet attrs)
        {
            PositionPer = 0.5f;
            HeightPx = 0f;
            if (attrs != null)
            {
                TypedArray attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.BubbleView);
                BorderRadiusPx = attributes.GetDimensionPixelSize(Resource.Styleable.BubbleView_shape_bubble_borderRadius, (int)DpToPx(10));
                ClipPosition = (BasePosition)attributes.GetInteger(Resource.Styleable.BubbleView_shape_bubble_arrowPosition, (int)ClipPosition);
                ArrowHeightPx = attributes.GetDimensionPixelSize(Resource.Styleable.BubbleView_shape_bubble_arrowHeight, (int)DpToPx(10));
                ArrowWidthPx = attributes.GetDimensionPixelSize(Resource.Styleable.BubbleView_shape_bubble_arrowWidth, (int)DpToPx(10));
                PositionPer = attributes.GetFloat(Resource.Styleable.BubbleView_arrow_posititon_percent, PositionPer);
                attributes.Recycle();
            }
            SetClipPathCreator(new BubbleClipPathCreator(ClipPosition, HeightPx, ArrowHeightPx, ArrowWidthPx, BorderRadiusPx, BorderRadiusPx, BorderRadiusPx, BorderRadiusPx));
        }
    }
}