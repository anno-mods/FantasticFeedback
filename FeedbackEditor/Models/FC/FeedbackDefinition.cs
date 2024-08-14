using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    public class FeedbackDefinition
    {
        [XmlArrayItem("i")]
        public List<FeedbackConfig> FeedbackConfigs { get; set; } = new();
    }
}
