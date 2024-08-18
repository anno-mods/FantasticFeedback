using FeedbackEditor.Models.FC;
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
    public class FeedbackDefinitionViewModel : TimeLinesDataBase, IChannel
    {
        public string ChannelName { get; set; } = "File";

        public Thickness OffsetOverride => new Thickness(20, 3, 3, 3);

        public ChannelType ChannelType => ChannelType.FEEDBACKDEFINITION;

        public ObservableCollection<FeedbackSequenceType> SequenceTypes { get; private set; }

        public FeedbackDefinition FeedbackDefinition { get; private set; }

        public FeedbackDefinitionViewModel(FeedbackDefinition feedbackDefinition)
        {
            SequenceTypes = new(feedbackDefinition.ValidSequenceIDs);
            SequenceTypes.CollectionChanged += ItemsManipulated;
            FeedbackDefinition = feedbackDefinition;
        }

        private void ItemsManipulated(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FeedbackDefinition.ValidSequenceIDs = SequenceTypes.ToList();
        }
    }
}
