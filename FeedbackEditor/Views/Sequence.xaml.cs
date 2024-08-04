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
        private LoopViewModel? _loopViewModel;
        public LoopViewModel? LoopViewModel 
        {
            get => _loopViewModel;
            set
            {
                _loopViewModel = value;
                Network = value is null ? _fallback : value.Network;
                HasNetwork = value is not null; 
            }
        }

        private NetworkViewModel _fallback = new NetworkViewModel();
        public NetworkViewModel Network { get; private set; }

        public bool HasNetwork { get; private set; }

        public Sequence()
        {
            DataContext = this;
            if(LoopViewModel is null)
                Network = _fallback; 
            InitializeComponent();
        }

        public void Layout()
        {
            if (LoopViewModel is null)
                return;
            int MaxX = 3000;
            int row = 0;
            Point Pos = new Point(50, 50);
            foreach (var n in LoopViewModel.Network.Nodes.Items)
            {
                n.Position = Pos;
                Pos.X += 400;

                if(Pos.X > MaxX)
                {
                    Pos.X %= MaxX;
                    row++;
                }
                Pos.Y = 50 + row * 400;
            }
        }

        private void OnLayoutButtonClick(object sender, RoutedEventArgs e)
        {
            Layout(); 
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void NetworkView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Layout();

        }
    }
}
