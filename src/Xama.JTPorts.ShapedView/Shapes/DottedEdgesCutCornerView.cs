using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Xama.JTPorts.ShapedView.Interfaces;
using Xama.JTPorts.ShapedView.Models;

namespace Xama.JTPorts.ShapedView.Shapes
{
    public class DottedEdgesCutCornerView : ViewShape, IClipPathCreator
    {
        private RectF _rectF = new RectF();
        private DotEdgePosition dotEdgePosition;
        private float topLeftCutSize;
        private float topRightCutSize;
        private float bottomRightCutSize;
        private float bottomLeftCutSize;
        private float dotRadius;
        private float dotSpacing;

        public DotEdgePosition DotEdgePosition
        {
            get => dotEdgePosition;
            set { dotEdgePosition = value; RequiresShapeUpdate(); }
        }

        public float TopLeftCutSize
        {
            get => topLeftCutSize;
            set
            {
                this.topLeftCutSize = value;
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
            get => topRightCutSize;
            set
            {
                topRightCutSize = value;
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
            get => bottomRightCutSize;
            set
            {
                bottomRightCutSize = value;
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
            get => bottomLeftCutSize;
            set
            {
                bottomLeftCutSize = value;
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
            get => dotRadius;
            set
            {
                dotRadius = value;
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
            get => dotSpacing;
            set
            {
                dotSpacing = value;
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
                DotEdgePosition = (DotEdgePosition)attributes.GetInteger(Resource.Styleable.DottedEdgesCutCornerView_shape_edge_position, (int)DotEdgePosition.None);
                DotRadius = attributes.GetDimensionPixelSize(Resource.Styleable.DottedEdgesCutCornerView_shape_dot_radius, (int)DotRadius);
                DotSpacing = attributes.GetDimensionPixelSize(Resource.Styleable.DottedEdgesCutCornerView_shape_dot_spacing, (int)DotSpacing);
                attributes.Recycle();
            }
            SetClipPathCreator(this);
        }

        public Path CreateClipPath(int width, int height)
        {
            _rectF.Set(0, 0, width, height);
            return generatePath(_rectF, TopLeftCutSize, TopRightCutSize, BottomRightCutSize, BottomLeftCutSize);
        }

        public bool RequiresBitmap()
        {
            return false;
        }

        private Path generatePath(RectF rect, float topLeftDiameter, float topRightDiameter, float bottomRightDiameter, float bottomLeftDiameter)
        {
            Path path = new Path();

            topLeftDiameter = topLeftDiameter < 0 ? 0 : topLeftDiameter;
            topRightDiameter = topRightDiameter < 0 ? 0 : topRightDiameter;
            bottomLeftDiameter = bottomLeftDiameter < 0 ? 0 : bottomLeftDiameter;
            bottomRightDiameter = bottomRightDiameter < 0 ? 0 : bottomRightDiameter;

            path.MoveTo(rect.Left + topLeftDiameter, rect.Top);
            if (ContainsFlag(DotEdgePosition.Top))
            {
                int count = 1;
                int x = (int)(rect.Left + topLeftDiameter + DotSpacing * count + DotRadius * 2 * (count - 1));
                while (x + DotSpacing + DotRadius * 2 <= rect.Right - topRightDiameter)
                {
                    x = (int)(rect.Left + topLeftDiameter + DotSpacing * count + DotRadius * 2 * (count - 1));
                    path.LineTo(x, rect.Top);
                    path.QuadTo(x + DotRadius, rect.Top + DotRadius, x + DotRadius * 2, rect.Top);
                    count++;
                }
                path.LineTo(rect.Right - topRightDiameter, rect.Top);
            }
            else
            {
                path.LineTo(rect.Right - topRightDiameter, rect.Top);
            }

            path.LineTo(rect.Right, rect.Top + topRightDiameter);
            if (ContainsFlag(DotEdgePosition.Right))
            {
                //drawing dots starts from the bottom just like the LEFT side so that when using two
                //widgets side by side, their dots positions will match each other
                path.LineTo(rect.Right - DotRadius, rect.Top + topRightDiameter);
                path.LineTo(rect.Right - DotRadius, rect.Bottom - bottomRightDiameter);
                path.LineTo(rect.Right, rect.Bottom - bottomRightDiameter);

                int count = 1;
                int y = (int)(rect.Bottom - bottomRightDiameter - DotSpacing * count - DotRadius * 2 * (count - 1));
                while (y - DotSpacing - DotRadius * 2 >= rect.Top + topRightDiameter)
                {
                    y = (int)(rect.Bottom - bottomRightDiameter - DotSpacing * count - DotRadius * 2 * (count - 1));
                    path.LineTo(rect.Right, y);
                    path.QuadTo(rect.Right - DotRadius, y - DotRadius, rect.Right, y - DotRadius * 2);
                    count++;
                }
                path.LineTo(rect.Right, rect.Top + topRightDiameter);
                path.LineTo(rect.Right - DotRadius, rect.Top + topRightDiameter);
                path.LineTo(rect.Right - DotRadius, rect.Bottom - bottomRightDiameter);
                path.LineTo(rect.Right, rect.Bottom - bottomRightDiameter);
            }
            else
            {
                path.LineTo(rect.Right, rect.Bottom - bottomRightDiameter);
            }

            path.LineTo(rect.Right - bottomRightDiameter, rect.Bottom);
            if (ContainsFlag(DotEdgePosition.Bottom))
            {
                int count = 1;
                int x = (int)(rect.Right - bottomRightDiameter - DotSpacing * count - DotRadius * 2 * (count - 1));
                while (x - DotSpacing - DotRadius * 2 >= rect.Left + bottomLeftDiameter)
                {
                    x = (int)(rect.Right - bottomRightDiameter - DotSpacing * count - DotRadius * 2 * (count - 1));
                    path.LineTo(x, rect.Bottom);
                    path.QuadTo(x - DotRadius, rect.Bottom - DotRadius, x - DotRadius * 2, rect.Bottom);
                    count++;
                }
                path.LineTo(rect.Left + bottomLeftDiameter, rect.Bottom);
            }
            else
            {
                path.LineTo(rect.Left + bottomLeftDiameter, rect.Bottom);
            }

            path.LineTo(rect.Left, rect.Bottom - bottomLeftDiameter);
            if (ContainsFlag(DotEdgePosition.Left))
            {
                int count = 1;
                int y = (int)(rect.Bottom - bottomLeftDiameter - DotSpacing * count - DotRadius * 2 * (count - 1));
                while (y - DotSpacing - DotRadius * 2 >= rect.Top + topLeftDiameter)
                {
                    y = (int)(rect.Bottom - bottomLeftDiameter - DotSpacing * count - DotRadius * 2 * (count - 1));
                    path.LineTo(rect.Left, y);
                    path.QuadTo(rect.Left + DotRadius, y - DotRadius, rect.Left, y - DotRadius * 2);
                    count++;
                }
                path.LineTo(rect.Left, rect.Top + topLeftDiameter);
            }
            else
            {
                path.LineTo(rect.Left, rect.Top + topLeftDiameter);
            }
            path.LineTo(rect.Left + topLeftDiameter, rect.Top);
            path.Close();

            return path;
        }

        private bool ContainsFlag(DotEdgePosition positionFlag)
        {
            return (DotEdgePosition | positionFlag) == DotEdgePosition;
        }
    }
}