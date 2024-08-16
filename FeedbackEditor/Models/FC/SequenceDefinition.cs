using DynamicData;
using FeedbackEditor.Models.FC.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    public class SequenceDefinition
    {
        [XmlElement(ElementName = "hasValue")]
        public bool HasValue { get => Loops.Any(x => x.HasValue); set { } }

        [XmlIgnore]
        public IEnumerable<Loop> Loops {
            get {
                var list = new List<Loop>();
                if (Loop0 is Loop loop0) 
                {
                    list.Add(loop0);
                }
                if (Loop1 is Loop loop1)
                {
                    list.Add(loop1);
                }
                if (Loop2 is Loop loop2)
                {
                    list.Add(loop2);
                }
                return list; 
            }
        }

        [XmlIgnore]
        //Initializer Loop
        public Loop Loop0 { get; set; } = new Loop();
        [XmlIgnore]
        //Running Sequence Loop
        public Loop Loop1 { get; set; } = new Loop();
        [XmlIgnore]
        //Destroy Loop
        public Loop Loop2 { get; set; } = new Loop();


        [XmlElement("Loop0")]
        public Loop? Loop0ForSerialization { get => Loop0.HasValue ? Loop0 : null; set => Loop0 = value; }
        [XmlElement("Loop1")]
        public Loop? Loop1ForSerialization { get => Loop1.HasValue ? Loop1 : null; set => Loop1 = value; }
        [XmlElement("Loop2")]
        public Loop? Loop2ForSerialization { get => Loop2.HasValue ? Loop2 : null; set => Loop2 = value; }


    }
}
