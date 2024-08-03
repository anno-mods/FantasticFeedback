using FeedbackEditor.Models.FC;
using FeedbackEditor.Models.FC.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeLines;

namespace FeedbackEditor.ViewModel
{
    public class LoopViewModel : TimeLinesDataBase, IChannel
    {
        public Thickness OffsetOverride => new Thickness(40, 3, 3, 3);
        public string ChannelName { get; set; } = "Unnamed Loop";
        public ChannelType ChannelType { get; } = ChannelType.LOOP;

        public LoopViewModel()
        { 
        
        }

        public LoopViewModel(Loop loop) : this()
        {
            var offset = 0;
            foreach (var action in loop.ElementContainer)
            {
                var viewModel = new SequenceActionViewModel(action, offset);
                if (action is PlaySequenceAction)
                    viewModel = new PlaySequenceActionViewModel((PlaySequenceAction)action, offset);
                if (action is WalkBetweenDummiesAction)
                    viewModel = new WalkBetweenDummiesActionViewModel((WalkBetweenDummiesAction)action, offset);
                Datas.Add(viewModel);
                offset += 100;
            }
        }
    }
}
