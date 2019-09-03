using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ConsoleTest.Infrastructure
{
    public class EnvironmentSettings
    {
        public string PODupCheckForEmeaApj { get; set; }
        public string PODupCheckUri { get; set; }

        public static IEnumerable<KeyValuePair<string, string>> Create(string settings = null)
        {
            var config = settings ?? Environment.GetEnvironmentVariable(Literals.PcfEnvKey);
            if (!string.IsNullOrWhiteSpace(config))
            {
                var configJson = JObject.Parse(config);
                var cupsSettings = configJson.SelectToken(Literals.PcfCupsKey).ToObject<EnvironmentSettings>();

                return new[]
                {
                    new KeyValuePair<string, string>(Literals.PODupCheckForEmeaApjKey, cupsSettings.PODupCheckForEmeaApj),
                    new KeyValuePair<string, string>(Literals.GdasPODupCheckUriUriKey, cupsSettings.PODupCheckUri),
                };
            }
            else return new KeyValuePair<string, string>[0];
        }
    }
}
