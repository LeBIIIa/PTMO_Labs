
using Android.App;
using Android.OS;
using Android.Widget;

namespace PTMO_Labs
{
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_second);
            Button btn = FindViewById<Button>(Resource.Id.Button2);
            ToastNotification.ToastMessage("OnCreate");
            btn.Click += (obj, e) =>
            {
                OnBackPressed();
            };
        }

        protected override void OnStart()
        {
            base.OnStart();
            ToastNotification.ToastMessage("OnStart");
        }

        protected override void OnResume()
        {
            base.OnResume();
            ToastNotification.ToastMessage("OnResume");
        }

        protected override void OnStop()
        {
            base.OnStop();
            ToastNotification.ToastMessage("OnStop");
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            ToastNotification.ToastMessage("OnRestart");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ToastNotification.ToastMessage("OnDestroy");
        }

        protected override void OnPause()
        {
            base.OnPause();
            ToastNotification.ToastMessage("OnPause");
        }

        protected override void OnPostResume()
        {
            base.OnPostResume();
            ToastNotification.ToastMessage("OnPostResume");
        }
    }
}