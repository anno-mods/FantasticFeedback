using DynamicData;
using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Views.Nodes;
using NodeNetwork.ViewModels;
using PropertyChanged;
using ReactiveUI;

namespace FeedbackEditor.ViewModel.Nodes.SequenceActions
{
    [AddINotifyPropertyChangedInterface]
    public class BranchActionNodeViewModel : SequenceActionNodeViewModel, IBranchNode
    {
        static BranchActionNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new BranchNodeView(), typeof(IViewFor<BranchActionNodeViewModel>));
        }
        public BranchAction Action { get; set; }

        public NodeOutputViewModel BranchOutput { get; private set; } = new NodeOutputViewModel();

        public BranchActionNodeViewModel(BranchAction sequenceAction) : base(sequenceAction)
        {
            Name = "Branch";
            Action = sequenceAction;
            Outputs.Add(BranchOutput);
            BranchOutput.Name = "Branches";
        }

    }
}
