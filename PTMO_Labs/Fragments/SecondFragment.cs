using Android.OS;
using Android.Views;
using Android.Widget;

using BottomNavigationViewPager.Fragments;

using Newtonsoft.Json;

using PTMO_Labs.Models;

using static Android.Views.View;

namespace PTMO_Labs.Fragments
{
    public class SecondFragment : TheFragment, IOnClickListener
    {
        private Restaurant r;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            r = new Restaurant();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.SecondFragment, container, false);

            Button btn = view.FindViewById<Button>(Resource.Id.BtnSave);

            btn.SetOnClickListener(this);

            return view;
        }

        public void OnClick(View v)
        {
            View root = v.RootView;
            EditText addr = root.FindViewById<EditText>(Resource.Id.Addr);
            EditText name = root.FindViewById<EditText>(Resource.Id.Name);

            r.Address = addr.Text;
            r.Name = name.Text;

            RadioGroup types = root.FindViewById<RadioGroup>(Resource.Id.Types);
            switch (types.CheckedRadioButtonId)
            {
                case Resource.Id.SitDown:
                    r.Type = RestaurantTypes.SitDown;
                    break;
                case Resource.Id.TakeOut:
                    r.Type = RestaurantTypes.TakeOut;
                    break;
                case Resource.Id.Delivery:
                    r.Type = RestaurantTypes.Delivery;
                    break;
            }

            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(r));

            ToastNotification.ToastMessage("Saved! Check console");


        }
    }
}