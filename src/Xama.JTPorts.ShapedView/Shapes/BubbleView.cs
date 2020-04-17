using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Xama.JTPorts.ShapedView.Interfaces;
using Xama.JTPorts.ShapedView.Models;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class BubbleView : ViewShape, IClipPathCreator
    {
        private float heightPx;
        private BubblePosition clipPosition;
        private float borderRadiusPx;
        private float arrowHeightPx;
        private float arrowWidthPx;
        private float positionPer;

        public float HeightPx
        {
            get => heightPx;
            set { heightPx = value; RequiresShapeUpdate(); }
        }

        public float HeightDp
        {
            get => PxToDp(HeightPx);
            set => HeightPx = PxToDp(HeightDp);
        }

        public BubblePosition ClipPosition
        {
            get => clipPosition;
            set { clipPosition = value; RequiresShapeUpdate(); }
        }

        public float BorderRadiusPx
        {
            get
            {
                return borderRadiusPx;
            }
            set
            {
                borderRadiusPx = value;
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
                return arrowHeightPx;
            }
            set
            {
                arrowHeightPx = DpToPx(value);
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
                return arrowWidthPx;
            }
            set
            {
                arrowWidthPx = DpToPx(value);
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
                return positionPer;
            }
            set
            {
                positionPer = value;
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
                ClipPosition = (BubblePosition)attributes.GetInteger(Resource.Styleable.BubbleView_shape_bubble_arrowPosition, (int)ClipPosition);
                ArrowHeightPx = attributes.GetDimensionPixelSize(Resource.Styleable.BubbleView_shape_bubble_arrowHeight, (int)DpToPx(10));
                ArrowWidthPx = attributes.GetDimensionPixelSize(Resource.Styleable.BubbleView_shape_bubble_arrowWidth, (int)DpToPx(10));
                PositionPer = attributes.GetFloat(Resource.Styleable.BubbleView_arrow_posititon_percent, PositionPer);
                attributes.Recycle();
            }
            SetClipPathCreator(this);
        }

        public Path CreateClipPath(int width, int height)
        {
            RectF myRect = new RectF(0, 0, width, height);
            return DrawBubble(myRect, BorderRadiusPx, BorderRadiusPx, BorderRadiusPx, BorderRadiusPx);
        }

        public bool RequiresBitmap()
        {
            return false;
        }

        private Path DrawBubble(RectF myRect, float topLeftDiameter, float topRightDiameter, float bottomRightDiameter, float bottomLeftDiameter)
        {
            Path path = new Path();

            topLeftDiameter = topLeftDiameter < 0 ? 0 : topLeftDiameter;
            topRightDiameter = topRightDiameter < 0 ? 0 : topRightDiameter;
            bottomLeftDiameter = bottomLeftDiameter < 0 ? 0 : bottomLeftDiameter;
            bottomRightDiameter = bottomRightDiameter < 0 ? 0 : bottomRightDiameter;

            float spacingLeft = this.ClipPosition == BubblePosition.Left ? ArrowHeightPx : 0;
            float spacingTop = this.ClipPosition == BubblePosition.Top ? ArrowHeightPx : 0;
            float spacingRight = this.ClipPosition == BubblePosition.Right ? ArrowHeightPx : 0;
            float spacingBottom = this.ClipPosition == BubblePosition.Bottom ? ArrowHeightPx : 0;

            float left = spacingLeft + myRect.Left;
            float top = spacingTop + myRect.Top;
            float right = myRect.Right - spacingRight;
            float bottom = myRect.Bottom - spacingBottom;

            float centerX = (myRect.Left + myRect.Right) * PositionPer;

            path.MoveTo(left + topLeftDiameter / 2f, top);
            // LEFT, TOP

            if (ClipPosition == BubblePosition.Top)
            {
                path.LineTo(centerX - ArrowWidthPx, top);
                path.LineTo(centerX, myRect.Top);
                path.LineTo(centerX + ArrowWidthPx, top);
            }

            path.LineTo(right - topRightDiameter / 2f, top);

            path.QuadTo(right, top, right, top + topRightDiameter / 2);
            //RIGHT, TOP

            if (ClipPosition == BubblePosition.Right)
            {
                path.LineTo(right, bottom - (bottom * (1 - PositionPer)) - ArrowWidthPx);
                path.LineTo(myRect.Right, bottom - (bottom * (1 - PositionPer)));
                path.LineTo(right, bottom - (bottom * (1 - PositionPer)) + ArrowWidthPx);
            }
            path.LineTo(right, bottom - bottomRightDiameter / 2);

            path.QuadTo(right, bottom, right - bottomRightDiameter / 2, bottom);
            //RIGHT, BOTTOM

            if (ClipPosition == BubblePosition.Bottom)
            {
                path.LineTo(centerX + ArrowWidthPx, bottom);
                path.LineTo(centerX, myRect.Bottom);
                path.LineTo(centerX - ArrowWidthPx, bottom);
            }
            path.LineTo(left + bottomLeftDiameter / 2, bottom);

            path.QuadTo(left, bottom, left, bottom - bottomLeftDiameter / 2);
            //LEFT, BOTTOM

            if (ClipPosition == BubblePosition.Left)
            {
                path.LineTo(left, bottom - (bottom * (1 - PositionPer)) + ArrowWidthPx);
                path.LineTo(myRect.Left, bottom - (bottom * (1 - PositionPer)));
                path.LineTo(left, bottom - (bottom * (1 - PositionPer)) - ArrowWidthPx);
            }
            path.LineTo(left, top + topLeftDiameter / 2);

            path.QuadTo(left, top, left + topLeftDiameter / 2, top);

            path.Close();

            return path;
        }
    }
}