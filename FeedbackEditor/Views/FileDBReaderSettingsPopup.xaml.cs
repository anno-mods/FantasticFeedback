using FeedbackEditor.Services;
using PropertyChanged;
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
using System.Windows.Shapes;

namespace FeedbackEditor.Views
{
    /// <summary>
    /// Interaktionslogik für GenericOkayPopup.xaml
    /// </summary>
    /// 

    [AddINotifyPropertyChangedInterface]
    public partial class FileDBReaderSettingsPopup : Window
    {

        public String FileDBReaderPath {
            get;
            set;
        }

        [DependsOn(nameof(FileDBReaderPath))]
        public bool IsValidPath { get => FileDBReaderService.Instance.IsInstalled(); }

        [DependsOn(nameof(FileDBReaderPath))]
        public String BackgroundColor { get => IsValidPath ? "White" : "Red"; }

        public FileDBReaderSettingsPopup()
        {
            InitializeComponent();
            DataContext = this;
            FileDBReaderPath = Properties.Settings.Default.FileDBReaderPath;
        }

        private void SelectButtonClick(object sender, RoutedEventArgs e)
        {
            var picker = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select Path to FileDBReader.exe",
                Filter = "FileDBReader (*.exe)|FileDBReader.exe",
                RestoreDirectory = true
            };

            if (true != picker.ShowDialog())
            {
                return;
            }
            FileDBReaderPath = picker.FileName;
        }

        private void OkayButtonClick(object sender, RoutedEventArgs e)
        {
            FileDBReaderService.Instance.SetFileDBReaderPath(FileDBReaderPath);
            DialogResult = true;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
