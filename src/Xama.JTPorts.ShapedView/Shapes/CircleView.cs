using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using System;
using Xama.JTPorts.ShapedView.PathCreators;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class CircleView : ViewShape
    {
        public float BorderWidth
        {
            get => BorderWidth;
            set { BorderWidth = value; RequiresShapeUpdate(); }
        }

        public float BorderWidthdP
        {
            get => PxToDp(BorderWidth);
            set { BorderWidth = DpToPx(value); }
        }

        public Color BorderColor
        {
            get => BorderColor;
            set => BorderColor = value;
        }

        private Paint borderPaint = new Paint(PaintFlags.AntiAlias);

        public CircleView(Context context) : base(context)
        {
            Init(context, null);
        }

        public CircleView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public CircleView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        private void Init(Context context, IAttributeSet attrs)
        {
            // apply defaults first
            BorderWidth = 0f;
            BorderColor = Color.White;

            if (attrs != null)
            {
                TypedArray attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.CircleView);
                BorderWidth = attributes.GetDimensionPixelSize(Resource.Styleable.CircleView_shape_circle_borderWidth, (int)BorderWidth);
                BorderColor = attributes.GetColor(Resource.Styleable.CircleView_shape_circle_borderColor, BorderColor);
                attributes.Recycle();
            }

            borderPaint.AntiAlias = true;
            borderPaint.SetStyle(Paint.Style.Stroke);
            SetClipPathCreator(new CircleClipPathCreator());
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            base.DispatchDraw(canvas);
            if (BorderWidth > 0)
            {
                borderPaint.StrokeWidth = BorderWidth;
                borderPaint.Color = BorderColor;
                canvas.DrawCircle(Width / 2f, Height / 2f, Math.Min((Width - BorderWidth) / 2f, (Height - BorderWidth) / 2f), borderPaint);
            }
        }
    }
}