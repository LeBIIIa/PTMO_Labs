using Android.Widget;

namespace PTMO_Labs
{
    public static class ToastNotification
    {
        public static void ToastMessage(string message)
        {
            Android.Content.Context context = Android.App.Application.Context;
            string tostMessage = message;
            ToastLength duration = ToastLength.Long;


            Toast.MakeText(context, tostMessage, duration).Show();
        }
    }
}
