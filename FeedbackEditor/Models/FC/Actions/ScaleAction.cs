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
    public class ScaleAction : SequenceAction
    {
        [XmlElement("m_MinScaleFactor")]
        public float MinScaleFactor { get; set; } = 1.0f;

        [XmlElement("m_MaxScaleFactor")]
        public float MaxScaleFactor { get; set; } = 1.0f;

        public ScaleAction()
        {
            ElementType = ActionType.SCALE;
        }
    }
}
