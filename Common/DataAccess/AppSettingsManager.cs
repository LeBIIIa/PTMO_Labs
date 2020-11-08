using Newtonsoft.Json.Linq;

using System.IO;
using System.Reflection;

namespace Common.DataAccess
{
    public class AppSettingsManager
    {
        private static AppSettingsManager _instance;
        private readonly JObject _secrets;

        private const string Namespace = "Common";
        private const string FileName = "appsettings.json";

        private AppSettingsManager()
        {
            Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppSettingsManager)).Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{Namespace}.{FileName}");
            using StreamReader reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            _secrets = JObject.Parse(json);
        }

        public static AppSettingsManager Settings
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppSettingsManager();
                }

                return _instance;
            }
        }

        public string this[string name]
        {
            get
            {
                string[] path = name.Split(':');

                JToken node = _secrets[path[0]];
                for (int index = 1; index < path.Length; index++)
                {
                    node = node[path[index]];
                }

                return node.ToString();
            }
        }

        public string GetConnectingString()
        {
            return this["ConnectionStrings:connectingString"];
        }
    }
}
