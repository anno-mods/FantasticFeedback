using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Models.FC.Dummy;
using FeedbackEditor.Services;
using FeedbackEditor.Views.Nodes;
using PropertyChanged;
using ReactiveUI;

namespace FeedbackEditor.ViewModel.Nodes.SequenceActions
{
    [AddINotifyPropertyChangedInterface]
    public class TurnActionNodeViewModel : SequenceActionNodeViewModel
    {
        static TurnActionNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new TurnNodeView(), typeof(IViewFor<TurnActionNodeViewModel>));
        }
        public TurnAction Action { get; set; }

        public Dummy? TurnToDummy
        {
            get => _turnToDummy;
            set
            {
                _turnToDummy = value;
                Action.TurnToDummy = value is not null ? value.Name : "";
                Action.TurnToDummyId = value is not null ? value.Id : 0;
            }
        }
        private Dummy? _turnToDummy { get; set; }

        public TurnActionNodeViewModel(TurnAction sequenceAction) : base(sequenceAction)
        {
            Name = "Turn to Dummy";
            Action = sequenceAction;
            _turnToDummy = FcFileService.Instance.GetDummy(sequenceAction.TurnToDummyId);
        }

    }
}
