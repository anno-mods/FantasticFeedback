using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC.Dummy
{
    [AddINotifyPropertyChangedInterface]
    public class DummyGroup : IDummyItem
    {
        [XmlElement(ElementName = "hasValue")]
        public bool HasValue { get; set; } = true;

        public String Name { get; set; } = "";

        [XmlArrayItem("i")]
        public List<DummyGroup> Groups { get; set; } = new();

        [XmlArrayItem("i")]
        public List<Dummy> Dummies { get; set; } = new();

        [XmlIgnore]
        public DummyItemType DummyType { get; } = DummyItemType.Group;

        [XmlIgnore]
        [DependsOn(nameof(Dummies))]
        public IList<IDummyItem> Children 
        { 
            get => Groups.Cast<IDummyItem>()
                        .ToList()
                        .Concat(
                            Dummies.Cast<IDummyItem>()
                                    .ToList())
                        .ToList();
        }

        public IEnumerable<Dummy> GetContainedDummies() 
        {
            return Dummies.Concat(Groups.SelectMany(x => x.Dummies).ToList()).ToList();
        }
    }
}
