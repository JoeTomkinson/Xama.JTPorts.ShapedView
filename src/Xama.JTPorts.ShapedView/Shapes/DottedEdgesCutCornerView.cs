using Android.Content;
using Android.Content.Res;
using Android.Util;
using Xama.JTPorts.ShapedView.Models;
using Xama.JTPorts.ShapedView.PathCreators;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class DottedEdgesCutCornerView : ViewShape
    {
        public BasePosition DotEdgePosition
        {
            get => DotEdgePosition;
            set { DotEdgePosition = value; RequiresShapeUpdate(); }
        }

        public float TopLeftCutSize
        {
            get => TopLeftCutSize;
            set
            {
                this.TopLeftCutSize = value;
                RequiresShapeUpdate();
            }
        }

        public float TopLeftCutSizeDp
        {
            get { return PxToDp(TopLeftCutSize); }
            set { TopLeftCutSize = DpToPx(value); }
        }

        public float TopRightCutSize
        {
            get => TopRightCutSize;
            set
            {
                TopRightCutSize = value;
                RequiresShapeUpdate();
            }
        }

        public float TopRightCutSizeDp
        {
            get { return PxToDp(TopRightCutSize); }
            set { TopLeftCutSize = DpToPx(value); }
        }

        public float BottomRightCutSize
        {
            get => BottomRightCutSize;
            set
            {
                BottomRightCutSize = value;
                RequiresShapeUpdate();
            }
        }

        public float BottomRightCutSizeDp
        {
            get { return PxToDp(BottomRightCutSize); }
            set { BottomRightCutSize = DpToPx(value); }
        }

        public float BottomLeftCutSize
        {
            get => BottomLeftCutSize;
            set
            {
                BottomLeftCutSize = value;
                RequiresShapeUpdate();
            }
        }

        public float BottomLeftCutSizeDp
        {
            get { return PxToDp(BottomLeftCutSize); }
            set { BottomLeftCutSize = DpToPx(value); }
        }

        public float DotRadius
        {
            get => DotRadius;
            set
            {
                DotRadius = value;
                RequiresShapeUpdate();
            }
        }

        public float DotRadiusdP
        {
            get { return PxToDp(DotRadius); }
            set { DotRadius = DpToPx(value); }
        }

        public float DotSpacing
        {
            get => DotSpacing;
            set
            {
                DotSpacing = value;
                RequiresShapeUpdate();
            }
        }

        public float DotSpacingdP
        {
            get { return PxToDp(DotSpacing); }
            set { DotSpacing = DpToPx(value); }
        }

        public DottedEdgesCutCornerView(Context context) : base(context)
        {
            Init(context, null);
        }

        public DottedEdgesCutCornerView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public DottedEdgesCutCornerView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        private void Init(Context context, IAttributeSet attrs)
        {
            TopLeftCutSize = 0f;
            TopRightCutSize = 0f;
            BottomRightCutSize = 0f;
            BottomLeftCutSize = 0f;
            DotRadius = 0f;
            DotSpacing = 0f;

            if (attrs != null)
            {
                TypedArray attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.DottedEdgesCutCornerView);
                TopLeftCutSize = attributes.GetDimensionPixelSize(Resource.Styleable.DottedEdgesCutCornerView_shape_dottedEdgesCutCorner_topLeftSize, (int)TopLeftCutSize);
                TopRightCutSize = attributes.GetDimensionPixelSize(Resource.Styleable.DottedEdgesCutCornerView_shape_dottedEdgesCutCorner_topRightSize, (int)TopRightCutSize);
                BottomLeftCutSize = attributes.GetDimensionPixelSize(Resource.Styleable.DottedEdgesCutCornerView_shape_dottedEdgesCutCorner_bottomLeftSize, (int)BottomLeftCutSize);
                BottomRightCutSize = attributes.GetDimensionPixelSize(Resource.Styleable.DottedEdgesCutCornerView_shape_dottedEdgesCutCorner_bottomRightSize, (int)BottomRightCutSize);
                DotEdgePosition = (BasePosition)attributes.GetInteger(Resource.Styleable.DottedEdgesCutCornerView_shape_edge_position, (int)BasePosition.None);
                DotRadius = attributes.GetDimensionPixelSize(Resource.Styleable.DottedEdgesCutCornerView_shape_dot_radius, (int)DotRadius);
                DotSpacing = attributes.GetDimensionPixelSize(Resource.Styleable.DottedEdgesCutCornerView_shape_dot_spacing, (int)DotSpacing);
                attributes.Recycle();
            }
            SetClipPathCreator(new DottedEdgeClipPathCreator(TopLeftCutSize, TopRightCutSize, BottomRightCutSize, BottomLeftCutSize, DotEdgePosition, DotRadius, DotSpacing));
        }
    }
}