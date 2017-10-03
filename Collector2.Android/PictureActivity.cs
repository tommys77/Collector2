using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


using Android.App;
using Android.Content;
using Android.OS;
using Uri = Android.Net.Uri;
using Android.Runtime;
using Android.Views;
using Java.IO;
using Android.Widget;
using Android.Graphics;
using Android.Provider;
using System.IO;
using Collector2.Model;

namespace Collector2.Android
{
    [Activity(Label = "Collector2.Android")]
    public class PictureActivity : Activity
    {
        private TextView mStatus;
        private EditText mDescription;
        private ImageView mPicture;
        private Button mEncodeBtn;
        private Bitmap picture;

        private const string URL = "http://collectorv2.azurewebsites.net/api/Items/";

        //private Bitmap mBitmap;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Picture);
            mPicture = FindViewById<ImageView>(Resource.Id.picture_iv_picture);
            mStatus = FindViewById<TextView>(Resource.Id.picture_tv_status);

            mEncodeBtn = FindViewById<Button>(Resource.Id.picture_btn_encode_and_upload);
            mEncodeBtn.Click += EncodeAndUpload;

            var path = Intent.GetStringExtra("path");
            picture = BitmapFactory.DecodeFile(path);
            //var height = Resources.DisplayMetrics.HeightPixels;
            //var width = mPicture.Height;
            //mBitmap = path.LoadAndResizeBitmap(width, height);
            mPicture.SetImageBitmap(path.ExifRotateBitmap(picture));
        }
        
        private async void EncodeAndUpload (object sender, EventArgs e)
        {
            var imageString = BitmapToBase64(picture);
            
            mDescription = FindViewById<EditText>(Resource.Id.picture_et_description);
            var itemDescription = mDescription.Text;
            if(itemDescription == "")
            {
                itemDescription = "None";
            }

            var ownerId = 99;
            Item item = new Item()
            {
                OwnerId = ownerId,
                ItemDescription = itemDescription,
                ImageString = imageString
            };
            var client = new HttpClient();

            var uri = new System.Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                mStatus.Text = "Success!";
                mStatus.SetTextColor(Color.Green);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                mStatus.Text = "Already exists";
                mStatus.SetTextColor(Color.Red);
            }
            
            
        }

        private string BitmapToBase64(Bitmap bitmap)
        {
            string str;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
                var bytes = stream.ToArray();
                str = Convert.ToBase64String(bytes);
            }
            return str;
        }

    }
}