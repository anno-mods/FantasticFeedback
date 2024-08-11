using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Views.Nodes;
using PropertyChanged;
using ReactiveUI;

namespace FeedbackEditor.ViewModel.Nodes.SequenceActions
{
    [AddINotifyPropertyChangedInterface]
    public class BarrierActionNodeViewModel : SequenceActionNodeViewModel
    {
        static BarrierActionNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new BarrierNodeView(), typeof(IViewFor<BarrierActionNodeViewModel>));
        }
        public BarrierAction Action { get; set; }

        public BarrierActionNodeViewModel(BarrierAction sequenceAction) : base(sequenceAction)
        {
            Name = "Barrier";
            Action = sequenceAction;
        }

    }
}
