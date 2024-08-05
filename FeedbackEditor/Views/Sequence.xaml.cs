using DynamicData;
using FeedbackEditor.Models.FC;
using FeedbackEditor.Models.FC.Actions;
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

        public bool ShowAddPanel { get; set; }

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
            NetworkView.CenterAndZoomView();
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
            ShowAddPanel = !ShowAddPanel;

        }

        private void NetworkView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void NetworkView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowAddPanel = false;
        }
    }
}
