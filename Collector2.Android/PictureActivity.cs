using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Java.IO;
using Android.Widget;
using Android.Graphics;

namespace Collector2.Android
{
    [Activity(Label = "Collector2.Android")]
    public class PictureActivity : Activity
    {
        private ImageView mPicture;
        private Bitmap mBitmap;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Picture);
            mPicture = FindViewById<ImageView>(Resource.Id.picture_iv_picture);
            var path = Intent.GetStringExtra("path");
            //Toast.MakeText(this, path, ToastLength.Long).Show();
            
            var height = Resources.DisplayMetrics.HeightPixels;
            var width = mPicture.Height;
            mBitmap = path.LoadAndResizeBitmap(width, height);
            mPicture.SetImageBitmap(mBitmap);
        }
    }
}