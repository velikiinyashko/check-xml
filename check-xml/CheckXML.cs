using check_xml.ClassSchema;
using check_xml.Interface;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace check_xml
{
    public class CheckXML
    {
        private int count = 0;
        private LoadSchema schemaSet;
        private MoveFile move;
        private XmlReaderSettings settings;
        private ILogApp _log;
        private string[] filePath { get; set; }

        public CheckXML(XmlSchemaSet xmlSchemas, ILogApp log)
        {
            _log = log;
            move = new MoveFile(log);
            schemaSet = new LoadSchema(log);
            settings = new XmlReaderSettings();
            settings.Schemas.Add(xmlSchemas);
        }

        public bool addPath(string XMLPath)
        {
            try
            {
                filePath = Directory.GetFiles(XMLPath, "*.xml");
                if (filePath.Length != 0)
                {
                    return true;
                }
                else
                {
                    _log.WriteLog(LogLevel.Error, "Not found to folder \"XML\" files");
                    return false;
                }
            }catch(Exception ex)
            {
                _log.WriteLog(LogLevel.Error, ex.Message);
                return false;
            }
        }
        public void CheckXml(string ToMoveNoValide)
        {
            //Console.WriteLine("\n");

            foreach (string path in filePath)
            {
                XmlReader reader = XmlReader.Create(path, settings);

                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(report));
                    report countBox = (report)serializer.Deserialize(reader);
                    XmlNamespaceManager manager = new XmlNamespaceManager(reader.NameTable);
                    XmlSchemaValidator validator = new XmlSchemaValidator(reader.NameTable, settings.Schemas, manager, XmlSchemaValidationFlags.None);
                    validator.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandler);
                    validator.Initialize();
                }
                catch (Exception ex)
                {
                    reader.Close();
                    move.Move(path, ToMoveNoValide);
                    _log.WriteLog(LogLevel.Error, $"file: [{Path.GetFileName(path)}] \r\n [{ex.InnerException}] | [{ex.Message}] \r\n");
                }
                reader.Close();
                _log.ProcentLog(filePath.Length, ++count);
            }
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {e.Message}");
                    break;
                case XmlSeverityType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Warning {e.Message}");
                    break;
            }
        }
    }
}
