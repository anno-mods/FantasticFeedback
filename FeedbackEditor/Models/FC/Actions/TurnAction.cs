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
    public class TurnAction : SequenceAction
    {
        public float TurnAngleF { get; set; }

        [XmlIgnore]
        public SequenceID TurnSequence { get; set; }

        public String TurnToDummy { get; set; }

        public int TurnToDummyId { get; set; }

        [XmlElement("TurnSequence")]
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int TurnSequenceForSerializing
        {
            get => (int)TurnSequence;
            set => TurnSequence = (SequenceID)value;
        }

        public TurnAction() 
        {
            ElementType = ActionType.TURN_TO;
        }
    }
}
