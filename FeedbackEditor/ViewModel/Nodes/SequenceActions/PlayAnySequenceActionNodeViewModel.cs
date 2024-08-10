using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Views.Nodes;
using NodeNetwork.Views;
using PropertyChanged;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class PlayAnySequenceActionNodeViewModel : SequenceActionNodeViewModel
    {
        static PlayAnySequenceActionNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new PlayAnySequenceNodeView(), typeof(IViewFor<PlayAnySequenceActionNodeViewModel>));
        }

        public PlayAnySequenceAction Action { get; set; }

        public ObservableCollection<SequenceIDWrapper> SequenceIDs { get; set; }

        public class SequenceIDWrapper
        { 
            public SequenceIDWrapper(SequenceID sequenceID)
            {
                SequenceID = sequenceID;
            }

            public SequenceID SequenceID { get; set; }
        }

        public PlayAnySequenceActionNodeViewModel(PlayAnySequenceAction sequenceAction) : base(sequenceAction)
        {
            Name = "Play Any Sequence";
            SequenceIDs = new(sequenceAction.SequenceIDs.Select(x => new SequenceIDWrapper(x)));
            Action = sequenceAction;

            SequenceIDs.CollectionChanged += (sender, e) =>
            {
                UpdateModel();
            };
        }

        public void UpdateModel()
        {
            Action.SequenceIDs = SequenceIDs.Select(x => x.SequenceID).ToList();
        }

        public void AddSequence()
        {
            SequenceIDs.Add(new SequenceIDWrapper(SequenceID.@default));
        }
    }
}
