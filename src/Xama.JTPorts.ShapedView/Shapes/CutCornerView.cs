using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Xama.JTPorts.ShapedView.Interfaces;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class CutCornerView : ViewShape, IClipPathCreator
    {
        private float topLeftCutSizePx;
        private float topRightCutSizePx;
        private float bottomRightCutSizePx;
        private float bottomLeftCutSizePx;

        public float TopLeftCutSizePx
        {
            get => topLeftCutSizePx;
            set { topLeftCutSizePx = value; RequiresShapeUpdate(); }
        }

        public float TopLeftCutSizedP
        {
            get => PxToDp(TopLeftCutSizePx);
            set => TopLeftCutSizePx = DpToPx(value);
        }

        public float TopRightCutSizePx
        {
            get => topRightCutSizePx;
            set { topRightCutSizePx = value; RequiresShapeUpdate(); }
        }

        public float TopRightCutSizedP
        {
            get => PxToDp(TopRightCutSizePx);
            set => TopRightCutSizePx = DpToPx(value);
        }

        public float BottomRightCutSizePx
        {
            get => bottomRightCutSizePx;
            set
            {
                bottomRightCutSizePx = value; RequiresShapeUpdate();
            }
        }

        public float BottomRightCutSizedP
        {
            get => PxToDp(BottomRightCutSizePx);
            set => BottomRightCutSizePx = DpToPx(value);
        }

        public float BottomLeftCutSizePx
        {
            get => bottomLeftCutSizePx;
            set
            {
                bottomLeftCutSizePx = value; RequiresShapeUpdate();
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
            SetClipPathCreator(this);
        }

        public Path CreateClipPath(int width, int height)
        {
            var rectF = new RectF(0, 0, width, height);
            return GeneratePath(rectF, TopLeftCutSizePx, TopRightCutSizePx, BottomRightCutSizePx, BottomLeftCutSizePx);
        }

        public bool RequiresBitmap()
        {
            return false;
        }

        private Path GeneratePath(RectF rect, float topLeftDiameter, float topRightDiameter, float bottomRightDiameter, float bottomLeftDiameter)
        {
            Path path = new Path();

            topLeftDiameter = topLeftDiameter < 0 ? 0 : topLeftDiameter;
            topRightDiameter = topRightDiameter < 0 ? 0 : topRightDiameter;
            bottomLeftDiameter = bottomLeftDiameter < 0 ? 0 : bottomLeftDiameter;
            bottomRightDiameter = bottomRightDiameter < 0 ? 0 : bottomRightDiameter;

            path.MoveTo(rect.Left + topLeftDiameter, rect.Top);
            path.LineTo(rect.Right - topRightDiameter, rect.Top);
            path.LineTo(rect.Right, rect.Top + topRightDiameter);
            path.LineTo(rect.Right, rect.Bottom - bottomRightDiameter);
            path.LineTo(rect.Right - bottomRightDiameter, rect.Bottom);
            path.LineTo(rect.Left + bottomLeftDiameter, rect.Bottom);
            path.LineTo(rect.Left, rect.Bottom - bottomLeftDiameter);
            path.LineTo(rect.Left, rect.Top + topLeftDiameter);
            path.LineTo(rect.Left + topLeftDiameter, rect.Top);
            path.Close();

            return path;
        }

    }
}