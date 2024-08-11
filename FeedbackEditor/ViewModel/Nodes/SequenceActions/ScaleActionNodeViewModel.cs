using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Views.Nodes;
using PropertyChanged;
using ReactiveUI;

namespace FeedbackEditor.ViewModel.Nodes.SequenceActions
{
    [AddINotifyPropertyChangedInterface]
    public class ScaleActionNodeViewModel : SequenceActionNodeViewModel
    {
        static ScaleActionNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new ScaleNodeView(), typeof(IViewFor<ScaleActionNodeViewModel>));
        }
        public ScaleAction Action { get; set; }

        public ScaleActionNodeViewModel(ScaleAction sequenceAction) : base(sequenceAction)
        {
            Name = "Scale";
            Action = sequenceAction;
        }

    }
}
