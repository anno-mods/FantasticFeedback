using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Views.Nodes;
using NodeNetwork.Views;
using PropertyChanged;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class PlaySequenceActionViewModel : SequenceActionViewModel
    {
        static PlaySequenceActionViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new PlaySequenceNodeView(), typeof(IViewFor<PlaySequenceActionViewModel>));
        }

        public string SequenceID { get; set; }

        public int FuckYou { 
            get; 
            set;
        }

        public PlaySequenceAction Action { get; set; }

        public PlaySequenceActionViewModel(PlaySequenceAction sequenceAction) : base(sequenceAction)
        {
            SequenceID = $"{sequenceAction.IdleSequenceID.ToString()} ({(int)sequenceAction.IdleSequenceID})";

            Name = "Play Sequence";

            Action = sequenceAction;
        }
    }
}
