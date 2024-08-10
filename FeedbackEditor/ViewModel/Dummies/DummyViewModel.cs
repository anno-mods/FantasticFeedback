using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedbackEditor.Models.FC.Dummy;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace FeedbackEditor.ViewModel.Dummies
{
    [AddINotifyPropertyChangedInterface]
    public class DummyViewModel : IDummyItem
    {
        public Dummy Dummy { get; }

        public DummyItemType DummyType { get; } = DummyItemType.Dummy;

        public IList<IDummyItem> Children { get; } = new List<IDummyItem>();

        [DependsOn(nameof(Dummy))]
        public string Name => Dummy.Name;

        public DummyViewModel(Dummy dummy)
        {
            Dummy = dummy;
        }
    }
}
