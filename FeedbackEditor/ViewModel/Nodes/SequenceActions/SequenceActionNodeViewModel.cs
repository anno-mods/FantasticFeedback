using DynamicData;
using DynamicData.Binding;
using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.ViewModel.Nodes;
using FeedbackEditor.ViewModel.Timeline;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using PropertyChanged;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TimeLines;

namespace FeedbackEditor.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class SequenceActionNodeViewModel : NodeViewModel, IFollowupPositionableNode
    {
        static SequenceActionNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<SequenceActionNodeViewModel>));
        }

        public ActionType ActionType { get; }

        public NodeInputViewModel PreviousActionInput { get; }
        public NodeOutputViewModel FollowupActionOutput { get; }

        public SequenceAction SequenceAction { get; }

        public SequenceActionNodeViewModel()
        {
            PreviousActionInput = new NodeInputViewModel();
            PreviousActionInput.Name = "Previous Action";

            FollowupActionOutput = new NodeOutputViewModel();
            FollowupActionOutput.Name = "Next Action";
            FollowupActionOutput.MaxConnections = 1; 


            Inputs.Add(PreviousActionInput);
            Outputs.Add(FollowupActionOutput);

            SequenceAction = new SequenceAction(); 
        }

        public SequenceActionNodeViewModel(SequenceAction sequenceAction) : this()
        {
            Name = sequenceAction.GetType().Name;
            ActionType = sequenceAction.ElementType;
            SequenceAction = sequenceAction;
        }
    }
}
