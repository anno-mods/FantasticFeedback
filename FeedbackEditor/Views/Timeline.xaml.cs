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

            var dummy = new DummyData().GetDummy();

            Timelines.ItemsSource = FeedbackConfigs;

            FeedbackConfigs.Add(new FeedbackConfigViewModel(dummy));
        }
    }

    public class TempDataType : ITimeLineData
    {
        public static int cnt = 0;
        static Random rand = new Random();
        public TempDataType()
        {
            cnt++;
            Name = "Temp" + cnt.ToString();
            StartTime = TimeSpan.FromMilliseconds(rand.Next(0, 600));
            EndTime = StartTime + TimeSpan.FromMilliseconds(rand.Next(300, 700));
        }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool? bResizable { get; set; }
        public String Name { get; set; }
    }

    public class TempDataTemplateSeletor : DataTemplateSelector
    {
        public DataTemplate TempDataType { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is SequenceActionViewModel)
                return TempDataType;
            return null;
        }
    }
}
