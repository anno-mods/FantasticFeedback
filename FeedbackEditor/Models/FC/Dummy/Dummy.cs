using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC.Dummy
{
    public class Dummy : IDummyItem
    {
        [XmlElement(ElementName = "hasValue")]
        public bool HasValue { get; set; } = true;

        public String Name { get; set; }

        public Vector3 Position { get; set; }

        public Quaternion Orientation { get; set; }

        public Vector3 Extents { get; set; }

        public float RotationY { get; set; }

        public int Id { get; set; }

        public int HeightAdaptionMode { get; set; }

        [XmlIgnore]
        public DummyItemType DummyType { get; } = DummyItemType.Dummy;
        [XmlIgnore]
        public IList<IDummyItem> Children { get; } = new List<IDummyItem>();
    }
}
