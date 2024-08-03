using FeedbackEditor.Models.FC.Actions;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    public class SequenceDefinition
    {
        [XmlAttribute(AttributeName = "hasValue")]
        public bool HasValue { get; set; }

        public FeedbackState DefaultState { get; set; }

        public List<Loop> Loops { get; set; }
    }
}
