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
    public class FadeAction : SequenceAction
    {
        public bool ShowObject { get; set; }
        public int FadingMode { get; set; } = 2;
        public int TimeOffset { get; set; }

        public FadeAction()
        {
            ElementType = ActionType.FADE;        
        }
    }
}
