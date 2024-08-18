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
using FeedbackEditor.Views;

namespace FeedbackEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class MainWindow : Window
    {
        public DummyGroupViewModel DummyRoot { get; private set; }

        public bool CanSave { get; private set; } = true; 

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            
            TimelineView.SelectionChanged += OnSelectedLoopChanged;
            NewFile();
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
            var file = LoadFromDialog();
            if (file is null)
            {
                return;
            }
            NodeView.ShowDefaultNodesView();

            FcFileService.Instance.SetCurrentFile(file);
            DummyRoot = new DummyGroupViewModel(file.DummyRoot);
            CanSave = true;
        }

        private void SaveFileClick(object sender, RoutedEventArgs e)
        {
            var picker = new Microsoft.Win32.SaveFileDialog
            {
                Filter = (FileDBReaderService.Instance.IsInstalled() ? "Fc Files (*.fc)|*.fc|" : "") +  "Xml Files (*.xml)|*.xml",
                RestoreDirectory = true
            };

            if (true != picker.ShowDialog())
            {
                return;
            }

            var useXmlDirectly = picker.FileName.EndsWith(".xml");
            var useConversionLogic = picker.FileName.EndsWith(".fc");
            var xmlPath = useXmlDirectly ? picker.FileName : System.IO.Path.ChangeExtension(picker.FileName, "xml");
            
            FcFileLoaderService.Instance.SaveFcFile(xmlPath, FcFileService.Instance.CurrentFile);

            if (useConversionLogic)
            {
                FileDBReaderService.Instance.ConvertXml(xmlPath);
                System.IO.File.Delete(xmlPath);
            }
        }

        private void NewFileClick(object sender, RoutedEventArgs e)
        {
            NewFile();
        }

        private void SetupFileDBReaderClick(object sender, RoutedEventArgs e)
        {
            FileDBReaderSettingsPopup popup = new FileDBReaderSettingsPopup();
            popup.ShowDialog();
        }

        private void LoadDummiesClick(object sender, RoutedEventArgs e)
        {
            var file = LoadFromDialog();
            if (file is null)
                return;
            FcFileService.Instance.CurrentFile.DummyRoot = file.DummyRoot;
            FcFileService.Instance.SetCurrentFile(FcFileService.Instance.CurrentFile);
            DummyRoot = new DummyGroupViewModel(file.DummyRoot);
        }

        private FcFile? LoadFromDialog()
        {
            var picker = new OpenFileDialog
            {
                Filter = (FileDBReaderService.Instance.IsInstalled() ? "Fc Files (*.fc)|*.fc|" : "") + "Xml Files (*.xml)|*.xml",
            };

            if (true != picker.ShowDialog())
                return null;
            if (!picker.FileName.EndsWith(".fc") && !picker.FileName.EndsWith(".xml"))
                return null;

            var useXmlDirectly = picker.FileName.EndsWith(".xml");
            var useConversionLogic = picker.FileName.EndsWith(".fc");
            var xmlPath = useXmlDirectly ? picker.FileName : System.IO.Path.ChangeExtension(picker.FileName, "xml");

            if (useConversionLogic)
            {
                FileDBReaderService.Instance.ConvertFc(picker.FileName);
            }
            var file = FcFileLoaderService.Instance.LoadFcFile(xmlPath);
            if (file is null)
                throw new InvalidDataException("The Fc File loaded is invalid");
            if(useConversionLogic)
                System.IO.File.Delete(xmlPath);
            return file;
        }

        private void NewFile()
        {
            var file = new FcFile();
            FcFileService.Instance.SetCurrentFile(file);
            NodeView.ShowDefaultNodesView();
            DummyRoot = new DummyGroupViewModel(file.DummyRoot);
            CanSave = true;

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
