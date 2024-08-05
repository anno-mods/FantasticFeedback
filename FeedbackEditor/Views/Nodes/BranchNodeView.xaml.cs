using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.ViewModel;
using FeedbackEditor.ViewModel.Nodes.SequenceActions;
using FeedbackEditor.ViewModel.Timeline;
using PropertyChanged;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class BranchNodeView : IViewFor<BranchActionNodeViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(nameof(ViewModel), typeof(BranchActionNodeViewModel), typeof(BranchNodeView), new PropertyMetadata(null));

        public BranchNodeView()
        {
            DataContext = this; 
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.WhenAnyValue(v => v.ViewModel).BindTo(this, v => v.NodeView.ViewModel).DisposeWith(d);
            });
        }

        public BranchActionNodeViewModel ViewModel
        {
            get => (BranchActionNodeViewModel)GetValue(ViewModelProperty);
            set {
                SetValue(ViewModelProperty, value);
                this.OnPropertyChanged(nameof(ViewModel));
            }
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (BranchActionNodeViewModel)value;
        }
    }
}
