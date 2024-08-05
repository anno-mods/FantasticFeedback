using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using FeedbackEditor.ViewModel.Timeline;

namespace FeedbackEditor.Converters
{
    [ValueConversion(typeof(ChannelType), typeof(Brush))]
    public class ChannelColorConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo Culture)
        {
            ChannelType type = (ChannelType)value;

            try
            {
                return type switch
                {
                    ChannelType.ACTOR => Application.Current.Resources["ActorChannelColorBrush"],
                    ChannelType.SEQUENCE => Application.Current.Resources["SequenceChannelColorBrush"],
                    ChannelType.LOOP => Application.Current.Resources["LoopChannelColorBrush"],
                    _ => "Red"
                };
            }
            catch (Exception ex)
            {
                return "Red";
            }
        }

        public object ConvertBack(object value, Type TargetType, object parameter, CultureInfo Culture)
        {
            throw new NotImplementedException("We don't need to convert from colors :)");
        }
    }
}
