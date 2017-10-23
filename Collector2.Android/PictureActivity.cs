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
using System.Threading.Tasks;
using Android.Content.Res;

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
        private string path;

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

            path = Intent.GetStringExtra("path");

            var height = Resources.DisplayMetrics.HeightPixels;
            var width = mPicture.Height;
           // picture = path.LoadAndResizeBitmap(width, height);
            picture = path.LoadAndResizeBitmap(600, 800);
            mPicture.SetImageBitmap(path.ExifRotateBitmap(picture));
        }

        private async void EncodeAndUpload(object sender, EventArgs e)
        {
            mStatus.SetTextColor(Color.White);
            mStatus.Text = "Working...";
            mDescription = FindViewById<EditText>(Resource.Id.picture_et_description);
           
            var itemDescription = mDescription.Text;
            if (itemDescription == "" || itemDescription == null)
            {
                itemDescription = "None";
            }
            Bitmap img = BitmapFactory.DecodeFile(path);
            var ownerId = 99;
            var newItemMobile = new NewItemMobile()
            {
                OwnerId = ownerId,
                ImageBase64 = path.ExifRotateBitmap(picture).BitmapToBase64(),
                Description = itemDescription
            };
            
            var uri = new System.Uri(URL);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(newItemMobile);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                mStatus.Text = "Success!";
                mStatus.SetTextColor(Color.Green);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                mStatus.Text = "Already exists";
                mStatus.SetTextColor(Color.Blue);
            }
            else
            {
                mStatus.SetTextColor(Color.Red);
                mStatus.Text = response.StatusCode.ToString();
            }
        }

       
    }
}