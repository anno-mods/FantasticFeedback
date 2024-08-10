using FeedbackEditor.Util;
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
        public List<(int, int)> GuidVariationList { get; set; } = new();

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
                    GuidVariationList = CdataHelper.ParseValues(el.Value);
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
            var cdata = CdataHelper.BuildCdataString(GuidVariationList);
            writer.WriteElementString(nameof(GuidVariationList), cdata);
            writer.WriteElementString(nameof(AssetGroupNames), AssetGroupNames);
        }
    }
}
