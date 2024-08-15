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
            get => FcFileService.Instance.CurrentFile.GetActorName(FeedbackConfig) ?? String.Empty;
            set => FcFileService.Instance.CurrentFile.TrySetActorName(FeedbackConfig, value);
        }
        public Thickness OffsetOverride => new Thickness(20, 3, 3, 3);
        public ChannelType ChannelType { get; } = ChannelType.ACTOR;

        public FeedbackConfig FeedbackConfig { get; }

        public FeedbackConfigViewModel(FeedbackConfig feedbackConfig)
        {
            FeedbackConfig = feedbackConfig;
            var index = 0;
            foreach (var sequenceDefinition in feedbackConfig.SequenceDefinitions)
            {
                var viewModel = new SequenceDefinitionViewModel(sequenceDefinition);
                viewModel.ChannelName = "Unnamed Sequence " + index;
                Childs.Add(viewModel);
            }
        }

        public void AddSequenceDefinition(SequenceDefinitionViewModel sequenceDefinitionViewModel)
        {
            Childs.Add(sequenceDefinitionViewModel);
        }

        public void RemoveSequenceDefinition(SequenceDefinitionViewModel sequenceDefinitionViewModel)
        {
            FeedbackConfig.RemoveSequenceDefinition(sequenceDefinitionViewModel.SequenceDefinition);
            Childs.Remove(sequenceDefinitionViewModel);
        }
    }
}
