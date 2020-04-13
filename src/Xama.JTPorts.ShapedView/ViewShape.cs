using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using System;
using Xama.JTPorts.ShapedView.Abstracts;
using Xama.JTPorts.ShapedView.Managers;

namespace Xama.JTPorts.ShapedView
{
    public abstract class ViewShape : FrameLayout
    {
        private Paint clipPaint = new Paint(PaintFlags.AntiAlias);
        private Path clipPath = new Path();
        protected PorterDuffXfermode pdMode = new PorterDuffXfermode(PorterDuff.Mode.DstOut);
#nullable enable
        protected Drawable? drawable = null;
#nullable disable
        private IClipManager clipManager = new ClipPathManager();
        private bool requiersShapeUpdate = true;
        private Bitmap clipBitmap;
        private Path rectView = new Path();

        public ViewShape(Context context) : base(context)
        {
            Init(context, null);
        }

        public ViewShape(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public ViewShape(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        private void Init(Context context, IAttributeSet attrs)
        {
            clipPaint.AntiAlias = true;

            //Depreciated
            //DrawingCacheEnabled = true;

            SetWillNotDraw(false);

            clipPaint.Color = Color.Blue;
            clipPaint.SetStyle(Paint.Style.Fill);
            clipPaint.StrokeWidth = 1;

            if (Build.VERSION.SdkInt <= BuildVersionCodes.OMr1)
            {
                clipPaint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.DstIn));
                SetLayerType(LayerType.Software, clipPaint); //Only works for software layers
            }
            else
            {
                clipPaint.SetXfermode(pdMode);
                SetLayerType(LayerType.Software, null); //Only works for software layers
            }

            if (attrs != null)
            {
                TypedArray attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.ShapeOfView);

                if (attributes.HasValue(Resource.Styleable.ShapeOfView_shape_clip_drawable))
                {
                    int resourceId = attributes.GetResourceId(Resource.Styleable.ShapeOfView_shape_clip_drawable, -1);
                    if (-1 != resourceId)
                    {
                        SetDrawable(resourceId);
                    }
                }

                attributes.Recycle();
            }
        }

        protected float DpToPx(float dp)
        {
            return dp * this.Context.Resources.DisplayMetrics.Density;
        }

        protected float PxToDp(float px)
        {
            return px / this.Context.Resources.DisplayMetrics.Density;
        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            base.OnLayout(changed, left, top, right, bottom);
            if (changed)
            {
                RequiresShapeUpdate();
            }
        }

        private bool RequiresBitmap()
        {
            return IsInEditMode || (clipManager != null && clipManager.RequiresBitmap()) || drawable != null;
        }

        public void SetDrawable(Drawable drawable)
        {
            this.drawable = drawable;
            RequiresShapeUpdate();
        }

        public void SetDrawable(int redId)
        {
            SetDrawable(AndroidX.AppCompat.Content.Res.AppCompatResources.GetDrawable(Context, redId));
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            base.DispatchDraw(canvas);
            if (requiersShapeUpdate)
            {
                CalculateLayout(canvas.Width, canvas.Height);
                requiersShapeUpdate = false;
            }
            if (RequiresBitmap())
            {
                clipPaint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.DstIn));
                canvas.DrawBitmap(clipBitmap, 0, 0, clipPaint);
            }
            else
            {
                if (Build.VERSION.SdkInt <= BuildVersionCodes.OMr1)
                {
                    canvas.DrawPath(clipPath, clipPaint);
                }
                else
                {
                    canvas.DrawPath(rectView, clipPaint);
                }
            }

            if (Build.VERSION.SdkInt <= BuildVersionCodes.OMr1)
            {
                SetLayerType(LayerType.Hardware, null);
            }
        }

        private void CalculateLayout(int width, int height)
        {
            rectView.Reset();
            rectView.AddRect(0f, 0f, 1f * Width, 1f * Height, Path.Direction.Cw);

            if (clipManager != null)
            {
                if (width > 0 && height > 0)
                {
                    clipManager.SetupClipLayout(width, height);
                    clipPath.Reset();
                    clipPath.Set(clipManager.CreateMask(width, height));

                    if (RequiresBitmap())
                    {
                        if (clipBitmap != null)
                        {
                            clipBitmap.Recycle();
                        }
                        clipBitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
                        Canvas canvas = new Canvas(clipBitmap);

                        if (drawable != null)
                        {
                            drawable.SetBounds(0, 0, width, height);
                            drawable.Draw(canvas);
                        }
                        else
                        {
                            canvas.DrawPath(clipPath, clipManager.GetPaint());
                        }
                    }

                    //invert the path for android P
                    if (Build.VERSION.SdkInt > BuildVersionCodes.OMr1)
                    {
                        bool success = rectView.InvokeOp(clipPath, Path.Op.Difference);
                    }

                    //this needs to be fixed for 25.4.0
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop && ViewCompat.GetElevation(this) > 0f)
                    {
                        try
                        {
                            OutlineProvider = new CustomOutlineProvider(clipManager);
                        }
                        catch (Exception e)
                        {
                            //
                        }
                    }
                }
            }

            PostInvalidate();
        }

        public void SetClipPathCreator(IClipPathCreator createClipPath)
        {
            ((ClipPathManager)clipManager).SetClipPathCreator(createClipPath);
            RequiresShapeUpdate();
        }

        public virtual void RequiresShapeUpdate()
        {
            this.requiersShapeUpdate = true;
            PostInvalidate();
        }
    }
}