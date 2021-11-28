using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace nookbot
{
    class Config
    {
        private const string configFile = "config.json";
        private const string configFolder = "config";
        public const string configFullPath = configFolder + "/" + configFile;

        public static BotConfig botConfig;

        static Config()
        {
            if (!Directory.Exists(configFolder))
                Directory.CreateDirectory(configFolder);

            if(!File.Exists(configFullPath))
            {
                botConfig = new BotConfig();
                File.WriteAllText(configFullPath, JsonConvert.SerializeObject(botConfig, Formatting.Indented));
            }
            else
            {
                botConfig = JsonConvert.DeserializeObject<BotConfig>(File.ReadAllText(configFullPath));
            }
        }
    }

    public class BotConfig
    {
        public string commandPrefix = ".";
        public string token = "";
    }
}
