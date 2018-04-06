using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Collector2.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

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

        private const string URL = "http://collectorv2.azurewebsites.net/api/ItemImages/";

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

            picture = path.LoadAndResizeBitmap(370, 470);
            mPicture.SetImageBitmap(path.ExifRotateBitmap(picture));
        }

        private async void EncodeAndUpload(object sender, EventArgs e)
        {
            mStatus.SetTextColor(Color.White);
            mStatus.Text = "Working...";
            mDescription = FindViewById<EditText>(Resource.Id.picture_et_description);

            var description = mDescription.Text;
            if (description == "" || description == null)
            {
                description = "None";
            }

            using (var client = new HttpClient())
            {
                var itemImage = new ItemImage()
                {
                    Description = description,
                    ImageBase64 = path.ExifRotateBitmap(picture).BitmapToBase64(),
                };

                var uri = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(itemImage);
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
}