using DynamicData;
using FeedbackEditor.Models.FC;
using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.ViewModel;
using NodeNetwork.ViewModels;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeLines;

namespace FeedbackEditor.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class LoopViewModel : TimeLinesDataBase, IChannel
    {
        public Thickness OffsetOverride => new Thickness(40, 3, 3, 3);
        public string ChannelName { get; set; } = "Unnamed Loop";
        public ChannelType ChannelType { get; } = ChannelType.LOOP;

        public NetworkViewModel Network { get; private set; }
        public EntryNodeViewModel EntryNode { get; }

        private List<SequenceActionViewModel> _viewModels = new List<SequenceActionViewModel>();

        public Loop Loop { get; }

        public event EventHandler<LoopViewModel> ConnectionsUpdated = delegate { };

        public LoopViewModel()
        {
            Network = new NetworkViewModel();
            EntryNode = new EntryNodeViewModel();
            Network.Nodes.Add(EntryNode);
            Loop = new Loop(); 
        }

        public LoopViewModel(Loop loop) : this()
        {
            Loop = loop;
            SequenceActionViewModel? previousNode = null;

            foreach (var action in loop.ElementContainer.Elements)
            {
                var viewModel = AddSequenceAction(action);
                CreateConnection(viewModel.PreviousActionInput, previousNode?.FollowupActionOutput ?? EntryNode.FollowupActionOutput);

                if (previousNode is not null)
                    viewModel.MoveAfter(previousNode);
                previousNode = viewModel;

                Network.Nodes.Add(viewModel);
                _viewModels.Add(viewModel);
            }

            Network.ConnectionsUpdated.Subscribe(x =>
            {
                RetrackSequenceFromNodes();
                ConnectionsUpdated?.Invoke(this, this); 
            });
        }

        public void CreateConnection(NodeInputViewModel input, NodeOutputViewModel output)
        {
            var con = Network.ConnectionFactory(input, output);
            Network.Connections.Edit(x => x.Add(con));
        }

        public SequenceActionViewModel AddSequenceAction(SequenceAction action)
        {
            var viewModel = new SequenceActionViewModel(action);
            if (action is PlaySequenceAction)
                viewModel = new PlaySequenceActionViewModel((PlaySequenceAction)action);
            if (action is WalkBetweenDummiesAction)
                viewModel = new WalkBetweenDummiesActionViewModel((WalkBetweenDummiesAction)action);
            Datas.Add(viewModel);
            return viewModel;
        }

        public SequenceActionViewModel? GetNext(NodeOutputViewModel fromOutput)
        {
            //We know Nodes only ever have a single connection. 
            var outgoingConnection = fromOutput.Connections.Items.FirstOrDefault();
            if (outgoingConnection is null)
                return null; 

            return outgoingConnection.Input.Parent as SequenceActionViewModel;
        }

        public void RetrackSequenceFromNodes()
        {
            var enumerate = Datas.ToList(); 
            foreach (var item in enumerate)
            {
                Datas.Remove(item);
            }
            Loop.ElementContainer.Elements.Clear(); 

            SequenceActionViewModel? node = null;
            SequenceActionViewModel? previousNode = null; 

            do
            {
                node = GetNext(node?.FollowupActionOutput ?? EntryNode.FollowupActionOutput);
                if (node is null)
                    continue;
                //This hacky fix is nessecary because Timelines was only designed to add a Node without a Starttime
                Datas.Add(node);
                Loop.ElementContainer.Elements.Add(node.SequenceAction);
                if (previousNode is not null)
                    node.MoveAfter(previousNode);
                previousNode = node;
               
            } while (node is not null);

            int i = 0; 
        }
    }
}
