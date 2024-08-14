using FeedbackEditor.Models.FC;
using FeedbackEditor.Services;
using FeedbackEditor.ViewModel.Timeline;
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
        public string ChannelName 
        { 
            get => FcFileService.Instance.GetActorName(FeedbackConfig);
            set => FcFileService.Instance.TrySetActorName(FeedbackConfig, value);
        }
        public Thickness OffsetOverride => new Thickness(20, 3, 3, 3);
        public ChannelType ChannelType { get; } = ChannelType.ACTOR;

        public FeedbackConfig FeedbackConfig { get; }

        public FeedbackConfigViewModel(FeedbackConfig feedbackConfig)
        {
            FeedbackConfig = feedbackConfig;
            if (ChannelName is null || ChannelName == "")
            {
                ChannelName = "Unnamed Actor";
            }
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
