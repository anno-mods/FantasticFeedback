using DynamicData;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
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
        public NetworkViewModel Network { get; set; } = new NetworkViewModel();
        public Sequence()
        {
            DataContext = this;
            InitializeComponent();
        }
    }
}
