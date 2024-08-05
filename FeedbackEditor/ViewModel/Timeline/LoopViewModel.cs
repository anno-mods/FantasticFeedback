using DynamicData;
using FeedbackEditor.Models.FC;
using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.ViewModel.Nodes;
using NodeNetwork.ViewModels;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeLines;

namespace FeedbackEditor.ViewModel.Timeline
{
    [AddINotifyPropertyChangedInterface]
    public class LoopViewModel : TimeLinesDataBase, IChannel
    {
        public Thickness OffsetOverride => new Thickness(40, 3, 3, 3);
        public string ChannelName { get; set; } = "Unnamed Loop";
        public ChannelType ChannelType { get; } = ChannelType.LOOP;

        public Loop Loop { get; }

        private List<SequenceActionTimelineViewModel> _viewModels = new(); 

        public LoopViewModel()
        {
            Loop = new Loop();
        }

        public LoopViewModel(Loop loop) : this()
        {
            Loop = loop;
            AddLoopElements(); 
        }

        private void ClearLoop()
        {
            foreach (var actionViewModel in _viewModels)
            { 
                Datas.Remove(actionViewModel);
            }
            _viewModels.Clear(); 
        }

        private void AddLoopElements()
        {
            SequenceActionTimelineViewModel? previous = null; 
            foreach (var action in Loop.ElementContainer.Elements)
            {
                SequenceActionTimelineViewModel viewModel = new SequenceActionTimelineViewModel(action);
                if (previous is not null)
                    viewModel.MoveAfter(previous);
                Datas.Add(viewModel);
                _viewModels.Add(viewModel);
                previous = viewModel;
            }
        }

        public void Update()
        {
            ClearLoop();
            AddLoopElements(); 
        }
    }
}
