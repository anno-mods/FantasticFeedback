using FeedbackEditor.Models.FC;
using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Services;
using FeedbackEditor.ViewModel;
using FeedbackEditor.ViewModel.Timeline;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TimeLines;

namespace FeedbackEditor.Views
{
    /// summary>
    /// Interaktionslogik für Timeline.xaml
    ///  <summary>
    /// summary>
    /// </summary>
    /// 
    [AddINotifyPropertyChangedInterface]
    public partial class Timeline : UserControl
    {
        public ObservableCollection<FeedbackConfigViewModel> FeedbackConfigs { get; set; } = new();

        [DoNotNotify]
        private LoopViewModel? _selectedLoop;
        [DoNotNotify]
        public LoopViewModel? SelectedLoop
        {
            get => _selectedLoop;
            private set
            {
                _selectedLoop = value;
                SelectionChanged.Invoke(this, SelectedLoop);
            }
        }

        public event EventHandler<LoopViewModel?> SelectionChanged = delegate { };

        public bool CanAddSequence { get; private set; }

        public FeedbackConfigViewModel? SelectedActor { get; private set; }
        public SequenceDefinitionViewModel? SelectedSequence { get; private set; }

        private TimeLinesDataBase? SelectedTimeLinesData = null; 

        [DependsOn(nameof(SelectedActor))]
        public bool CanDelete { get => SelectedActor is not null; }

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

        private void ChooseLoopClick(object sender, RoutedEventArgs e)
        {
            if (sender is not RadioButton button)
                return;

            CanAddSequence = button.DataContext is FeedbackConfigViewModel feedbackConfigViewModel;
            SelectedActor = CanAddSequence ? button.DataContext as FeedbackConfigViewModel : null;
            if (button.DataContext is LoopViewModel viewModel)
            {
                if (viewModel is null)
                    return;

                if (viewModel == SelectedLoop)
                    return;

                SelectedLoop = viewModel;
                SelectedSequence = null;
            }
            else if (button.DataContext is SequenceDefinitionViewModel sequenceDefinition)
            {
                SelectedLoop = null;
                SelectedSequence = sequenceDefinition;
            }
            SelectedTimeLinesData = button.DataContext as TimeLinesDataBase;
        }

        private void AddActorButtonClick(object sender, RoutedEventArgs e)
        {
            if (FcFileService.Instance.CurrentFile is null)
                return;
            var feedbackConfig = new FeedbackConfig();
            feedbackConfig.SequenceDefinitions.Add(new SequenceDefinition()
            {
                Loop0 = new Loop(),
                Loop1 = new Loop(),
                Loop2 = new Loop()
            });
            FcFileService.Instance.AddActor(feedbackConfig, "Unnamed Actor");
            var vm = new FeedbackConfigViewModel(feedbackConfig);
            FeedbackConfigs.Add(vm);
            Timelines.Redraw();
        }

        private void AddSequenceButtonClick(object sender, RoutedEventArgs e)
        {
            if (FcFileService.Instance.CurrentFile is null)
                return;

            var sequenceDefinition = new SequenceDefinition()
            {
                Loop0 = new Loop(),
                Loop1 = new Loop(),
                Loop2 = new Loop()
            };
            var viewModel = new SequenceDefinitionViewModel(sequenceDefinition);
            SelectedActor?.AddSequenceDefinition(viewModel);
            if(SelectedTimeLinesData is not null)
                SelectedTimeLinesData.IsExpanded = true;
            Timelines.CreateTimelineControls();
            Timelines.Redraw();
        }

        private void RemoveButtonClick(object sender, RoutedEventArgs e)
        {
            if (FcFileService.Instance.CurrentFile is null)
                return;

            if (SelectedActor is not null)
            {
                FcFileService.Instance.RemoveActor(SelectedActor.FeedbackConfig);
                FeedbackConfigs.Remove(SelectedActor);
                SelectedActor = null;
                CanAddSequence = false;
            }
            Timelines.CreateTimelineControls();
            Timelines.Redraw();
        }
    }

    public class TempDataTemplateSeletor : DataTemplateSelector
    {
        public DataTemplate PlaySequenceAction { get; set; }
        public DataTemplate WalkBetweenDummiesAction { get; set; }
        public DataTemplate GenericSequenceAction { get; set; }
        public DataTemplate BranchAction { get; set; }
        public DataTemplate PlayAnySequenceAction { get; set; }
        public DataTemplate FadeAction { get; set; }
        public DataTemplate ScaleAction { get; set; }
        public DataTemplate WaitAction { get; set; }
        public DataTemplate BarrierAction { get; set; }
        public DataTemplate TurnAction { get; set; }

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
                ActionType.PLAY_ANY_SEQUENCE => PlayAnySequenceAction,
                ActionType.FADE => FadeAction,
                ActionType.WAIT => WaitAction,
                ActionType.TURN_TO => TurnAction,
                ActionType.BARRIER => BarrierAction,
                ActionType.SCALE => ScaleAction,
                _ => GenericSequenceAction
            };
        }
    }
}
