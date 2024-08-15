using FeedbackEditor.ViewModel.Dummies;
using FeedbackEditor.ViewModel.Timeline;
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
    /// Interaktionslogik für LoopSettings.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class LoopSettingsView : UserControl
    {
        public LoopViewModel? DisplayedLoop
        {
            get => GetValue(DisplayedLoopProperty) as LoopViewModel;
            set => SetValue(DisplayedLoopProperty, value);
        }

        public static readonly DependencyProperty DisplayedLoopProperty = DependencyProperty.Register(
            nameof(DisplayedLoop), 
            typeof(LoopViewModel),
            typeof(LoopSettingsView)
        );


        public LoopSettingsView()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void OnPreviewDropAcceptOnlyDummies(object sender, DragEventArgs e)
        {
            e.Handled = e.Data.GetData(typeof(DummyViewModel)) is DummyViewModel;
        }

        private void OnDefaultStateDummyDropped(object sender, DragEventArgs e)
        {
            var vm = e.Data.GetData(typeof(DummyViewModel)) as DummyViewModel;

            if (vm != null && DisplayedLoop is not null)
            {
                DisplayedLoop.DefaultDummy = vm.Dummy;
            }
        }

        private void OnClearDefaultDummyClick(object sender, RoutedEventArgs e)
        {
            if(DisplayedLoop is not null)
                DisplayedLoop.DefaultDummy = null;
        }
    }
}
