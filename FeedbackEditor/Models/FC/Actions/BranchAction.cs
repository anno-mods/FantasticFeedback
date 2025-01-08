using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC.Actions
{
    [XmlRoot("i")]
    public class BranchAction : SequenceAction
    {
        [XmlArrayItem("i")]
        public List<BranchEntry> BranchList { get; set; } = new();

        public BranchAction() {
            ElementType = ActionType.BRANCH;
        }
    }

    public class BranchEntry
    {
        public int pair1 { get; set; }
        public BranchElementContainer pair2 { get; set; }
    }

    [Serializable]
    public class BranchElementContainer : ElementContainer, IXmlSerializable
    {
        public BranchElementContainer() {
            int i = 0; 
        }
        [XmlElement(ElementName = "hasValue")]
        public bool HasValue { get; set; } = true;

        public new void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteStartElement("hasValue");
            writer.WriteValue(HasValue ? "1" : "0");
            writer.WriteEndElement();
        }
    }
}
