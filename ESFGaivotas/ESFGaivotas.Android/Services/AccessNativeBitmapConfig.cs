using Android.App;
using Android.Graphics;
using ESFGaivotas.Enumerations;
using System;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Android.Factories;
using AndroidBitmapDescriptor = Android.Gms.Maps.Model.BitmapDescriptor;
using AndroidBitmapDescriptorFactory = Android.Gms.Maps.Model.BitmapDescriptorFactory;

namespace ESFGaivotas.Droid.Services
{
    public sealed class AccessNativeBitmapConfig : IBitmapDescriptorFactory
    {
        public AndroidBitmapDescriptor ToNative(BitmapDescriptor descriptor)
        {
            var debrisType = (EDebrisType)Enum.Parse(typeof(EDebrisType), descriptor.Id);
            var resourceName = $"debris{debrisType.ToString().ToLower()}medium";
            var resId = Application.Context.Resources.GetIdentifier(resourceName, "drawable", "com.companyname.ESFGaivotas");
            var frameId = Resource.Drawable.DebrisFrame;

            using (Bitmap frameBitmap = BitmapFactory.DecodeResource(Application.Context.Resources, frameId))
            using (Bitmap baseBitmap = BitmapFactory.DecodeResource(Application.Context.Resources, resId))
            using (Paint paint = new Paint(PaintFlags.AntiAlias))
            {
                Bitmap resultBitmap = Bitmap.CreateBitmap(frameBitmap, 0, 0, frameBitmap.Width - 1, frameBitmap.Height - 1);
                Canvas canvas = new Canvas(resultBitmap);
                canvas.DrawBitmap(resultBitmap, 0, 0, paint);
                canvas.DrawBitmap(baseBitmap, baseBitmap.Width / 2, baseBitmap.Height / 2, paint);

                return AndroidBitmapDescriptorFactory.FromBitmap(resultBitmap);
            }
        }
    }
}