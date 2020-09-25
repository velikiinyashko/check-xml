using check_xml.Interface;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace check_xml
{
    public class Config
    {
        public string PathXML { get; set; }
        public string PathXSD { get; set; }
        public string PathMove { get; set; }
    }

    public class ConfigurationJson
    {
        private ILogApp _log;
        public ConfigurationJson(ILogApp log)
        {
            _log = log;
        }

        public async Task<Config> Load()
        {
            try
            {
                using (FileStream stream = new FileStream($"{Directory.GetCurrentDirectory()}\\config.json", FileMode.Open))
                {
                    Config config = await JsonSerializer.DeserializeAsync<Config>(stream);
                    return config;
                }
            }
            catch (Exception ex)
            {
                _log.WriteLog(LogLevel.Error, ex.Message);
                return null;
            }
        }
    }
}
