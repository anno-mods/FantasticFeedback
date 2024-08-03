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

            var feedbackConfig = new FeedbackConfigViewModel();
            var feedbackConfig2 = new FeedbackConfigViewModel();
            var sequenceDefinition = new SequenceDefinitionViewModel();
            var loop = new LoopViewModel();
            var loop2 = new LoopViewModel();
            var loop3 = new LoopViewModel();
            sequenceDefinition.AddLoop(loop);
            sequenceDefinition.AddLoop(loop2);
            var sequenceDefinition2 = new SequenceDefinitionViewModel();
            sequenceDefinition2.AddLoop(loop3);
            feedbackConfig.AddSequenceDefinition(sequenceDefinition);
            feedbackConfig2.AddSequenceDefinition(sequenceDefinition2);
            FeedbackConfigs.Add(feedbackConfig);
            FeedbackConfigs.Add(feedbackConfig2);

            feedbackConfig.ChannelName = "Actor 01";
            feedbackConfig2.ChannelName = "Actor 02";
            sequenceDefinition.ChannelName = "work01";
            sequenceDefinition2.ChannelName = "idle01";
            loop.ChannelName = "Loop0";
            loop2.ChannelName = "loop1";
            loop3.ChannelName = "loop0";
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
            if (item is TempDataType)
                return TempDataType;
            return null;
        }
    }
}
