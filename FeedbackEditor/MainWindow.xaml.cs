using FeedbackEditor.Models.FC;
using FeedbackEditor.Serialization;
using FeedbackEditor.Services;
using FeedbackEditor.Util;
using FeedbackEditor.ViewModel;
using FeedbackEditor.ViewModel.Nodes;
using FeedbackEditor.ViewModel.Timeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using static System.Net.WebRequestMethods;

namespace FeedbackEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            FcFileService.Instance.LoadFcFile("costume_factory_01.xml");
            
            TimelineView.SelectionChanged += OnSelectedLoopChanged;
        }

        public void OnSelectedLoopChanged(object? sender, LoopViewModel? selectedLoop)
        {
            if (selectedLoop?.Loop is not Loop loop)
            {
                NodeView.ShowDefaultNodesView();
                return;
            }
            NodeView.ShowLoop(loop);
            NodeView.ChangeListenerTo(selectedLoop);
        }

        private void OpenFileClick(object sender, RoutedEventArgs e)
        {
            var picker = new OpenFileDialog
            {
                Filter = "Fc Files converted with FileDBReader (*.xml)|*.xml"
            };

            if (true == picker.ShowDialog())
            {
                var file = FcFileService.Instance.LoadFcFile(picker.FileName);
                if (file is null)
                    throw new InvalidDataException("The Fc File loaded is invalid");
                FcFileService.Instance.SetCurrentFile(file);
            }
        }
    }
}
