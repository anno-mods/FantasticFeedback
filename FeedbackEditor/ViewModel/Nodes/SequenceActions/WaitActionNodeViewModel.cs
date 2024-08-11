using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Views.Nodes;
using PropertyChanged;
using ReactiveUI;

namespace FeedbackEditor.ViewModel.Nodes.SequenceActions
{
    [AddINotifyPropertyChangedInterface]
    public class WaitActionNodeViewModel : SequenceActionNodeViewModel
    {
        static WaitActionNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new WaitNodeView(), typeof(IViewFor<WaitActionNodeViewModel>));
        }
        public WaitAction Action { get; set; }

        public WaitActionNodeViewModel(WaitAction sequenceAction) : base(sequenceAction)
        {
            Name = "Wait Time";
            Action = sequenceAction;
        }

    }
}
