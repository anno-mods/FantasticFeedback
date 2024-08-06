using FeedbackEditor.Models.FC.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLines;

namespace FeedbackEditor.ViewModel.Timeline
{
    public class SequenceActionTimelineViewModel : ITimeLineData, IChannelItem
    {
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool? bResizable => false;

        public int Width 
        {
            get => SequenceAction is not BranchAction branchAction || !branchAction.BranchList.Any()
                ? 100
                : branchAction.BranchList.Max(x => x.pair2.Elements.Count()) * 100;                
            }

        public string Name { get => SequenceAction.ElementType.ToString(); }

        public SequenceAction SequenceAction { get; }

        public SequenceActionTimelineViewModel()
        {
            StartTime = TimeSpan.FromMilliseconds(0);
            EndTime = TimeSpan.FromMilliseconds(Width);
            SequenceAction = new SequenceAction();
        }

        public SequenceActionTimelineViewModel(SequenceAction sequenceAction) : this()
        {
            SequenceAction = sequenceAction;
        }

        public void MoveAfter(SequenceActionTimelineViewModel viewModel)
        {
            StartTime = viewModel.EndTime;
            EndTime = StartTime!.Value.Add(TimeSpan.FromMilliseconds(Width));
        }
    }
}
