using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;

using BottomNavigationViewPager.Fragments;

using Common;
using Common.Entities;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using PTMO_Labs.Adapters;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTMO_Labs.Fragments
{

    public enum TypeOfWork
    {
        Add,
        Edit
    }

    public class Lab5Fragment : TheFragment
    {
        private OpSystemDataAdapter dataAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            List<OpSystem> data = await Task.Run(async () =>
            {
                return await GetDataFromDB();
            });
            dataAdapter.SetItems(data);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Lab5Fragment, container, false);

            Toolbar toolbarMenu = view.FindViewById<Toolbar>(Resource.Id.toolbar_up);
            toolbarMenu.InflateMenu(Resource.Menu.toolbar_menu);
            toolbarMenu.MenuItemClick += ToolbarMenu_MenuItemClick;

            RecyclerView recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView1);
            recyclerView.SetLayoutManager(new LinearLayoutManager(inflater.Context));
            dataAdapter = new OpSystemDataAdapter();
            recyclerView.SetAdapter(dataAdapter);

            return view;
        }

        private async void ToolbarMenu_MenuItemClick(object sender, Toolbar.MenuItemClickEventArgs e)
        {
            Intent intent;
            switch (e.Item.ItemId)
            {
                case Resource.Id.menu_item_add:
                    intent = new Intent(Activity, typeof(AddEditActivity));
                    intent.PutExtra("TypeOfWork", (int)TypeOfWork.Add);
                    StartActivityForResult(intent, 0);
                    break;
                case Resource.Id.menu_item_edit:
                    intent = new Intent(Activity, typeof(AddEditActivity));
                    if (dataAdapter.GetSelected == null)
                        return;
                    intent.PutExtra("Edit", JsonConvert.SerializeObject(dataAdapter.GetSelected));
                    intent.PutExtra("TypeOfWork", (int)TypeOfWork.Edit);
                    StartActivityForResult(intent, 0);
                    break;
                case Resource.Id.menu_item_delete:
                    using (ApplicationContext context = new ApplicationContext())
                    {
                        OpSystem deleted = dataAdapter.GetSelected;
                        if (deleted == null)
                            return;
                        context.OperatingSystems.Remove(deleted);
                        await context.SaveChangesAsync();
                    }
                    dataAdapter.RemoveItem(dataAdapter.SelectedId);
                    break;
                default:
                    break;
            }
        }

        private async Task<List<OpSystem>> GetDataFromDB()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return await context.OperatingSystems.AsNoTracking().ToListAsync();
            }
        }

        public void NotifyCollectionChanged(TypeOfWork typeOfWork, OpSystem opSystem)
        {
            if (typeOfWork == TypeOfWork.Add)
            {
                dataAdapter.AddItem(opSystem);
            }
            else if (typeOfWork == TypeOfWork.Edit)
            {
                dataAdapter.UpdateItem(opSystem);
            }
        }
    }
}