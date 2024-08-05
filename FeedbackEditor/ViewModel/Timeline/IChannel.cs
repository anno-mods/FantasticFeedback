using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FeedbackEditor.ViewModel.Timeline
{
    public enum ChannelType
    {
        ACTOR,
        SEQUENCE,
        LOOP
    }
    public interface IChannel
    {
        string ChannelName { get; set; }
        Thickness OffsetOverride { get; }
        ChannelType ChannelType { get; }
    }
}
