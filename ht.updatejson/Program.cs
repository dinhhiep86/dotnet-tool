using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Dynamic;
using System.IO;

namespace ht.updatejson
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Updating EPiServer Find configuration - Default Index and Service Url");
			UpdateConfiguration(args);
            Console.WriteLine(@"Updated EPiServer Find configuration - Index:{0} Url:{1}", args[0], args[1]);
        }
		
		
		public static void UpdateConfiguration(string[] args)
        {
            var path = System.IO.Directory.GetCurrentDirectory();
            if (args.Length > 2)
            {
                path += args[2];
            }
            var appSettingsPath = Path.Combine(path, "appsettings.json");
            var json = File.ReadAllText(appSettingsPath);

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(new ExpandoObjectConverter());
            jsonSettings.Converters.Add(new StringEnumConverter());

            dynamic config = JsonConvert.DeserializeObject<ExpandoObject>(json, jsonSettings);

            config.EPiServer.Find.DefaultIndex = args[0];
            config.EPiServer.Find.ServiceUrl = args[1];
            //config.EPiServer.Find.TrackingTimeout = 1000;

            var newJson = JsonConvert.SerializeObject(config, Formatting.Indented, jsonSettings);

            File.WriteAllText(appSettingsPath, newJson);
        }
    }
}
