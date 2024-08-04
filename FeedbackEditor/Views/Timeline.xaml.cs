using FeedbackEditor.Models.FC;
using FeedbackEditor.Util;
using FeedbackEditor.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TimeLines;

namespace FeedbackEditor.Views
{
    /// summary>
    /// Interaktionslogik für Timeline.xaml
    /// 
    public partial class Timeline : UserControl
    {
        public ObservableCollection<FeedbackConfigViewModel> FeedbackConfigs { get; set; } = new();

        public Timeline()
        {
            InitializeComponent();
            DataContext = this;
            Timelines.ItemsSource = FeedbackConfigs;
        }

        private void Timelines_DragEnter(object sender, DragEventArgs e)
        {

        }
    }

    public class TempDataTemplateSeletor : DataTemplateSelector
    {
        public DataTemplate PlaySequenceAction { get; set; }
        public DataTemplate WalkBetweenDummiesAction { get; set; }
        public DataTemplate GenericSequenceAction { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is PlaySequenceActionViewModel)
                return PlaySequenceAction;

            if (item is WalkBetweenDummiesActionViewModel)
                return WalkBetweenDummiesAction;

            if (item is SequenceActionViewModel)
                return GenericSequenceAction;
            return null;
        }
    }
}
