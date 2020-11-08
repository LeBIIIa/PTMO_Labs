using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

using Common.Entities;

using Java.Security;

using System;
using System.Collections.Generic;

using static Android.Support.V7.Widget.RecyclerView;

namespace PTMO_Labs.Adapters
{
    public class OpSystemDataAdapter : RecyclerView.Adapter
    {
        public override int ItemCount => opSystems.Count;

        private List<OpSystem> opSystems = new List<OpSystem>();
        private RecyclerView Recycler;

        public OpSystem GetSelected
        {
            get
            {
                if (SelectedId != -1)
                {
                    return opSystems[SelectedId];
                }

                return null;
            }
        }

        public int SelectedId { get; private set; } = 0;
        public int LastPosition { get; private set; } = 0;

        public override void OnAttachedToRecyclerView(RecyclerView recyclerView)
        {
            base.OnAttachedToRecyclerView(recyclerView);
            Recycler = recyclerView;
        }

        public override void OnBindViewHolder(ViewHolder holder, int position)
        {
            ((OpSystemViewHolder)holder).Bind(opSystems[position], SelectedId == holder.AdapterPosition);
            holder.ItemView.Click += (sender, e) =>
            {
                SelectedId = position;
                NotifyDataSetChanged();
            };
        }
        public override ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListItem, parent, false);
            return new OpSystemViewHolder(view);
        }

        public void AddItem(OpSystem opSystem)
        {
            opSystems.Add(opSystem);
            NotifyDataSetChanged();
        }

        public void UpdateItem(OpSystem opSystem)
        {
            int index = opSystems.FindIndex(op => op.OpSystemPK == opSystem.OpSystemPK);
            opSystems[index] = opSystem;
            NotifyDataSetChanged();
        }

        public void RemoveItem(int position)
        {
            opSystems.RemoveAt(position);
            NotifyDataSetChanged();
        }

        public void SetItems(List<OpSystem> opSystems)
        {
            this.opSystems = opSystems;
            NotifyDataSetChanged();
        }

        private class OpSystemViewHolder : ViewHolder
        {
            private readonly TextView nameTextView;
            private readonly TextView companyTextView;
            private readonly TextView versionTextView;
            private readonly ImageView selectedImageView;


            public OpSystemViewHolder(View itemView) : base(itemView)
            {
                nameTextView = itemView.FindViewById<TextView>(Resource.Id.opSystemName);
                companyTextView = itemView.FindViewById<TextView>(Resource.Id.opSystemCompany);
                versionTextView = itemView.FindViewById<TextView>(Resource.Id.opSystemVersion);
                selectedImageView = itemView.FindViewById<ImageView>(Resource.Id.imageSelected);
            }

            public void Bind(OpSystem opSystem, bool isSelected)
            {
                if (isSelected)
                    selectedImageView.Visibility = ViewStates.Visible;
                else
                    selectedImageView.Visibility = ViewStates.Gone;

                nameTextView.Text = opSystem.Name;
                companyTextView.Text = opSystem.CompanyName;
                versionTextView.Text = opSystem.LatestVersion;
            }
        }
    }
}