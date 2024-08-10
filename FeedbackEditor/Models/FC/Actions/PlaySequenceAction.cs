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
    public class PlaySequenceAction : SequenceAction
    {
        [XmlElement("m_IdleSequenceID")]
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        [DoNotNotify]
        public int SequenceIDForSerializing
        {
            get => (int)IdleSequenceID;
            set => IdleSequenceID = (SequenceID)value;
        }

        [XmlIgnore]
        public SequenceID IdleSequenceID { get; set; }

        [XmlIgnore]
        [DoNotNotify]
        public override ActionType ElementType { get; set; } = ActionType.PLAY_SEQUENCE;

        public int MinPlayCount { get; set; } = 1;
        public int MinPlayTime { get; set; } = 0;
        public int MaxPlayCount { get; set; } = 0;
        public int MaxPlayTime { get; set; } = 0;
        public bool ResetStartTime { get; set; } = false;
    }
}
