﻿using FeedbackEditor.Models.FC;
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
                Datas.Add(new SequenceActionViewModel(action, offset));
                offset += 100;
            }
        }
    }
}
