using FeedbackEditor.Util;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC.Actions
{
    [XmlRoot("i")]
    [AddINotifyPropertyChangedInterface]
    public class PlayAnySequenceAction : SequenceAction
    {
        [XmlElement("m_SequenceIds")]
        public String SequenceIDsForSerialization 
        {
            get
            {
                var intList = SequenceIDs.Select(x => (int)x);
                return CdataHelper.BuildCdataString(intList);
            }
            set
            {
                try
                {
                    var intList = CdataHelper.ParseValues(value);
                    SequenceIDs = intList.Select(x => (SequenceID)x).ToList();
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine($"Could not load SequenceIDs. {value} Contains an invalid sequenceID | {e.Message}");
                }
            }
        }

        [XmlIgnore]
        public List<SequenceID> SequenceIDs { get; set; } = new();

        [XmlIgnore]
        [DoNotNotify]
        public override ActionType ElementType { get; set; } = ActionType.PLAY_ANY_SEQUENCE;

        public int MinPlayCount { get; set; } = 1;
        public int MinPlayTime { get; set; } = 0;
        public int MaxPlayCount { get; set; } = 0;
        public int MaxPlayTime { get; set; } = 0;
    }
}
