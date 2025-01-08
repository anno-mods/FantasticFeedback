using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Models.FC.Dummy;
using FeedbackEditor.Services;
using FeedbackEditor.Views.Nodes;
using PropertyChanged;
using ReactiveUI;

namespace FeedbackEditor.ViewModel.Nodes.SequenceActions
{
    [AddINotifyPropertyChangedInterface]
    public class PositionRandomActionNodeViewModel : SequenceActionNodeViewModel
    {
        static PositionRandomActionNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new PositionRandomNodeView(), typeof(IViewFor<PositionRandomActionNodeViewModel>));
        }
        public PositionRandomAction Action { get; set; }

        public Dummy? CenterDummy
        {
            get => _centerDummy;
            set
            {
                _centerDummy = value;
                Action.CenterDummy = value is not null ? value.Name : "";
                Action.CenterDummyId = value is not null ? value.Id : 0;
            }
        }
        private Dummy? _centerDummy { get; set; }

        public PositionRandomActionNodeViewModel(PositionRandomAction sequenceAction) : base(sequenceAction)
        {
            Name = "Set Random Position";
            Action = sequenceAction;
            _centerDummy = FcFileService.Instance.GetDummy(sequenceAction.CenterDummyId);
        }

    }
}
