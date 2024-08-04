using FeedbackEditor.Models.FC;
using PropertyChanged;
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
    [AddINotifyPropertyChangedInterface]
    public class FeedbackConfigViewModel : TimeLinesDataBase, IChannel
    {
        public string ChannelName { get; set; } = "Unnamed Actor";
        public Thickness OffsetOverride => new Thickness(20, 3, 3, 3);
        public ChannelType ChannelType { get; } = ChannelType.ACTOR;

        public FeedbackConfigViewModel()
        {

        }

        public FeedbackConfigViewModel(FeedbackConfig feedbackConfig) : this() 
        {
            foreach (var sequenceDefinition in feedbackConfig.SequenceDefinitions)
            {
                AddSequenceDefinition(new SequenceDefinitionViewModel(sequenceDefinition));
            }

        }

        public void AddSequenceDefinition(SequenceDefinitionViewModel sequenceDefinitionViewModel)
        {
            Childs.Add(sequenceDefinitionViewModel);
        }

        public void RemoveSequenceDefinition(SequenceDefinitionViewModel sequenceDefinitionViewModel)
        {
            Childs.Remove(sequenceDefinitionViewModel);
        }
    }
}
