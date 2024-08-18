using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using FeedbackEditor.ViewModel.Timeline;
using FeedbackEditor.Models.FC;

namespace FeedbackEditor.Converters
{
    [ValueConversion(typeof(FeedbackSequenceType), typeof(String))]
    public class SequenceToolTipConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo Culture)
        {
            FeedbackSequenceType type = (FeedbackSequenceType)value;

            try
            {
                return type switch
                {
                    FeedbackSequenceType.Default => "Default/Idle State of the Building. Played by default for any Building",
                    FeedbackSequenceType.Work => "Working State of the Building. Autoplayed for factories when productivity > 0%",
                    _ => $"No Autoplay, activate using ActionStartObjectSequence with StartLocalFeedbackSequence={type}"
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
