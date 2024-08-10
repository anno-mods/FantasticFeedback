using FeedbackEditor.Models.FC.Dummy;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedbackEditor.ViewModel.Dummies
{
    [AddINotifyPropertyChangedInterface]
    public class DummyGroupViewModel : IDummyItem
    {
        public DummyGroup DummyGroup { get; }

        public DummyItemType DummyType { get; } = DummyItemType.Group;

        [DependsOn(nameof(DummyGroup))]
        public IList<IDummyItem> Children
        {
            get => DummyGroup.Groups.Select(x => new DummyGroupViewModel(x)).Cast<IDummyItem>()
                        .ToList()
                        .Concat(
                            DummyGroup.Dummies.Select(x => new DummyViewModel(x)).Cast<IDummyItem>()
                                    .ToList())
                        .ToList();
        }

        [DependsOn(nameof(Dummy))]
        public string Name => DummyGroup.Name;

        public DummyGroupViewModel(DummyGroup dummyGroup)
        {
            DummyGroup = dummyGroup;
        }
    }
}
