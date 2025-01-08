using PropertyChanged;
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
    [AddINotifyPropertyChangedInterface]
    public class WalkRandomAction : SequenceAction
    {
        public float TurnAngleF { get; set; }

        [XmlIgnore]
        public SequenceID WalkSequence { get; set; }

        public String CenterDummy { get; set; }

        public int CenterDummyId { get; set; }

        [XmlElement("RandomRadiusF")]
        public float RandomRadius { get; set; }

        public bool WalkFromCurrentPosition { get; set; }

        public bool UseTargetDirection { get; set; }

        [XmlElement("TargetDirectionF")]
        public float TargetDirection { get; set; }

        [XmlElement("WalkSequence")]
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int WalkSequenceForSerializing
        {
            get => (int)WalkSequence;
            set => WalkSequence = (SequenceID)value;
        }

        public WalkRandomAction() 
        {
            ElementType = ActionType.WALK_RANDOM;
        }
    }
}
