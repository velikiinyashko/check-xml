using System.Xml.Schema;

namespace check_xml.Interface
{
    public interface ILoadScheme
    {
        XmlSchemaSet AddSchema();
        bool AddPath(string XSDPath);
    }
}
