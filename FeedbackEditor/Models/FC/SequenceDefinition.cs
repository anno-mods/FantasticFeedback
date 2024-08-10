using DynamicData;
using FeedbackEditor.Models.FC.Actions;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    public class SequenceDefinition
    {
        [XmlElement(ElementName = "hasValue")]
        public bool HasValue { get; set; }

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

        [XmlElement]
        public Loop? Loop0 { get; set; }
        [XmlElement]
        public Loop? Loop1 { get; set; }
        [XmlElement]
        public Loop? Loop2 { get; set; }


    }
}
