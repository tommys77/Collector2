using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Media;
using Orientation = Android.Media.Orientation;
using System.IO;

namespace Collector2.Android
{
    public static class BitmapHelpers
    {
        public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
        {
            // First we get the the dimensions of the file on disk
            BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
            BitmapFactory.DecodeFile(fileName, options);

            // Next we calculate the ratio that we need to resize the image by
            // in order to fit the requested dimensions.
            int outHeight = options.OutHeight;
            int outWidth = options.OutWidth;
            int inSampleSize = 1;

            if (outHeight > height || outWidth > width)
            {
                inSampleSize = outWidth > outHeight
                                   ? outHeight / height
                                   : outWidth / width;
            }

            // Now we will load the image and have BitmapFactory resize it for us.
            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;
            Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

            return resizedBitmap;
        }

        public static Bitmap ExifRotateBitmap(this string filepath, Bitmap bitmap)
        {
            GC.Collect();
            var exif = new ExifInterface(filepath);
            var rotation = exif.GetAttributeInt(ExifInterface.TagOrientation, (int)Orientation.Normal);
            var rotationInDegrees = ExifToDegrees(rotation);
            if (rotationInDegrees == 0)
                return bitmap;

            using (var matrix = new Matrix())
            {
                matrix.PreRotate(rotationInDegrees);
                return Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
            }
        }
        public static int ExifToDegrees(int exifOrientation)
        {
            switch (exifOrientation)
            {
                case (int)Orientation.Rotate90:
                    return 90;
                case (int)Orientation.Rotate180:
                    return 180;
                case (int)Orientation.Rotate270:
                    return 270;
                default:
                    return 0;
            }
        }

        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
                bytes = stream.ToArray();

            }
            return bytes;
        }

    }
}