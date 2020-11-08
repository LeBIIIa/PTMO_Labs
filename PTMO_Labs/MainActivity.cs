using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;

using BottomNavigationViewPager.Fragments;

using Common.Entities;
using Common.Helpers;

using PTMO_Labs.Fragments;

using System;

namespace PTMO_Labs
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private BottomNavigationView _navigationView;
        private ViewPager _viewPager;
        private Android.Support.V4.App.Fragment[] _fragments;

        private event Action<TypeOfWork, OpSystem> UpdateCollection;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            InitializeTabs();

            ToastNotification.ToastMessage("OnCreate");

            _navigationView = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            _navigationView.NavigationItemSelected += BottomBar_NavigationItemSelected;

            // find the view
            _viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);

            // set the adapter
            _viewPager.Adapter = new ViewPagerAdapter(SupportFragmentManager, _fragments);
            _viewPager.PageSelected += ViewPager_PageSelected;

        }

        private void InitializeTabs()
        {
            var fragment = TheFragment.NewInstance<Lab5Fragment>();
            UpdateCollection += fragment.NotifyCollectionChanged;

            _fragments = new Android.Support.V4.App.Fragment[] {
                 TheFragment.NewInstance<MainFragment>(),
                 TheFragment.NewInstance<SecondFragment>(),
                 TheFragment.NewInstance<Lab4Fragment>(),
                 fragment,
                 TheFragment.NewInstance<Lab6Fragment>()
            };
        }

        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            Android.Views.IMenuItem item = _navigationView.Menu.GetItem(e.Position);
            _navigationView.SelectedItemId = item.ItemId;
        }

        private void BottomBar_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            _viewPager.SetCurrentItem(e.Item.Order, true);
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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                var opSystem = data.GetExtra<OpSystem>("OpSystem");
                TypeOfWork typeOfWork = (TypeOfWork)data.GetIntExtra("TypeOfWork", (int)TypeOfWork.Add);
                UpdateCollection(typeOfWork, opSystem);
            }
        }
    }
}

