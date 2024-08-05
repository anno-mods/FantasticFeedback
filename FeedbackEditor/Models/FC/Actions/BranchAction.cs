using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC.Actions
{
    [XmlRoot("i")]
    public class BranchAction : SequenceAction
    {
        [XmlArrayItem("i")]
        public List<BranchEntry> BranchList { get; set; } = new();
    }

    public class BranchEntry
    {
        public int pair1 { get; set; }
        public ElementContainer pair2 { get; set; }
    }

    public class BranchElementContainer : ElementContainer
    {
        [XmlElement(ElementName = "hasValue")]
        public bool HasValue { get; set; }
    }
}
