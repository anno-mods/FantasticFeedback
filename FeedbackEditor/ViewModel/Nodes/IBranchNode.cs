using FeedbackEditor.Models.FC.Actions;
using NodeNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.ViewModel.Nodes
{
    public interface IBranchNode
    {
        NodeOutputViewModel BranchOutput { get; }
    }

    public static class IBranchNodeExtension
    {
        public static IEnumerable<SequenceActionNodeViewModel> GetBranchedSuccessors(this IBranchNode node)
        { 
            //We know Nodes only ever have a single connection. 
            var outgoingConnections = node.BranchOutput.Connections.Items.Where(x => x.Input.Parent is SequenceActionNodeViewModel);
            if (outgoingConnections is not null)
            {
                foreach (var connection in outgoingConnections)
                {
                    yield return connection.Input.Parent as SequenceActionNodeViewModel;
                }
            }            
        }
    }
}
