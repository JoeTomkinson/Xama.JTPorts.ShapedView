using Android.Graphics;
using Android.Views;
using AndroidLiveWallpaperUtility.Custom.Managers;
using System;

namespace Xama.JTPorts.ShapedView.Abstracts
{
    public class CustomOutlineProvider : ViewOutlineProvider
    {
        public IClipManager ClipManager { get; private set; }

        public CustomOutlineProvider(IClipManager clipManager)
        {
            ClipManager = clipManager;
        }

        public override void GetOutline(View view, Outline outline)
        {
            if (ClipManager != null && !view.IsInEditMode)
            {
                Path shadowConvexPath = ClipManager.GetShadowConvexPath();
                if (shadowConvexPath != null)
                {
                    try
                    {
                        outline.SetConvexPath(shadowConvexPath);
                    }
                    catch (Exception e)
                    {
                        //
                    }
                }
            }
        }
    }
}