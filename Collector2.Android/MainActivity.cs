using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Provider;
using Android.Content;
using Android.Runtime;

namespace Collector2.Android
{
    [Activity(Label = "Collector2.Android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Button mCameraBtn;
        private Button mGalleryBtn;

        private const string AUTHORITY = "collector2.fileprovider";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mCameraBtn = FindViewById<Button>(Resource.Id.main_btn_camera);
            mCameraBtn.Click += TakePicture;

            mGalleryBtn = FindViewById<Button>(Resource.Id.main_btn_gallery);
            mGalleryBtn.Click += SelectFromGallery;
        }

        private void TakePicture(object sender, System.EventArgs e)
        {
            App._file = new File(App._dir, String.Format("collector2_item{0}.jpg", Guid.NewGuid()));
            Intent camera = new Intent(MediaStore.ActionImageCapture);
            camera.PutExtra(MediaStore.ExtraOutput, FileProvider.GetUriForFile(context, AUTHORITY, App._file));
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
            Uri contentUri = FileProvider.GetUriForFile(context, AUTHORITY, App._file);
            mediaScanIntent.SetData(contentUri);
            context.SendBroadcast(mediaScanIntent);
        }

        private void SelectFromGallery(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}

