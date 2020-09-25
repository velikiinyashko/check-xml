using System;
using System.Xml;
using System.Xml.Serialization;

namespace check_xml.ClassSchema
{
    [XmlRoot]
    public class report
    {
        [XmlAttribute("time_from", DataType = "time")]
        public DateTime time_from;
        [XmlAttribute("time_to", DataType = "time")]
        public DateTime time_to;
        [XmlAttribute("from", DataType = "date")]
        public DateTime from;
        [XmlAttribute("to", DataType = "date")]
        public DateTime to;
        [XmlAttribute("devicename", DataType = "string")]
        public string devicename;
        [XmlAttribute("serialnumber", DataType = "long")]
        public long serialnumber;
        [XmlAttribute("last_start_up_correct_time", DataType = "string")]
        public string last_start_up_correct_time;
        [XmlElement("pass")]
        public pass[] pass;
    }
    public class pass
    {
        [XmlElement("Id")]
        public int Id;
        [XmlElement("Direction")]
        public string Direction;
        [XmlElement("Timestamp")]
        public string Timestamp;
    }
}
