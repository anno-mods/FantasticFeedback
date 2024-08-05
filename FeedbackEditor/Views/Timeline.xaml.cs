using FeedbackEditor.Models.FC;
using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Services;
using FeedbackEditor.ViewModel;
using FeedbackEditor.ViewModel.Timeline;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FeedbackEditor.Views
{
    /// summary>
    /// Interaktionslogik für Timeline.xaml
    /// 
    public partial class Timeline : UserControl
    {
        public ObservableCollection<FeedbackConfigViewModel> FeedbackConfigs { get; set; } = new();

        private LoopViewModel? _selectedLoop; 
        public LoopViewModel? SelectedLoop 
        { 
            get => _selectedLoop; 
            private set 
            { 
                _selectedLoop= value;
                SelectionChanged.Invoke(this, SelectedLoop);
            }
        }

        public event EventHandler<LoopViewModel?> SelectionChanged = delegate { };

        public Timeline()
        {
            InitializeComponent();
            DataContext = this;
            Timelines.ItemsSource = FeedbackConfigs;

            FcFileService.Instance.FileLoaded += (sender, file) => {
                FeedbackConfigs.Clear();
                file.FeedbackDefinition.FeedbackConfigs.ForEach(x =>
                {
                    var vm = new FeedbackConfigViewModel(x);
                    FeedbackConfigs.Add(vm);
                });
            };
        }

        private void ChooseLoop(object sender, RoutedEventArgs e)
        {
            if (sender is not RadioButton button)
                return;

            if (button.DataContext is LoopViewModel viewModel)
            {
                if (viewModel is null)
                    return;

                if (viewModel == SelectedLoop)
                    return;

                SelectedLoop = viewModel;
            }
            else if (button.DataContext is SequenceDefinitionViewModel sequenceDefinition)
            {
                SelectedLoop = null;
            }
        }
    }

    public class TempDataTemplateSeletor : DataTemplateSelector
    {
        public DataTemplate PlaySequenceAction { get; set; }
        public DataTemplate WalkBetweenDummiesAction { get; set; }
        public DataTemplate GenericSequenceAction { get; set; }
        public DataTemplate BranchAction { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is not SequenceActionTimelineViewModel timelineViewModel)
            {
                return null;
            }

            return timelineViewModel.SequenceAction.ElementType switch
            {
                ActionType.PLAY_SEQUENCE => PlaySequenceAction,
                ActionType.WALK_BETWEEN_DUMMIES => WalkBetweenDummiesAction,
                ActionType.BRANCH => BranchAction,
                _ => GenericSequenceAction
            };
        }
    }
}
