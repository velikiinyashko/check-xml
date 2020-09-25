using check_xml.Interface;
using System;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace check_xml
{
    class Program
    {
        static async Task Main(string[] args)
        {

            ILogApp log = new LogApp();
            ConfigurationJson json = new ConfigurationJson(log);

            Config config = await json.Load();

            if (config != null)
            {
                LoadSchema schema = new LoadSchema(log);
                if (schema.AddPath(config.PathXSD) != false)
                {
                    XmlSchemaSet schemaSet = schema.AddSchema();
                    CheckXML checkXML = new CheckXML(schemaSet, log);

                    if (checkXML.addPath(config.PathXML) != false)
                    {
                        checkXML.CheckXml(config.PathMove);
                    }
                }
            }
            log.WriteLog(LogLevel.Info, $"Complite! (Press any key)");
            Console.ReadKey();
        }
    }
}
