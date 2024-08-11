using FeedbackEditor.ViewModel.Nodes.SequenceActions;
using PropertyChanged;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Windows;

namespace FeedbackEditor.Views.Nodes
{
    [AddINotifyPropertyChangedInterface]
    public partial class WaitNodeView : IViewFor<WaitActionNodeViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(nameof(ViewModel), typeof(WaitActionNodeViewModel), typeof(WaitNodeView), new PropertyMetadata(null));

        public WaitNodeView()
        {
            DataContext = this; 
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.WhenAnyValue(v => v.ViewModel).BindTo(this, v => v.NodeView.ViewModel).DisposeWith(d);
            });
        }

        public WaitActionNodeViewModel ViewModel
        {
            get => (WaitActionNodeViewModel)GetValue(ViewModelProperty);
            set {
                SetValue(ViewModelProperty, value);
                this.OnPropertyChanged(nameof(ViewModel));
            }
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (WaitActionNodeViewModel)value;
        }

        private void NumericSpinner_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}
