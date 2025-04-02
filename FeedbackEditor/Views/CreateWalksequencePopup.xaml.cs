using FeedbackEditor.Models.FC.Actions;
using FeedbackEditor.Models.FC.Dummy;
using FeedbackEditor.Services;
using FeedbackEditor.ViewModel;
using FeedbackEditor.ViewModel.Dummies;
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
using System.Windows.Shapes;

namespace FeedbackEditor.Views
{
    /// <summary>
    /// Interaktionslogik für CreateWalksequencePopup.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class CreateWalksequencePopup : Window
    {
        public WalkBetweenDummiesActionViewModel ViewModel { get; set; }

        public DummyGroupViewModel DummyGroups { get; set; }

        public DummyGroup SelectedGroup { get; set; }

        public bool ExplicitStartDummies { get; set; }

        public WalkBetweenDummiesAction ActionTemplate { get => ViewModel.Action; }

        public CreateWalksequencePopup()
        {
            ViewModel = new WalkBetweenDummiesActionViewModel(new WalkBetweenDummiesAction());
            DummyGroups = new DummyGroupViewModel(FcFileService.Instance.CurrentFile.DummyRoot);

            ActionTemplate.SpeedFactorF = 1.0f;

            DataContext = this;

            InitializeComponent();


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

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
