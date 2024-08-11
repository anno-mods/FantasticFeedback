using FeedbackEditor.ViewModel.Nodes.SequenceActions;
using PropertyChanged;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Windows;

namespace FeedbackEditor.Views.Nodes
{
    [AddINotifyPropertyChangedInterface]
    public partial class ScaleNodeView : IViewFor<ScaleActionNodeViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(nameof(ViewModel), typeof(ScaleActionNodeViewModel), typeof(ScaleNodeView), new PropertyMetadata(null));

        public ScaleNodeView()
        {
            DataContext = this; 
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.WhenAnyValue(v => v.ViewModel).BindTo(this, v => v.NodeView.ViewModel).DisposeWith(d);
            });
        }

        public ScaleActionNodeViewModel ViewModel
        {
            get => (ScaleActionNodeViewModel)GetValue(ViewModelProperty);
            set {
                SetValue(ViewModelProperty, value);
                this.OnPropertyChanged(nameof(ViewModel));
            }
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ScaleActionNodeViewModel)value;
        }
        private void NumericSpinner_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}
