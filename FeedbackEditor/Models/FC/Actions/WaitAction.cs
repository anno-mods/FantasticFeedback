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
    public class WaitAction : SequenceAction
    {
        public int MinTime { get; set; }
        public int MaxTime { get; set; }

        public WaitAction() 
        {
            ElementType = ActionType.WAIT;
        }
    }
}
