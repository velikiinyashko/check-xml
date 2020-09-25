using check_xml.Interface;
using System;
using System.IO;
using System.Xml.Schema;

namespace check_xml
{
    public class LoadSchema : ILoadScheme
    {
        private ILogApp _log;
        private string[] fileScheme;
        private int i = 0;
        public LoadSchema(ILogApp log)
        {
            _log = log;
        }

        public bool AddPath(string XSDPath)
        {
            fileScheme = Directory.GetFiles(XSDPath, "*.xsd");
            if (fileScheme.Length != 0)
            {
                return true;
            }
            else
            {
                _log.WriteLog(LogLevel.Error, $"Not found to folder \"XSD\" files");
                return false;
            }
        }

        public XmlSchemaSet AddSchema()
        {
            try
            {
                XmlSchemaSet schemaSet = new XmlSchemaSet();
                foreach (string path in fileScheme)
                {
                    schemaSet.Add($"{++i}", path);
                    _log.WriteLog(LogLevel.Info, $"Load XML scheme[{i}] name: {Path.GetFileName(path)}");
                }
                schemaSet.Compile();
                return schemaSet;
            }
            catch (Exception ex)
            {
                _log.WriteLog(LogLevel.Error, ex.Message);
                return null;
            }
        }
    }
}
