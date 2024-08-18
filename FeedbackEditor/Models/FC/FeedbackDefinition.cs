using FeedbackEditor.Util;
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

        [XmlElement("ValidSequenceIDs")]
        public String ValidSequenceIDsForSerialization 
        {
            get => CdataHelper.BuildCdataString(ValidSequenceIDs.Cast<int>());
            set => ValidSequenceIDs 
                = CdataHelper.ParseValues(value)
                .Where(x => Enum.IsDefined(typeof(FeedbackSequenceType), x))
                .Cast<FeedbackSequenceType>()
                .ToList();
        }

        [XmlIgnore]
        public List<FeedbackSequenceType> ValidSequenceIDs { get; set; } = new() { FeedbackSequenceType.Default, FeedbackSequenceType.Work };
    }
}
