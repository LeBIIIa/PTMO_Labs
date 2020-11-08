using Android.Content;

using Newtonsoft.Json;

namespace Common.Helpers
{
    public static class IntentExtension
    {
        public static Intent PutExtra<TExtra>(this Intent intent, string name, TExtra extra)
        {
            string json = JsonConvert.SerializeObject(extra);
            intent.PutExtra(name, json);
            return intent;
        }

        public static TExtra GetExtra<TExtra>(this Intent intent, string name)
        {
            string json = intent.GetStringExtra(name);
            if (string.IsNullOrWhiteSpace(json))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<TExtra>(json);
        }
    }
}
