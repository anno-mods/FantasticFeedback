using DynamicData;
using DynamicData.Binding;
using FeedbackEditor.Models.FC.Actions;
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
    public class SequenceActionViewModel : NodeViewModel, ITimeLineData, IChannelItem
    {
        static SequenceActionViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<SequenceActionViewModel>));
        }

        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool? bResizable => false;

        public int Width { get; } = 100;

        public ActionType ActionType { get; }

        public NodeInputViewModel PreviousActionInput { get; }
        public NodeOutputViewModel FollowupActionOutput { get; }

        public SequenceAction SequenceAction { get; }

        public SequenceActionViewModel()
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

        public SequenceActionViewModel(SequenceAction sequenceAction) : this()
        {
            Name = sequenceAction.GetType().Name;
            StartTime = TimeSpan.FromMilliseconds(0);
            EndTime = TimeSpan.FromMilliseconds(Width);
            ActionType = sequenceAction.ElementType;
            SequenceAction = sequenceAction;
        }

        public void MoveAfter(SequenceActionViewModel sequenceActionViewModel)
        {
            StartTime = sequenceActionViewModel.EndTime;
            EndTime = StartTime!.Value.Add(TimeSpan.FromMilliseconds(Width));
        }
    }
}
