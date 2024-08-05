using NodeNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.ViewModel.Nodes
{
    public interface IFollowupPositionableNode : IFollowupNode
    {
        System.Windows.Point Position { get; }
    }
}
