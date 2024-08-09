using FeedbackEditor.Models.FC;
using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Models.FC.Dummy;
using FeedbackEditor.Services;
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
    public class WalkBetweenDummiesActionViewModel : SequenceActionNodeViewModel
    {
        static WalkBetweenDummiesActionViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new WalkBetweenDummiesNodeView(), typeof(IViewFor<WalkBetweenDummiesActionViewModel>));
        }

        private Dummy? _startDummy;

        public Dummy? StartDummy 
        {
            get => _startDummy;
            set
            {
                UpdateModelStartDummy();
            }
        }
        public Dummy? TargetDummy 
        {
            get => _targetDummy;
            set
            {
                Action.TargetDummy = value is not null ? value.Name : "";
                Action.TargetDummyId = value is not null ? value.Id : 0;
            }
        }
        public Dummy? _targetDummy { get; set; }

        private bool _hasStartDummy;
        public bool HasStartDummy
        {
            get => _hasStartDummy;
            set
            {
                _hasStartDummy = value;
                UpdateModelStartDummy();
            }  
        }

        public WalkBetweenDummiesAction Action { get; set; }

        public WalkBetweenDummiesActionViewModel(WalkBetweenDummiesAction sequenceAction) : base(sequenceAction)
        {
            Action = sequenceAction;
            _startDummy = FcFileService.Instance.GetDummy(sequenceAction.StartDummyId);
            _targetDummy = FcFileService.Instance.GetDummy(sequenceAction.TargetDummyId);
            HasStartDummy = StartDummy is not null;

            Name = "Walk Between Dummies";
        }

        private void UpdateModelStartDummy()
        {
            Action.StartDummy = StartDummy is not null && HasStartDummy ? StartDummy.Name : "";
            Action.StartDummyId = StartDummy is not null && HasStartDummy ? StartDummy.Id : 0;
        }
    }
}
