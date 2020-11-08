using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Views;
using Android.Widget;

using BottomNavigationViewPager.Fragments;

using Common;
using Common.Entities;

using Microsoft.EntityFrameworkCore;

using PTMO_Labs.Adapters;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTMO_Labs.Fragments
{
    public class Lab6Fragment : TheFragment, IOnMapReadyCallback
    {
        private SupportMapFragment mapFragment;
        private GoogleMap map;
        private SpinnerAdapter Adapter;
        private LatLng latLngSource;
        private LatLng latLngDestination;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            List<UniversityBuilding> data = await Task.Run(async () =>
            {
                return await GetDataFromDB();
            });
            Adapter.SetItems(data);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Lab6Fragment, container, false);

            Spinner spinner = view.FindViewById<Spinner>(Resource.Id.spinner);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);

            Adapter = new SpinnerAdapter(inflater.Context, Android.Resource.Layout.SimpleSpinnerDropDownItem, Android.Resource.Layout.SimpleSpinnerItem);
            spinner.Adapter = Adapter;

            if (mapFragment == null)
            {
                mapFragment = (SupportMapFragment)ChildFragmentManager.FindFragmentById(Resource.Id.map);
                mapFragment.GetMapAsync(this);
            }

            Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.map, mapFragment).Commit();
            return view;
        }

        public void OnMapReady(GoogleMap map)
        {
            this.map = map;

            map.MyLocationEnabled = true;
            map.UiSettings.ZoomControlsEnabled = true;
            map.UiSettings.MyLocationButtonEnabled = true;

            map.MyLocationChange += Map_MyLocationChange;
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            UniversityBuilding dest = Adapter.GetItem(e.Position);

            latLngDestination = new LatLng(dest.Latitude, dest.Longitude);

            FnUpdateCameraPosition(latLngSource);

            Activity.RunOnUiThread(() =>
            {
                if (map != null)
                {
                    map.Clear();
                    MarkOnMap("MyLocation", latLngSource, Resource.Drawable.MarkerSource);
                    MarkOnMap(dest.BuildingName, latLngDestination, Resource.Drawable.MarkerDest);
                }
            });

            PolylineOptions polylineoption = new PolylineOptions();
            polylineoption.InvokeColor(Android.Graphics.Color.Red);
            //polylineoption.Geodesic(true);
            polylineoption.Add(latLngSource, latLngDestination);
            Activity.RunOnUiThread(() =>
                map.AddPolyline(polylineoption));
        }

        private void Map_MyLocationChange(object sender, GoogleMap.MyLocationChangeEventArgs e)
        {
            latLngSource = new LatLng(e.Location.Latitude, e.Location.Longitude);
        }

        private async Task<List<UniversityBuilding>> GetDataFromDB()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return await context.UniversityBuildings.ToListAsync();
            }
        }

        private void MarkOnMap(string title, LatLng pos, int resourceId)
        {
            Activity.RunOnUiThread(() =>
            {
                try
                {
                    MarkerOptions marker = new MarkerOptions();
                    marker.SetTitle(title);
                    marker.SetPosition(pos); //Resource.Drawable.BlueDot
                    marker.SetIcon(BitmapDescriptorFactory.FromResource(resourceId));
                    map.AddMarker(marker);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        private void FnUpdateCameraPosition(LatLng pos)
        {
            try
            {
                CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
                builder.Target(pos);
                builder.Zoom(12);
                builder.Bearing(45);
                builder.Tilt(10);
                CameraPosition cameraPosition = builder.Build();
                CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
                map.AnimateCamera(cameraUpdate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }
    }

}