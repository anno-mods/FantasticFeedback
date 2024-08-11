using FeedbackEditor.ViewModel.Nodes.SequenceActions;
using PropertyChanged;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Windows;

namespace FeedbackEditor.Views.Nodes
{
    [AddINotifyPropertyChangedInterface]
    public partial class BarrierNodeView : IViewFor<BarrierActionNodeViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(nameof(ViewModel), typeof(BarrierActionNodeViewModel), typeof(BarrierNodeView), new PropertyMetadata(null));

        public BarrierNodeView()
        {
            DataContext = this; 
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.WhenAnyValue(v => v.ViewModel).BindTo(this, v => v.NodeView.ViewModel).DisposeWith(d);
            });
        }

        public BarrierActionNodeViewModel ViewModel
        {
            get => (BarrierActionNodeViewModel)GetValue(ViewModelProperty);
            set {
                SetValue(ViewModelProperty, value);
                this.OnPropertyChanged(nameof(ViewModel));
            }
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (BarrierActionNodeViewModel)value;
        }

        private void NumericSpinner_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}
