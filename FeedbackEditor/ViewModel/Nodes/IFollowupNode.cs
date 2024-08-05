using NodeNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.ViewModel.Nodes
{
    public interface IFollowupNode
    {
        NodeOutputViewModel FollowupActionOutput { get; }
    }

    public static class IFollowupNodeExtension
    {
        public static SequenceActionNodeViewModel? GetSuccessor(this IFollowupNode node)
        {
            //We know Nodes only ever have a single connection. 
            var outgoingConnections = node.FollowupActionOutput.Connections.Items.Where(x => x.Input.Parent is SequenceActionNodeViewModel).FirstOrDefault();
            return outgoingConnections?.Input?.Parent as SequenceActionNodeViewModel;
        }
    }
}
