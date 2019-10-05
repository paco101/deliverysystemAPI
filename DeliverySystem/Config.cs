using System;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;

namespace DeliverySystem
{
    public static class Config
    {
        private const string ConfigFile = "appconfiguration.json";
        public static AppConfig AppConfiguration;

        static Config()
        {
            if (!File.Exists(ConfigFile))
            {
                AppConfiguration = new AppConfig();
                string json = JsonConvert.SerializeObject(AppConfiguration, Formatting.Indented);
                File.WriteAllText(ConfigFile, json);
            }
            else
            {
                string json = File.ReadAllText(ConfigFile);
                AppConfiguration = JsonConvert.DeserializeObject<AppConfig>(json);
            }
        }
    }

    public struct AppConfig
    {
        #region botConfigurations

        public string Url { get; set; } //final url

        public string Name { get; set; }

        public string ApiKey { get; set; }

        #endregion

        #region DeliveryConfigurations

        public int DailyRate { get; set; } //minimum deliveries per work day

        public TimeSpan EndWorkTime { get; set; }
        public TimeSpan StartWorkTime { get; set; }

        #endregion
    }
}