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
