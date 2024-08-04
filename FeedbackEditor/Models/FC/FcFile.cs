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
        public FeedbackDefinition FeedbackDefinition { get; set; }
        [XmlElement]
        public ActorNames ActorNames { get; set; }
    }

}
