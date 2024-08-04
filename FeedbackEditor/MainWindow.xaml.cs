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
            var second_dummy = new DummyData().GetDummy2();
            var viewModel = new FeedbackConfigViewModel(dummy);
            var viewModel2 = new FeedbackConfigViewModel(second_dummy);
            TimelineView.FeedbackConfigs.Add(viewModel);
            TimelineView.FeedbackConfigs.Add(viewModel2);

            TimelineView.SelectedLoopChanged += (sender, e) => NodeView.LoopViewModel = e;

            NodeView.LoopViewModel = viewModel.Childs.First().Childs.First() as LoopViewModel;
        }
    }
}
