using Android.Content;
using Android.Views;
using Android.Widget;

using Common.Entities;

using System.Collections.Generic;

namespace PTMO_Labs.Adapters
{
    public class SpinnerAdapter : ArrayAdapter<UniversityBuilding>
    {
        private readonly int resourceId;
        private readonly int textViewId;
        private List<UniversityBuilding> Buildings { get; set; } = new List<UniversityBuilding>();
        public override int Count => Buildings.Count;



        public SpinnerAdapter(Context context, int resourceId, int textviewId) : base(context, resourceId, textviewId)
        {
            this.resourceId = resourceId;
            this.textViewId = textviewId;
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = (TextView)LayoutInflater.From(Context).Inflate(resourceId, parent, false);
            } ((TextView)convertView).Text = GetItem(position).BuildingName;
            return convertView;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView textView = (TextView)LayoutInflater.From(Context).Inflate(textViewId, parent, false);

            textView.Text = GetItem(position).BuildingName;
            return textView;
        }

        public new UniversityBuilding GetItem(int position)
        {
            return Buildings[position];
        }

        public void AddItem(UniversityBuilding building)
        {
            Buildings.Add(building);
            NotifyDataSetChanged();
        }

        public void RemoveItem(int position)
        {
            Buildings.RemoveAt(position);
            NotifyDataSetChanged();
        }

        public void SetItems(List<UniversityBuilding> building)
        {
            Buildings = building;
            NotifyDataSetChanged();
        }
    }
}