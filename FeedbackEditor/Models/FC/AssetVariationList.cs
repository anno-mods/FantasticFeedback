using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    public class AssetVariationList : IXmlSerializable
    {
        public Dictionary<int, int> GuidVariationList { get; set; } = new();
        public string AssetGroupNames { get; set; }

        public XmlSchema? GetSchema()
        {
            return null; 
        }

        public void ReadXml(XmlReader reader)
        {
        }

        public void WriteXml(XmlWriter writer)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("CDATA[");
            var first = true; 
            foreach (var(key, value) in GuidVariationList)
            {
                if (!first)
                {
                    builder.Append(' ');
                    first = false; 
                }
                builder.Append(key);
                builder.Append(' ');
                builder.Append(value);
            }
            builder.Append(']');
            writer.WriteElementString(nameof(GuidVariationList), builder.ToString());
            writer.WriteElementString(nameof(AssetGroupNames), AssetGroupNames);
        }
    }
}
