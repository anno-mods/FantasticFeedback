using FeedbackEditor.Models.FC.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLines;

namespace FeedbackEditor.ViewModel
{
    public class SequenceActionViewModel : ITimeLineData, IChannelItem
    {
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool? bResizable => false;

        public string Name { get; set; }

        public SequenceActionViewModel(SequenceAction sequenceAction, int msOffset)
        {
            Name = sequenceAction.GetType().Name;
            StartTime = TimeSpan.FromMilliseconds(msOffset);
            EndTime = TimeSpan.FromMilliseconds(msOffset+100);
        }
    }
}
