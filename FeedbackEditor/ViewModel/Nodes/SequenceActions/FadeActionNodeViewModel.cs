using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Views.Nodes;
using NodeNetwork.Views;
using PropertyChanged;
using ReactiveUI;

namespace FeedbackEditor.ViewModel.Nodes.SequenceActions
{
    [AddINotifyPropertyChangedInterface]
    public class FadeActionNodeViewModel : SequenceActionNodeViewModel
    {
        static FadeActionNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new FadeNodeView(), typeof(IViewFor<FadeActionNodeViewModel>));
        }
        public FadeAction Action { get; set; }

        public FadeActionNodeViewModel(FadeAction sequenceAction) : base(sequenceAction)
        {
            Name = "Fade In/Out";
            Action = sequenceAction;
        }

    }
}