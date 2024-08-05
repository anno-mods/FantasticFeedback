using DynamicData;
using FeedbackEditor.Models.FC;
using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.ViewModel.Nodes.SequenceActions;
using FeedbackEditor.ViewModel.Timeline;
using NodeNetwork.Toolkit;
using NodeNetwork;
using NodeNetwork.ViewModels;
using PropertyChanged;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace FeedbackEditor.ViewModel.Nodes
{
    public class LoopNodesViewModel
    {
        public NetworkViewModel Network { get; private set; }
        public EntryNodeViewModel EntryNode { get; }

        public event EventHandler<LoopNodesViewModel> ConnectionsUpdated = delegate { };

        public Loop Loop { get; }

        private List<SequenceActionNodeViewModel> _viewModels = new List<SequenceActionNodeViewModel>();

        public bool ContainsBranches { get => _viewModels.Any(x => x is BranchActionNodeViewModel); }

        public LoopNodesViewModel()
        {
            Network = new NetworkViewModel();
            EntryNode = new EntryNodeViewModel();
            Network.Nodes.Add(EntryNode);
            Loop = new Loop();
        }

        public LoopNodesViewModel(Loop loop) : this()
        {
            Loop = loop;

            Init(Loop.ElementContainer.Elements, EntryNode.FollowupActionOutput);

            Network.ConnectionsUpdated.Subscribe(x =>
            {
                RetrackSequenceFromNodes();
                ConnectionsUpdated?.Invoke(this, this);
            });

            Network.Validator = network =>
            {
                bool containsLoops = GraphAlgorithms.FindLoops(network).Any();
                if (containsLoops)
                {
                    return new NetworkValidationResult(false, false, new ErrorMessageViewModel("Network contains loops!"));
                }
                return new NetworkValidationResult(true, true, null);
            };
        }

        public void Init(IEnumerable<SequenceAction> sequenceActions, NodeOutputViewModel root_output)
        {
            SequenceActionNodeViewModel? previousNode = null;
            foreach (var action in sequenceActions.ToList())
            {
                var viewModel = NodeViewModelFactory.GetSequenceActionViewModel(action);
                CreateConnection(viewModel.PreviousActionInput, previousNode?.FollowupActionOutput ?? root_output);

                if (action is BranchAction branchAction)
                {
                    var branches = branchAction.BranchList.Select(x => x.pair2.Elements).Where(x => x is not null);
                    var branchViewModel = viewModel as BranchActionNodeViewModel;

                    foreach (var branch in branches)
                    {
                        Init(branch, branchViewModel?.BranchOutput!);
                    }
                }

                previousNode = viewModel;

                Network.Nodes.Add(viewModel);
                _viewModels.Add(viewModel);
            }
        }

        public void CreateConnection(NodeInputViewModel input, NodeOutputViewModel output)
        {
            var con = Network.ConnectionFactory(input, output);
            Network.Connections.Edit(x => x.Add(con));
        }

        public void RetrackSequenceFromNodes()
        {
            Loop.ElementContainer.Elements.Clear();
            IFollowupNode? node = EntryNode;

            AddSuccessors(node, Loop.ElementContainer.Elements);
        }

        public void AddSuccessors(IFollowupNode node, List<SequenceAction> sequences)
        {
            var followup = node.GetSuccessor();
            if (followup is null)
                return; 

            if(followup is SequenceActionNodeViewModel sequenceNode)
            {
                sequences.Add(sequenceNode.SequenceAction);
                AddSuccessors(followup, sequences);
            }

            if (followup is BranchActionNodeViewModel branchNode)
            {
                var branchAction = branchNode.SequenceAction as BranchAction;
                branchAction.BranchList.Clear();

                var succList = branchNode.GetBranchedSuccessors().ToList();
                foreach (var branched in succList)
                {
                    BranchEntry entry = new BranchEntry() { pair1 = 50, pair2=new() };
                    entry.pair2.Elements.Add(branched.SequenceAction);
                    AddSuccessors(branched, entry.pair2.Elements);
                    branchAction.BranchList.Add(entry);
                }
            }
        
        }
    }
}
