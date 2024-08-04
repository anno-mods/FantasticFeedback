using DynamicData;
using FeedbackEditor.ViewModel;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.ViewModel
{
    public class EntryNodeViewModel : NodeViewModel
    {
        static EntryNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<EntryNodeViewModel>));
        }

        public NodeOutputViewModel FollowupActionOutput { get; }

        public EntryNodeViewModel()
        {
            FollowupActionOutput= new NodeOutputViewModel();
            Outputs.Add(FollowupActionOutput);

            FollowupActionOutput.Name = "Next Action";
            Name = "Loop Start";

            CanBeRemovedByUser = false;
        }
    }
}
