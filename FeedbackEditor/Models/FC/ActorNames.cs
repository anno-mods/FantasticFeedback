using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    public class ActorNames
    {
        [XmlElement(ElementName = "hasValue")]
        public bool HasValue { get; set; }

        [XmlElement("i")]
        public List<String> Names = new List<String>();

        public ActorNames() { }
    }
}
