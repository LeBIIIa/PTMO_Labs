using Android.Support.V4.App;

using System;

namespace BottomNavigationViewPager.Fragments
{
    public class TheFragment : Fragment
    {
        public static T NewInstance<T>()
        {
            System.Type type = typeof(T);
            T obj = (T)Activator.CreateInstance(type);
            return obj;
        }
    }
}