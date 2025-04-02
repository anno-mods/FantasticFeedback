using DynamicData;
using FeedbackEditor.Models.FC;
using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Models.FC.Dummy;
using FeedbackEditor.ViewModel;
using FeedbackEditor.ViewModel.Nodes;
using FeedbackEditor.ViewModel.Nodes.SequenceActions;
using FeedbackEditor.ViewModel.Timeline;
using NodeNetwork.Toolkit.Layout.ForceDirected;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FeedbackEditor.Views
{
    /// <summary>
    /// Interaktionslogik für Sequence.xaml
    /// </summary>
    /// 
    [AddINotifyPropertyChangedInterface]
    public partial class Sequence : UserControl
    {
        private LoopNodesViewModel? _currentLoop;
        private NetworkViewModel _fallback = new NetworkViewModel();

        public NetworkViewModel Network { get; private set; }

        public bool HasNetwork { get; private set; }

        public bool ShowAddPanel 
        {
            get => _showAddPanel;
            set
            { 
                _showAddPanel = value;
                if (value)
                { 
                    ShowSpecialAddPanel = false;
                }
            }
        }
        private bool _showAddPanel;

        public bool ShowSpecialAddPanel 
        {
            get => _showSpecialAddPanel;
            set 
            {
                _showSpecialAddPanel = value;
                if (value)
                {
                    ShowAddPanel = false;
                }
            }
        }
        private bool _showSpecialAddPanel;

        private IDisposable? _disposeEvents;

        private ForceDirectedLayouter _layouter = new ForceDirectedLayouter();

        private const int AssumedNodeHeight = 600;
        private const int AssumedNodeWidth = 400;

        public Sequence()
        {
            DataContext = this;
            if (_currentLoop is null)
                Network = _fallback;
            InitializeComponent();
        }

        public void ShowLoop(Loop loop)
        {
            ShowAddPanel = false;
            _currentLoop = new LoopNodesViewModel(loop);
            Network = _currentLoop.Network;
            HasNetwork = true;
            LinearLayout();
            Network.ZoomFactor = 1;
        }

        public void ChangeListenerTo(LoopViewModel loopViewModel)
        {
            _disposeEvents?.Dispose();
            _disposeEvents = Network.ConnectionsUpdated.Subscribe(x =>
            {
                loopViewModel.Update();
            });
        }

        public void LinearLayout()
        {
            if (_currentLoop is null)
                return;
            Point Pos = new Point(50, 50);

            //Manually because we fucked up with the types here.
            TreeLayoutRecursive(_currentLoop.EntryNode);
        }

        public void TreeLayoutRecursive(NodeViewModel viewModel)
        {
            if (_currentLoop is null)
                return;
            if (viewModel is BranchActionNodeViewModel branchNode)
            {
                var followers = LayoutFollowerTree(branchNode);
                foreach (var follower in followers)
                    TreeLayoutRecursive(follower);
            }
            if (viewModel is IFollowupPositionableNode sequenceNode)
            {
                var next = LayoutFollowerLinear(sequenceNode);
                TreeLayoutRecursive(next);
            }
        }

        private SequenceActionNodeViewModel LayoutFollowerLinear(IFollowupPositionableNode fixedNode)
        {
            var followup = fixedNode.FollowupActionOutput.Connections.Items.Select(x => x.Input.Parent).FirstOrDefault() as SequenceActionNodeViewModel;
            if (followup is null)
                return null;
            followup.Position = new Point(fixedNode.Position.X + AssumedNodeWidth, fixedNode.Position.Y);
            return followup;
        }

        private IEnumerable<SequenceActionNodeViewModel> LayoutFollowerTree(BranchActionNodeViewModel branchAction)
        {
            var followups = branchAction.BranchOutput.Connections.Items.Select(x => x.Input.Parent).Cast<SequenceActionNodeViewModel>();

            float index = (followups.Count() / 2f) - followups.Count() + 0.5f;
            foreach (var followup in followups)
            {
                followup.Position = new Point(branchAction.Position.X + AssumedNodeWidth, branchAction.Position.Y + index*AssumedNodeHeight );
                index += 1;
            }
            return followups;
        }

        public void ShowDefaultNodesView()
        {
            _disposeEvents?.Dispose();
            Network = _fallback;
            HasNetwork = false;
            _currentLoop = null;
        }

        private System.Windows.Point ComputeNodeLocation()
        {
            var screen_center = new Point(NetworkView.NetworkViewportRegion.X + (NetworkView.NetworkViewportRegion.Width / 2), NetworkView.NetworkViewportRegion.Y + (NetworkView.NetworkViewportRegion.Height /2));
            return screen_center;
            //return new Point(0, 0);
        } 

        private async void OnLayoutButtonClick(object sender, RoutedEventArgs e)
        {
            if (_currentLoop is null)
                return;

            var config = new Configuration()
            {
                Network = Network,
                NodeRepulsionForce = 40,
                EquilibriumDistance = node => 0.9,
                RowForce = node => 1000,
                NodeMass = node => node is BranchActionNodeViewModel ? 20 : 10,
                IsFixedNode = (node) => node is EntryNodeViewModel
            };
            var cts = new CancellationTokenSource();
            cts.CancelAfter(1000);
            try
            {
                await _layouter.LayoutAsync(config, cts.Token);
            }
            catch (TaskCanceledException)
            { 
                //this is expected you dumbass;
            }
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            //autohiding of SpecialAddPanel is taken care of by automatic setting of ShowSpecialAddPanel
            ShowAddPanel = !ShowAddPanel;
        }

        private void OnSpecialAddButtonClick(object sender, RoutedEventArgs e)
        {
            //autohiding of AddPanel is taken care of by automatic setting of ShowAddPanel
            ShowSpecialAddPanel = !ShowSpecialAddPanel;
        }

        private void OnAnyAddButtonClick<T>(object sender, RoutedEventArgs e) where T : SequenceAction, new()
        {
            var screen_center = ComputeNodeLocation();
            if (HasNetwork)
            {
                _currentLoop?.AddEmpty<T>(screen_center);
            }
            ShowAddPanel = false;
        }

        /// <summary>
        /// Generates a Walk Sequence in the Current Network. Nothing happens if there is no Network active.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="group"></param>
        private void CreateWalkSequence(WalkBetweenDummiesAction template, DummyGroup group, bool addStartDummies)
        {
            var screen_center = ComputeNodeLocation();
            if (!HasNetwork)
                return;
            var actions = _currentLoop?.AddSequence<WalkBetweenDummiesAction>(group.Dummies.Count() - 1, screen_center, AssumedNodeWidth);
            
            if (actions is null)
                return;

            var list = group.Dummies;

            list = list.OrderBy(x =>
            {
                if (!x.Name.Contains("_"))
                    return -1;
                var suffix = x.Name.Split("_").LastOrDefault();
                if (int.TryParse(suffix, out var suffixValue))
                {
                    return suffixValue;
                }
                return -1; 
            }).ToList();

            for (int i = 1; i < list.Count(); i++)
            {
                //first element is always skipped, so actions are all offset by -1
                var action = actions.ElementAt(i-1);
                action.WalkFromCurrentPosition = template.WalkFromCurrentPosition;
                action.WalkSequence = template.WalkSequence;

                action.TargetDummy = list[i].Name;
                action.TargetDummyId = list[i].Id;

                action.SpeedFactorF = template.SpeedFactorF;

                if (addStartDummies)
                {
                    action.StartDummy = list[i - 1].Name;
                    action.StartDummyId = list[i - 1].Id;                    
                }
            }
        }

        private void OnGenerateWalkSequenceButtonClick(object sender, RoutedEventArgs e)
        {
            var popup = new CreateWalksequencePopup();
            if (popup.ShowDialog() is not true)
                return;

            CreateWalkSequence(popup.ActionTemplate, popup.SelectedGroup, popup.ExplicitStartDummies);
        }

        private void OnAddSequenceButtonClick(object sender, RoutedEventArgs e)
            => OnAnyAddButtonClick<PlaySequenceAction>(sender, e);

        private void OnAddWalkButtonClick(object sender, RoutedEventArgs e)
            => OnAnyAddButtonClick<WalkBetweenDummiesAction>(sender, e);

        private void OnAddBranchButtonClick(object sender, RoutedEventArgs e)
            => OnAnyAddButtonClick<BranchAction>(sender, e);

        private void OnAddPlayAnyButtonClick(object sender, RoutedEventArgs e)
            => OnAnyAddButtonClick<PlayAnySequenceAction>(sender, e);

        private void OnAddFadeButtonClick(object sender, RoutedEventArgs e)
            => OnAnyAddButtonClick<FadeAction>(sender, e);

        private void OnAddWaitButtonClick(object sender, RoutedEventArgs e)
            => OnAnyAddButtonClick<WaitAction>(sender, e);

        private void OnAddScaleButtonClick(object sender, RoutedEventArgs e)
            => OnAnyAddButtonClick<ScaleAction>(sender, e);

        private void OnAddBarrierButtonClick(object sender, RoutedEventArgs e)
            => OnAnyAddButtonClick<BarrierAction>(sender, e);

        private void OnAddTurnButtonClick(object sender, RoutedEventArgs e)
            => OnAnyAddButtonClick<TurnAction>(sender, e);

        private void OnAddWalkRandomButtonClick(object sender, RoutedEventArgs e)
            => OnAnyAddButtonClick<WalkRandomAction>(sender, e);

        private void OnAddPositionRandomButtonClick(object sender, RoutedEventArgs e)
            => OnAnyAddButtonClick<PositionRandomAction>(sender, e);

        private void NetworkView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

        private void NetworkView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowAddPanel = false;
        }
    }
}
