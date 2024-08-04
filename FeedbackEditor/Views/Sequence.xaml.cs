using DynamicData;
using FeedbackEditor.ViewModel;
using NodeNetwork.Toolkit.Layout.ForceDirected;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaktionslogik für Sequence.xaml
    /// </summary>
    /// 
    [AddINotifyPropertyChangedInterface]
    public partial class Sequence : UserControl
    {
        public LoopViewModel LoopViewModel { get; set; }

        public Sequence()
        {
            DataContext = this;
            InitializeComponent();
        }

        public void Layout()
        {
            Point Pos = new Point(0, 0);
            foreach (var n in LoopViewModel.Network.Nodes.Items)
            {
                n.Position = Pos;
                Pos.X += n.Size.Width + 200;
            }

        }

        private void OnLayoutButtonClick(object sender, RoutedEventArgs e)
        {
            Layout();
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
