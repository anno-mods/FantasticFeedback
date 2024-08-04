using FeedbackEditor.Util;
using FeedbackEditor.ViewModel;
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
            var viewModel = new FeedbackConfigViewModel(dummy);
            TimelineView.FeedbackConfigs.Add(viewModel);

            var network = (viewModel.Childs.First().Childs.First() as LoopViewModel)?.Network;
            NodeView.Network = network;
        }
    }
}
