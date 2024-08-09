using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Joins;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    public class AssetVariationList : IXmlSerializable
    {
        public Dictionary<int, int> GuidVariationList { get; set; } = new();

        public String Lol { get; set; }

        public string AssetGroupNames { get; set; }

        public XmlSchema? GetSchema()
        {
            return null; 
        }

        public void ReadXml(XmlReader reader)
        {
            var tempReader = reader.ReadSubtree();

            while (tempReader.Read())
            {
                if (tempReader.NodeType != XmlNodeType.Element)
                    continue;
                if (tempReader.Name == nameof(GuidVariationList))
                {
                    XElement? el = XNode.ReadFrom(tempReader) as XElement;
                    Lol = el.Value;
                    FillGuidsFromCdataString(el.Value);
                }
                if (reader.Name == nameof(AssetGroupNames))
                {
                    XElement? el = XNode.ReadFrom(tempReader) as XElement;
                    AssetGroupNames = el.Value;
                }
            }
            reader.ReadEndElement();
            int i = 0; 
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
                }
                first = false;
                builder.Append(key);
                builder.Append(' ');
                builder.Append(value);
            }
            builder.Append(']');
            writer.WriteElementString(nameof(GuidVariationList), builder.ToString());
            writer.WriteElementString(nameof(AssetGroupNames), AssetGroupNames);
        }

        private void FillGuidsFromCdataString(String cdata)
        {
            var match = Regex.Match(cdata, @"CDATA\[([0-9\s-]+)\]");
            try
            {
                var extract = match.Groups[1].Value;
                var split = extract.Split(' ');
                for (int i = 0; i < split.Length; i += 2)
                {
                    GuidVariationList.Add(int.Parse(split[i]), int.Parse(split[i + 1]));
                }
            }
            catch
            {
                Console.WriteLine($"Error parsing CDATA {cdata}");
            }

        }
    }
}
