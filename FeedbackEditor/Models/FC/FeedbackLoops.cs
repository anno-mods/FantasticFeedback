using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    public class FeedbackLoop
    {
        [XmlElement("k")]
        //Index of the SequenceDefinition that should be active
        public int KeyIndex { get; set; }
        [XmlElement("v")]
        //Index of the SequenceDefinition that is played when k is active
        public int ValueIndex { get; set; }
    }
}
