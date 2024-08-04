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

            var serializer = new XmlSerializer(typeof(FcFile));

            using var fs1 = File.OpenRead("fc.xml");
            using (XmlReader reader = XmlReader.Create(fs1))
            {
                var fcFile = serializer.Deserialize(reader) as FcFile;

                fcFile.FeedbackDefinition.FeedbackConfigs.ForEach(x =>
                {
                    var vm = new FeedbackConfigViewModel(x);
                    TimelineView.FeedbackConfigs.Add(vm);
                });
            }

            TimelineView.SelectedLoopChanged += (sender, e) => NodeView.LoopViewModel = e;
        }
    }
}
