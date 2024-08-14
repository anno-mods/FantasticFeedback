using FeedbackEditor.Models.FC.Dummy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    [XmlRoot("Content")]
    public class FcFile
    {
        [XmlElement]
        public DummyGroup DummyRoot { get; set; } = new();
        [XmlElement]
        public int IdCounter { get; set; }
        [XmlElement]
        public FeedbackDefinition FeedbackDefinition { get; set; } = new();
        [XmlElement]
        public ActorNames ActorNames { get; set; } = new();
    }

}
