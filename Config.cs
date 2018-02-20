using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace _200oker
{
    public static class Config
    {
        public static List<string> IgnoreUrlsStartingWith { get; set; }
        public static int MaxParentThreads { get; set; }
        public static int MaxChildThreads { get; set; }
        public static int TimeoutInSeconds { get; set; }

        public static IConfiguration Configuration { get; set; }

        static Config()
        {
            IgnoreUrlsStartingWith = new List<string>();
            MaxParentThreads = 5;
            MaxChildThreads = 5;
            TimeoutInSeconds = 60;

            // Read settings from appsettings.json to config
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var urls = GetSettingValueAsStringList("IgnoreUrlsStartingWith");
            if (urls != null)
                IgnoreUrlsStartingWith = urls;

            var mpt = GetSettingValueAsInt("maxParentThreads");
            if (mpt != null)
                MaxParentThreads = mpt.Value;

            var mct = GetSettingValueAsInt("maxChildThreads");
            if (mct != null)
                MaxChildThreads = mct.Value;

            var timeout = GetSettingValueAsInt("timeoutInSeconds");
            if (timeout != null)
                TimeoutInSeconds = timeout.Value;
        }

        private static int? GetSettingValueAsInt(string key)
        {
            var value = Configuration[key];
            if (String.IsNullOrWhiteSpace(value))
                return null;

            var intValue = 0;
            if (!int.TryParse(value, out intValue))
                return null;

            return intValue;
        }

        private static List<string> GetSettingValueAsStringList(string key)
        {
            var value = Configuration[key];
            if (String.IsNullOrWhiteSpace(value))
                return null;

            var list = new List<string>();
            foreach (var s in value.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                list.Add(s);
            }
            return list;
        }
    }
}