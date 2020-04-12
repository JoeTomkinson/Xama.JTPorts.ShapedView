﻿using Android.Graphics;
using Xama.JTPorts.ShapedView.Managers;

namespace AndroidLiveWallpaperUtility.Custom.Managers
{
    public class ClipPathManager : IClipManager
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

        public Path CreateMask(int width, int height)
        {
            return path;
        }

        public Paint GetPaint()
        {
            return paint;
        }

#nullable enable
        public Path? GetShadowConvexPath()
        {
            return path;
        }
#nullable disable

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

#nullable enable
        protected Path? CreateClipPath(int width, int height)
        {
            if (clipPathCreator != null)
            {
                return clipPathCreator.CreateClipPath(width, height);
            }
            return null;
        }
#nullable disable

        public void SetClipPathCreator(IClipPathCreator createClipPath)
        {
            this.clipPathCreator = createClipPath;
        }

    }
}