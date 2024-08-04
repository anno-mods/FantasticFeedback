using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC.Actions
{
    [XmlRoot("i")]
    public class WalkBetweenDummiesAction : SequenceAction
    {
        [XmlElement("WalkSequence")]
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int SequenceIDForSerializing
        {
            get => (int)WalkSequence;
            set => WalkSequence = (SequenceID)value;
        }

        [XmlIgnore]
        public SequenceID WalkSequence { get; set; } = SequenceID.walk01;

        [XmlIgnore]
        public override ActionType ElementType { get; set; } = ActionType.WALK_BETWEEN_DUMMIES;

        public float SpeedFactorF { get; set; }

        public int StartDummyId { get; set; }

        public int TargetDummyId { get; set; }

        public String StartDummy { get; set; }

        public String TargetDummy { get; set; }

        public bool WalkFromCurrentPosition { get; set; }

        public bool UseTargetDummyDirection { get; set; }

        public String DummyGroup { get; set; } = "CDATA[-1 - 1 - 1]";
    }
}
