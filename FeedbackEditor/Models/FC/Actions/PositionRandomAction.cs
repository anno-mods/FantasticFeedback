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
    public class PositionRandomAction : SequenceAction
    {
        [XmlIgnore]
        public SequenceID WalkSequence { get; set; }

        public String CenterDummy { get; set; }

        public int CenterDummyId { get; set; }

        [XmlElement("RandomRadiusF")]
        public float RandomRadius { get; set; }

        public bool UseRandomDirection { get; set; }

        [XmlElement("RotationY")]
        public float Rotation { get; set; }


        [XmlElement("WalkSequence")]
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int WalkSequenceForSerializing
        {
            get => (int)WalkSequence;
            set => WalkSequence = (SequenceID)value;
        }

        public PositionRandomAction() 
        {
            ElementType = ActionType.TURN_RANDOM;
        }
    }
}
