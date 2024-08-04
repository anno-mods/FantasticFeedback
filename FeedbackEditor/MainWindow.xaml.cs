using FeedbackEditor.Models.FC;
using FeedbackEditor.Serialization;
using FeedbackEditor.Util;
using FeedbackEditor.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using System.Xml.Serialization;

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
            var dummy = new DummyData().GetDummy();
            var second_dummy = new DummyData().GetDummy2();
            var viewModel = new FeedbackConfigViewModel(dummy);
            var viewModel2 = new FeedbackConfigViewModel(second_dummy);
            TimelineView.FeedbackConfigs.Add(viewModel);
            TimelineView.FeedbackConfigs.Add(viewModel2);

            var fcfile = new DummyData().GetDummyFcFile();

            XmlDocument doc = new XmlDocument();
            var serializer = new XmlSerializer(typeof(FcFile));

            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;

            using var fs = File.Create("fc.xml");
            using (XmlWriter writer = new FeedbackXmlWriter(XmlWriter.Create(fs, settings))) 
            {
                serializer.Serialize(writer, fcfile);
            }
            fs.Close();
            using var fs1 = File.OpenRead("fc.xml");
            using (XmlReader reader = XmlReader.Create(fs1))
            {
                var obj = serializer.Deserialize(reader);
            }

            TimelineView.SelectedLoopChanged += (sender, e) => NodeView.LoopViewModel = e;
        }
    }
}
