using FeedbackEditor.Models.FC;
using FeedbackEditor.ViewModel;
using FeedbackEditor.ViewModel.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FeedbackEditor.Views
{
    /// <summary>
    /// Interaktionslogik für FileSettingsView.xaml
    /// </summary>
    public partial class FileSettingsView : UserControl
    {
        public FeedbackDefinitionViewModel? DisplayedFile
        {
            get => GetValue(DisplayedFileProperty) as FeedbackDefinitionViewModel;
            set => SetValue(DisplayedFileProperty, value);
        }

        public static readonly DependencyProperty DisplayedFileProperty = DependencyProperty.Register(
            nameof(DisplayedFile),
            typeof(FeedbackDefinitionViewModel),
            typeof(FileSettingsView)
        );

        public class SequenceActivatedEntry
        { 
            FeedbackSequenceType Type { get; set; }
            bool IsActivated { get; set; }
        }

        public FileSettingsView()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (DisplayedFile is null)
                return;
            if (sender is not CheckBox checkBox)
                return;
            if (checkBox.DataContext is not FeedbackSequenceType fst)
                return;
            if (DisplayedFile.FeedbackDefinition.ValidSequenceIDs.Contains(fst))
                return;

            DisplayedFile.FeedbackDefinition.ValidSequenceIDs.Add(fst);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DisplayedFile is null)
                return;
            if (sender is not CheckBox checkBox)
                return;
            if (checkBox.DataContext is not FeedbackSequenceType fst)
                return;

            bool remove = true; 
            //There is a user for this Feedback Sequence
            if (DisplayedFile
                .FeedbackDefinition
                .FeedbackConfigs
                .Any(x => x.FeedbackLoops.ReadonlyValues.ContainsKey(fst)))
            { 
                GenericOkayPopup popup = new GenericOkayPopup()
                { 
                    MESSAGE = "There are Users of this Feedback Sequence. Disabling this Feedback Sequence will clear the link between it and all Actor Sequences. from all Users. \n\n Proceed?",
                    OK_TEXT= "OK",
                    CANCEL_TEXT= "Cancel",
                };
                remove = popup.ShowDialog() == true; 
            }
            if(remove)
            {
                DisplayedFile?.FeedbackDefinition.ValidSequenceIDs.Remove(fst);
                foreach (var feedbackConfig in DisplayedFile!.FeedbackDefinition.FeedbackConfigs)
                {
                    feedbackConfig.FeedbackLoops.Remove(fst);
                }
            }
            else
            {
                checkBox.IsChecked = true;
            }
        }

        private void CheckBox_Initialized(object sender, EventArgs e)
        {
            if (sender is not CheckBox checkBox)
                return;
            if (checkBox.DataContext is not FeedbackSequenceType fst)
                return;
            checkBox.IsChecked = DisplayedFile?.FeedbackDefinition.ValidSequenceIDs.Contains(fst);
        }
    }
}
