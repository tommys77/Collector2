using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Java.IO;
using Android.Graphics;
using Android.Provider;
using Android.Content;
using Android.Runtime;
using Uri = Android.Net.Uri;
using Android.Support.V4.Content;
using Environment = Android.OS.Environment;
using Android.Util;
using System.Collections.Generic;
using Android.Content.PM;

namespace Collector2.Android
{
    [Activity(Label = "Collector2.Android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Button mCameraBtn;
        private Button mGalleryBtn;
        public static class App
        {
            public static File _file;
            public static File _dir;
            public static Bitmap bitmap;
        }

        private const string AUTHORITY = "collector2.fileprovider";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();
            }

            mCameraBtn = FindViewById<Button>(Resource.Id.main_btn_camera);
            mCameraBtn.Click += TakePicture;

            mGalleryBtn = FindViewById<Button>(Resource.Id.main_btn_gallery);
            mGalleryBtn.Click += SelectFromGallery;
        }

        private void TakePicture(object sender, System.EventArgs e)
        {
            App._file = new File(App._dir, String.Format("collector2_item{0}.jpg", Guid.NewGuid()));
            Intent camera = new Intent(MediaStore.ActionImageCapture);
            camera.PutExtra(MediaStore.ExtraOutput, FileProvider.GetUriForFile(this, AUTHORITY, App._file));
            try
            {
                StartActivityForResult(camera, 0);
            }
            catch (ActivityNotFoundException ex)
            {
                Log.Debug("MyTAG", ex.Message);
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = FileProvider.GetUriForFile(this, AUTHORITY, App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            if (resultCode == Result.Ok)
            {
                Intent pictureActivity = new Intent(this, typeof(PictureActivity));
                pictureActivity.PutExtra("path", App._file.Path);
                StartActivity(pictureActivity);
            }
        }

        private void SelectFromGallery(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities = this.PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);

            return availableActivities != null && availableActivities.Count > 0;
        }

        private void CreateDirectoryForPictures()
        {
            App._dir = new File(
                Environment.GetExternalStoragePublicDirectory(
                    Environment.DirectoryPictures), "Collector2_Photos");
            App._dir.Mkdirs();
        }
    }
}

