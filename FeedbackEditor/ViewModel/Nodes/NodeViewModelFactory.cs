using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.ViewModel.Nodes.SequenceActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.ViewModel.Nodes
{
    public class NodeViewModelFactory
    {
        public static SequenceActionNodeViewModel GetSequenceActionViewModel(SequenceAction action)
        { 
            var viewModel = new SequenceActionNodeViewModel(action);
            if (action is PlaySequenceAction)
                viewModel = new PlaySequenceActionViewModel((PlaySequenceAction)action);
            if (action is WalkBetweenDummiesAction)
                viewModel = new WalkBetweenDummiesActionViewModel((WalkBetweenDummiesAction)action);
            if (action is BranchAction)
                viewModel = new BranchActionNodeViewModel((BranchAction)action);
            if (action is PlayAnySequenceAction)
                viewModel = new PlayAnySequenceActionNodeViewModel((PlayAnySequenceAction)action);
            if (action is FadeAction)
                viewModel = new FadeActionNodeViewModel((FadeAction)action);
            return viewModel;
        }
    }
}
