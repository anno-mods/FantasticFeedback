using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Models.FC.Dummy;
using FeedbackEditor.ViewModel;
using FeedbackEditor.ViewModel.Dummies;
using PropertyChanged;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
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

namespace FeedbackEditor.Views.Nodes
{
    /// <summary>
    /// Interaktionslogik für PlaySequenceNodeView.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class WalkBetweenDummiesNodeView : IViewFor<WalkBetweenDummiesActionViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(nameof(ViewModel), typeof(WalkBetweenDummiesActionViewModel), typeof(WalkBetweenDummiesNodeView), new PropertyMetadata(null));

        public WalkBetweenDummiesNodeView()
        {
            DataContext = this;
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.WhenAnyValue(v => v.ViewModel).BindTo(this, v => v.NodeView.ViewModel).DisposeWith(d);
            });
        }

        public WalkBetweenDummiesActionViewModel ViewModel
        {
            get => (WalkBetweenDummiesActionViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (WalkBetweenDummiesActionViewModel)value;
        }

        private void NumericSpinner_ValueChanged(object sender, EventArgs e)
        {

        }

        private void OnStartDummyDropped(object sender, DragEventArgs e)
        {
            var vm = e.Data.GetData(typeof(DummyViewModel)) as DummyViewModel;

            if (vm != null)
            {
                ViewModel.StartDummy = vm.Dummy;
            }
        }

        private void OnEndDummyDropped(object sender, DragEventArgs e)
        {
            var vm = e.Data.GetData(typeof(DummyViewModel)) as DummyViewModel;

            if (vm != null)
            {
                ViewModel.TargetDummy = vm.Dummy;
            }
        }

        private void OnPreviewDropAcceptOnlyDummies(object sender, DragEventArgs e)
        {
            e.Handled = e.Data.GetData(typeof(DummyViewModel)) is DummyViewModel;
        }
    }
}
