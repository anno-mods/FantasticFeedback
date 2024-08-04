using DynamicData;
using FeedbackEditor.Models.FC.Actions;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using PropertyChanged;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionType ActionType { get; }

        public SequenceActionViewModel(SequenceAction sequenceAction, int msOffset)
        {
            Name = sequenceAction.GetType().Name;
            StartTime = TimeSpan.FromMilliseconds(msOffset);
            EndTime = TimeSpan.FromMilliseconds(msOffset+100);
            ActionType = sequenceAction.ElementType;

            var prev = new NodeInputViewModel();
            prev.Name = "Previous Action";
            
            var next = new NodeOutputViewModel();
            next.Name = "Next Action";

            Inputs.Add(prev);
            Outputs.Add(next);
        }
    }
}
