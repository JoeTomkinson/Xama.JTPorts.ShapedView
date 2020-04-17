using Android.Graphics;
using Android.Runtime;
using System;
using Xama.JTPorts.ShapedView.Interfaces;

namespace Xama.JTPorts.ShapedView.Managers
{
    public class ClipPathManager : Java.Lang.Object, IClipManager
    {
        protected Path path = new Path();
        private Paint paint = new Paint(PaintFlags.AntiAlias);
        private IClipPathCreator clipPathCreator = null;

        public ClipPathManager()
        {
            paint.Color = Color.Black;
            paint.SetStyle(Paint.Style.Fill);
            paint.AntiAlias = true;
            paint.StrokeWidth = 1;
        }

        public ClipPathManager(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public Path CreateMask(int width, int height)
        {
            return path;
        }

        public Paint GetPaint()
        {
            return paint;
        }

        public Path GetShadowConvexPath()
        {
            return path;
        }

        public bool RequiresBitmap()
        {
            return clipPathCreator != null && clipPathCreator.RequiresBitmap();
        }

        public void SetupClipLayout(int width, int height)
        {
            path.Reset();
            Path clipPath = CreateClipPath(width, height);
            if (clipPath != null)
            {
                path.Set(clipPath);
            }
        }

        protected Path CreateClipPath(int width, int height)
        {
            if (clipPathCreator != null)
            {
                return clipPathCreator.CreateClipPath(width, height);
            }
            return null;
        }

        public void SetClipPathCreator(IClipPathCreator createClipPath)
        {
            this.clipPathCreator = createClipPath;
        }
    }
}