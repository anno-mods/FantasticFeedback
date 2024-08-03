using FeedbackEditor.Models.FC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeLines;

namespace FeedbackEditor.ViewModel
{
    public class SequenceDefinitionViewModel : TimeLinesDataBase, IChannel
    {
        public string ChannelName { get; set; } = "Unnamed Sequence";
        public Thickness OffsetOverride => new Thickness(30, 3, 3, 3);
        public ChannelType ChannelType { get; } = ChannelType.SEQUENCE;

        public SequenceDefinitionViewModel() { 
        
        }

        public SequenceDefinitionViewModel(SequenceDefinition sequenceDefinition) : this()
        {
            var loopCount = 0;
            foreach (var loop in sequenceDefinition.Loops) 
            {
                var loopViewModel = new LoopViewModel(loop);
                loopViewModel.ChannelName = "Loop" + loopCount;
                AddLoop(loopViewModel);
                loopCount++;
            }
        }

        public void AddLoop(LoopViewModel loopViewModel)
        {
            Childs.Add(loopViewModel);
        }

        public void RemoveLoop(LoopViewModel loopViewModel)
        {
            Childs.Remove(loopViewModel);
        }
    }
}
