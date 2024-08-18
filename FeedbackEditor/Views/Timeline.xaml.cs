using FeedbackEditor.Models.FC;
using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Services;
using FeedbackEditor.ViewModel;
using FeedbackEditor.ViewModel.Timeline;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        public ObservableCollection<TimeLinesDataBase> Channels { get; set; } = new();

        private LoopViewModel? _selectedLoop;
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
        public bool CanAddActor { get; private set; }

        public FeedbackConfigViewModel? SelectedActor { get; private set; }
        public FeedbackDefinitionViewModel? SelectedFile { get; private set; }
        public SequenceDefinitionViewModel? SelectedSequence { get; private set; }

        private TimeLinesDataBase? SelectedTimeLinesData = null; 

        [DependsOn(nameof(SelectedActor))]
        public bool CanDelete { get => SelectedActor is not null || SelectedSequence is not null; }

        public Timeline()
        {
            InitializeComponent();
            DataContext = this;
            Timelines.ItemsSource = Channels;

            FcFileService.Instance.FileLoaded += (sender, file) => {
                Channels.Clear();
                Channels.Add(new FeedbackDefinitionViewModel(file.FeedbackDefinition));
                file.FeedbackDefinition.FeedbackConfigs.ForEach(x =>
                {
                    var vm = new FeedbackConfigViewModel(x);
                    Channels.Add(vm);
                });
            };
        }

        private void ChooseLoopClick(object sender, RoutedEventArgs e)
        {
            if (sender is not RadioButton button)
                return;

            CanAddSequence = button.DataContext is FeedbackConfigViewModel;
            SelectedActor = CanAddSequence ? button.DataContext as FeedbackConfigViewModel : null;
            SelectedFile = button.DataContext as FeedbackDefinitionViewModel;
            CanAddActor = SelectedFile is not null;
            SelectedLoop = button.DataContext as LoopViewModel;
            SelectedSequence = button.DataContext as SequenceDefinitionViewModel;
            SelectedTimeLinesData = button.DataContext as TimeLinesDataBase;

            SelectedActor?.InitFeedbackLoops();
        }

        private void AddActorButtonClick(object sender, RoutedEventArgs e)
        {
            if (FcFileService.Instance.CurrentFile is null)
                return;
            var feedbackConfig = new FeedbackConfig();
            feedbackConfig.CreateNewSequenceDefinition();
            FcFileService.Instance.CurrentFile.AddActor(feedbackConfig, "Unnamed Actor");
            var vm = new FeedbackConfigViewModel(feedbackConfig);
            Channels.Add(vm);
            Timelines.Redraw();
        }

        private void AddSequenceButtonClick(object sender, RoutedEventArgs e)
        {
            if (FcFileService.Instance.CurrentFile is null)
                return;

            var sequenceDefinition = SelectedActor?.FeedbackConfig.CreateNewSequenceDefinition();
            var viewModel = new SequenceDefinitionViewModel(sequenceDefinition);
            viewModel.ChannelName += " " + (SelectedActor?.FeedbackConfig.SequenceDefinitions.Count - 1);
            SelectedActor?.AddSequenceDefinition(viewModel);
            if(SelectedTimeLinesData is not null)
                SelectedTimeLinesData.IsExpanded = true;

            //Redraw Timelines
            Timelines.CreateTimelineControls();
            Timelines.Redraw();
        }

        private void RemoveButtonClick(object sender, RoutedEventArgs e)
        {
            if (FcFileService.Instance.CurrentFile is null)
                return;

            if (SelectedActor is not null)
            {
                FcFileService.Instance.CurrentFile.RemoveActor(SelectedActor.FeedbackConfig);
                Channels.Remove(SelectedActor);
                SelectedActor = null;
                CanAddSequence = false;
            }
            else if (SelectedSequence is not null)
            {
                var actor = GetParentActor(SelectedSequence);
                actor?.Childs.Remove(SelectedSequence);
                actor?.FeedbackConfig.RemoveSequenceDefinition(SelectedSequence.SequenceDefinition);
                SelectedSequence = null;
            }
            Timelines.CreateTimelineControls();
            Timelines.Redraw();
        }

        private FeedbackConfigViewModel? GetParentActor(SequenceDefinitionViewModel sequenceAction)
        {
            return Channels.FirstOrDefault(x => x.Childs.Contains(sequenceAction)) as FeedbackConfigViewModel;        
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
