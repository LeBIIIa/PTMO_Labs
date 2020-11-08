using Android.Support.V4.App;
namespace PTMO_Labs
{
    public class ViewPagerAdapter : FragmentPagerAdapter
    {
        private readonly Fragment[] _fragments;

        public ViewPagerAdapter(FragmentManager fm, Fragment[] fragments) : base(fm)
        {
            _fragments = fragments;
        }

        public override int Count => _fragments.Length;

        public override Fragment GetItem(int position)
        {
            return _fragments[position];
        }
    }
}