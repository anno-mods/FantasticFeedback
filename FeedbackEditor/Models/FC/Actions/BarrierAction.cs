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
    public class BarrierAction : SequenceAction
    {
        public int BarrierId { get; set; }
        public int TimeOffset { get; set; }

        public BarrierAction()
        {
            ElementType = ActionType.BARRIER;
        }
    }
}
