using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Java.Lang;
using Xama.JTPorts.ShapedView.PathCreators;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class RoundRectView : ViewShape
    {
        public float TopLeftRadius
        {
            get => TopLeftRadius;
            set { TopLeftRadius = value; RequiresShapeUpdate(); }
        }

        public float TopRightRadius
        {
            get => TopRightRadius;
            set { TopRightRadius = value; RequiresShapeUpdate(); }
        }

        public float BottomRightRadius
        {
            get => BottomRightRadius;
            set { BottomRightRadius = value; RequiresShapeUpdate(); }
        }

        public float BottomLeftRadius
        {
            get => BottomLeftRadius;
            set { BottomLeftRadius = value; RequiresShapeUpdate(); }
        }

        public Color BorderColor
        {
            get => BorderColor;
            set { BorderColor = value; RequiresShapeUpdate(); }
        }

        public float BorderWidthPx
        {
            get => BorderWidthPx;
            set { BorderWidthPx = value; RequiresShapeUpdate(); }
        }

        public float BorderWidthdP
        {
            get => PxToDp(BorderWidthPx);
            set { BorderWidthPx = DpToPx(value); }
        }

        //region border
        private Paint BorderPaint = new Paint(PaintFlags.AntiAlias);
        private RectF BorderRectF = new RectF();
        private Path BorderPath = new Path();

        public RoundRectView(Context context) : base(context)
        {
            Init(context, null);
        }

        public RoundRectView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public RoundRectView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        private void Init(Context context, IAttributeSet attrs)
        {
            // defaults
            TopLeftRadius = 0f;
            TopRightRadius = 0f;
            BottomRightRadius = 0f;
            BottomLeftRadius = 0f;
            BorderColor = Color.White;
            BorderWidthPx = 0f;

            if (attrs != null)
            {
                TypedArray attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.RoundRectView);
                TopLeftRadius = attributes.GetDimensionPixelSize(Resource.Styleable.RoundRectView_shape_roundRect_topLeftRadius, (int)TopLeftRadius);
                TopRightRadius = attributes.GetDimensionPixelSize(Resource.Styleable.RoundRectView_shape_roundRect_topRightRadius, (int)TopRightRadius);
                BottomLeftRadius = attributes.GetDimensionPixelSize(Resource.Styleable.RoundRectView_shape_roundRect_bottomLeftRadius, (int)BottomLeftRadius);
                BottomRightRadius = attributes.GetDimensionPixelSize(Resource.Styleable.RoundRectView_shape_roundRect_bottomRightRadius, (int)BottomRightRadius);
                BorderColor = attributes.GetColor(Resource.Styleable.RoundRectView_shape_roundRect_borderColor, BorderColor);
                BorderWidthPx = attributes.GetDimensionPixelSize(Resource.Styleable.RoundRectView_shape_roundRect_borderWidth, (int)BorderWidthPx);
                attributes.Recycle();
            }
            BorderPaint.SetStyle(Paint.Style.Stroke);
            SetClipPathCreator(new RoundedRectClipPathCreator(TopLeftRadius, TopRightRadius, BottomRightRadius, BottomLeftRadius));
        }

        public override void RequiresShapeUpdate()
        {
            BorderRectF.Set(BorderWidthPx / 2f, BorderWidthPx / 2f, Width - BorderWidthPx / 2f, Height - BorderWidthPx / 2f);

            BorderPath.Set(GeneratePath(false, BorderRectF,
                    TopLeftRadius,
                    TopRightRadius,
                    BottomRightRadius,
                    BottomLeftRadius
            ));
            base.RequiresShapeUpdate();
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            base.DispatchDraw(canvas);
            if (BorderWidthPx > 0)
            {
                BorderPaint.StrokeWidth = BorderWidthPx;
                BorderPaint.Color = BorderColor;
                canvas.DrawPath(BorderPath, BorderPaint);
            }
        }

        private Path GeneratePath(bool useBezier, RectF rect, float topLeftRadius, float topRightRadius, float bottomRightRadius, float bottomLeftRadius)
        {
            Path path = new Path();

            float left = rect.Left;
            float top = rect.Top;
            float bottom = rect.Bottom;
            float right = rect.Right;

            float maxSize = Math.Min(rect.Width() / 2f, rect.Height() / 2f);

            float topLeftRadiusAbs = Math.Abs(topLeftRadius);
            float topRightRadiusAbs = Math.Abs(topRightRadius);
            float bottomLeftRadiusAbs = Math.Abs(bottomLeftRadius);
            float bottomRightRadiusAbs = Math.Abs(bottomRightRadius);

            if (topLeftRadiusAbs > maxSize)
            {
                topLeftRadiusAbs = maxSize;
            }
            if (topRightRadiusAbs > maxSize)
            {
                topRightRadiusAbs = maxSize;
            }
            if (bottomLeftRadiusAbs > maxSize)
            {
                bottomLeftRadiusAbs = maxSize;
            }
            if (bottomRightRadiusAbs > maxSize)
            {
                bottomRightRadiusAbs = maxSize;
            }

            path.MoveTo(left + topLeftRadiusAbs, top);
            path.LineTo(right - topRightRadiusAbs, top);

            //float left, float top, float right, float bottom, float startAngle, float sweepAngle, boolean forceMoveTo
            if (useBezier)
            {
                path.QuadTo(right, top, right, top + topRightRadiusAbs);
            }
            else
            {
                float arc = topRightRadius > 0 ? 90 : -270;
                path.ArcTo(new RectF(right - topRightRadiusAbs * 2f, top, right, top + topRightRadiusAbs * 2f), -90, arc);
            }
            path.LineTo(right, bottom - bottomRightRadiusAbs);
            if (useBezier)
            {
                path.QuadTo(right, bottom, right - bottomRightRadiusAbs, bottom);
            }
            else
            {
                float arc = bottomRightRadiusAbs > 0 ? 90 : -270;
                path.ArcTo(new RectF(right - bottomRightRadiusAbs * 2f, bottom - bottomRightRadiusAbs * 2f, right, bottom), 0, arc);
            }
            path.LineTo(left + bottomLeftRadiusAbs, bottom);
            if (useBezier)
            {
                path.QuadTo(left, bottom, left, bottom - bottomLeftRadiusAbs);
            }
            else
            {
                float arc = bottomLeftRadiusAbs > 0 ? 90 : -270;
                path.ArcTo(new RectF(left, bottom - bottomLeftRadiusAbs * 2f, left + bottomLeftRadiusAbs * 2f, bottom), 90, arc);
            }
            path.LineTo(left, top + topLeftRadiusAbs);
            if (useBezier)
            {
                path.QuadTo(left, top, left + topLeftRadiusAbs, top);
            }
            else
            {
                float arc = topLeftRadiusAbs > 0 ? 90 : -270;
                path.ArcTo(new RectF(left, top, left + topLeftRadiusAbs * 2f, top + topLeftRadiusAbs * 2f), 180, arc);
            }
            path.Close();

            return path;
        }
    }
}