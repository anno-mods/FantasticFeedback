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
using FeedbackEditor.Models.FC.Dummy;
using PropertyChanged;
using FeedbackEditor.ViewModel.Dummies;

namespace FeedbackEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class MainWindow : Window
    {
        public DummyGroupViewModel DummyRoot { get; private set; }

        public bool CanSave { get; private set; } = false; 

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            
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
                NodeView.ShowDefaultNodesView();
                DummyRoot = new DummyGroupViewModel(file.DummyRoot);
                CanSave = true;
                return;
            }
            CanSave = false;
        }

        private void SaveFileClick(object sender, RoutedEventArgs e)
        {
            var picker = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Fc Files converted with FileDBReader (*.xml)|*.xml",
                RestoreDirectory = true
            };

            if (true == picker.ShowDialog())
            {
                FcFileService.Instance.SaveCurrentFile(picker.FileName);
            }
        }

        private void TreeView_PreviewDragLeave(object sender, DragEventArgs e)
        {

        }

        private void OnDummyMouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (sender is TextBlock text && text.DataContext is IDummyItem item)
                {
                    DragDrop.DoDragDrop(text, item, DragDropEffects.Move);
                }
            }
        }
    }
}
