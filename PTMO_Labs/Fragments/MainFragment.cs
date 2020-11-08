using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using BottomNavigationViewPager.Fragments;

using System;

namespace PTMO_Labs.Fragments
{
    public class MainFragment : TheFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.MainFragment, container, false);

            Button btn = view.FindViewById<Button>(Resource.Id.Button);
            btn.Click += Button_Click;

            return view;
        }

        public void Button_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Activity, typeof(SecondActivity));
            StartActivity(intent);
        }
    }
}