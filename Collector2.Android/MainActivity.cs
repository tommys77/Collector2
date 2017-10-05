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
        private const int CAMERA_REQUEST = 0;
        private const int GALLERY_REQUEST = 1;

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

        private void SelectFromGallery(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Intent gallery = new Intent();
            gallery.SetType("image/*");
            gallery.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(gallery, "Select photo"), 1);

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Intent pictureActivity = new Intent(this, typeof(PictureActivity));
            pictureActivity.PutExtra("requestCode", requestCode);

            if (requestCode == CAMERA_REQUEST)
            {
                Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                Uri contentUri = FileProvider.GetUriForFile(this, AUTHORITY, App._file);
                mediaScanIntent.SetData(contentUri);
                SendBroadcast(mediaScanIntent);

                if (resultCode == Result.Ok)
                {
                    pictureActivity.PutExtra("path", App._file.Path);
                    StartActivity(pictureActivity);
                }
            }
            if (requestCode == GALLERY_REQUEST)
            {
                if (resultCode != Result.Canceled)
                {
                    pictureActivity.PutExtra("path", GetRealPathFromURI(data.Data));
                    StartActivity(pictureActivity);
                }
            }
        }

        //Source: https://gist.github.com/ryupold/fe38e5acbe1586681e27#file-getrealpathfromuri-cs
        private string GetRealPathFromURI(Uri contentURI)
        {
            var cursor = ContentResolver.Query(contentURI, null, null, null, null);
            cursor.MoveToFirst();
            var documentId = cursor.GetString(0);
            documentId = documentId.Split(':')[1];
            cursor.Close();

            cursor = ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri, null, MediaStore.Images.Media.InterfaceConsts.Id + " = ? ", new[] { documentId }, null);
            cursor.MoveToFirst();
            string path = cursor.GetString(cursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.Data));
            cursor.Close();

            return path;
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

