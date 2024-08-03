using FeedbackEditor.Models.FC.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.ViewModel
{
    public class WalkBetweenDummiesActionViewModel : SequenceActionViewModel
    {
        public String DummyFrom { get; set; }
        public String DummyTo { get; set; }

        public WalkBetweenDummiesActionViewModel(WalkBetweenDummiesAction sequenceAction, int msOffset) : base(sequenceAction, msOffset)
        {
            DummyFrom = sequenceAction.StartDummy;
            DummyTo = sequenceAction.TargetDummy;
        }
    }
}
