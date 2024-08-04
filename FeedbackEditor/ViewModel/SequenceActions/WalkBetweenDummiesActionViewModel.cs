﻿using FeedbackEditor.Models.FC.Actions;
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
    public class WalkBetweenDummiesActionViewModel : SequenceActionViewModel
    {
        static WalkBetweenDummiesActionViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new WalkBetweenDummiesNodeView(), typeof(IViewFor<WalkBetweenDummiesActionViewModel>));
        }

        public string DummyFrom { get; set; }
        public string DummyTo { get; set; }

        public WalkBetweenDummiesActionViewModel(WalkBetweenDummiesAction sequenceAction) : base(sequenceAction)
        {
            DummyFrom = sequenceAction.StartDummy;
            DummyTo = sequenceAction.TargetDummy;

            Name = "Walk Between Dummies";
        }
    }
}
