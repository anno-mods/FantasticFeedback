using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Models.FC.Dummy;
using FeedbackEditor.Services;
using FeedbackEditor.Views.Nodes;
using PropertyChanged;
using ReactiveUI;

namespace FeedbackEditor.ViewModel.Nodes.SequenceActions
{
    [AddINotifyPropertyChangedInterface]
    public class WalkRandomActionNodeViewModel : SequenceActionNodeViewModel
    {
        static WalkRandomActionNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new WalkRandomNodeView(), typeof(IViewFor<WalkRandomActionNodeViewModel>));
        }
        public WalkRandomAction Action { get; set; }

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

        public WalkRandomActionNodeViewModel(WalkRandomAction sequenceAction) : base(sequenceAction)
        {
            Name = "Walk Random";
            Action = sequenceAction;
            _centerDummy = FcFileService.Instance.GetDummy(sequenceAction.CenterDummyId);
        }

    }
}
