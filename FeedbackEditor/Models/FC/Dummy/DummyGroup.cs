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
    public class DummyGroup
    {
        [XmlElement(ElementName = "hasValue")]
        public bool HasValue { get; set; } = true;

        public String Name { get; set; } = "";


        [XmlArrayItem("i")]
        public List<DummyGroup> Groups { get; set; } = new();

        [XmlArrayItem("i")]
        public List<Dummy> Dummies { get; set; } = new();
        public int Id { get; set; }

        public IEnumerable<Dummy> GetContainedDummies() 
        {
            return Dummies.Concat(Groups.SelectMany(x => x.Dummies).ToList()).ToList();
        }

        public IEnumerable<DummyGroup> GetContainedDummyGroups()
        {
            return Groups.Concat(Groups.SelectMany(x => x.Groups).ToList()).ToList();
        }
    }
}
