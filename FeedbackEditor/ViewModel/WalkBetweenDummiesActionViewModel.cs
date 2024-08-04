using FeedbackEditor.Models.FC.Actions;
using NodeNetwork.Views;
using PropertyChanged;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class WalkBetweenDummiesActionViewModel : SequenceActionViewModel
    {
        static WalkBetweenDummiesActionViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<WalkBetweenDummiesActionViewModel>));
        }

        public String DummyFrom { get; set; }
        public String DummyTo { get; set; }

        public WalkBetweenDummiesActionViewModel(WalkBetweenDummiesAction sequenceAction, int msOffset) : base(sequenceAction, msOffset)
        {
            DummyFrom = sequenceAction.StartDummy;
            DummyTo = sequenceAction.TargetDummy;
        }
    }
}
