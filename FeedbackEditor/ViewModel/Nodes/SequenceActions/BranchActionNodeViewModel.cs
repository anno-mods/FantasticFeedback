using DynamicData;
using DynamicData.Binding;
using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Views.Nodes;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using PropertyChanged;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
