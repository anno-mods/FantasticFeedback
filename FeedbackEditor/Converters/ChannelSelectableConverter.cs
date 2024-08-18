using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using FeedbackEditor.ViewModel.Timeline;

namespace FeedbackEditor.Converters
{
    [ValueConversion(typeof(ChannelType), typeof(bool))]
    public class ChannelSelectableConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo Culture)
        {
            ChannelType type = (ChannelType)value;
            return type != ChannelType.LOOP && type != ChannelType.FEEDBACKDEFINITION;
        }

        public object ConvertBack(object value, Type TargetType, object parameter, CultureInfo Culture)
        {
            throw new NotImplementedException("We don't need to convert from colors :)");
        }
    }
}
