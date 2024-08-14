using FeedbackEditor.Models.FC;
using NodeNetwork.ViewModels;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeLines;

namespace FeedbackEditor.ViewModel.Timeline
{
    [AddINotifyPropertyChangedInterface]
    public class SequenceDefinitionViewModel : TimeLinesDataBase, IChannel
    {
        public string ChannelName { get; set; } = "Unnamed Sequence";
        public Thickness OffsetOverride => new Thickness(30, 3, 3, 3);
        public ChannelType ChannelType { get; } = ChannelType.SEQUENCE;

        public SequenceDefinition SequenceDefinition { get; set; }

        public SequenceDefinitionViewModel(SequenceDefinition sequenceDefinition)
        {
            SequenceDefinition = sequenceDefinition;
            if (sequenceDefinition.Loop0 is Loop loop0)
            {
                var loop0Vm = new LoopViewModel(loop0);
                AddLoop(loop0Vm);
                loop0Vm.ChannelName = "Intro Loop";
            }
            if (sequenceDefinition.Loop1 is Loop loop1)
            {
                var loop1Vm = new LoopViewModel(loop1);
                AddLoop(loop1Vm);
                loop1Vm.ChannelName = "Running Loop";
            }
            if (sequenceDefinition.Loop2 is Loop loop2)
            {
                var loop2Vm = new LoopViewModel(loop2);
                AddLoop(loop2Vm);
                loop2Vm.ChannelName = "Outro Loop";
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
