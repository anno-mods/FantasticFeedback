using FeedbackEditor.Models.FC.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    public class Loop
    {
        [XmlElement(ElementName = "hasValue")]
        public bool HasValue { get; set; }

        public FeedbackState DefaultState { get; set; }

        public ElementContainer ElementContainer { get; set; }

        public Loop()
        {
            DefaultState = new FeedbackState();
            ElementContainer= new ElementContainer();
        }
    }


    [XmlInclude(typeof(BranchElementContainer))]
    public class ElementContainer : IXmlSerializable
    {
        [XmlArrayItem("i")]
        public List<SequenceAction> Elements { get; set; } = new(); 

        public XmlSchema? GetSchema()
        {
            return null; 
        }

        public void ReadXml(XmlReader reader)
        {
            var clonedReader = reader.ReadSubtree();
            var loadedNode = new XmlDocument();
            loadedNode.Load(clonedReader);

            var nodes = loadedNode.SelectNodes($"/{loadedNode.DocumentElement.Name}/Elements/i");

            foreach (XmlNode node in nodes)
            {
                ProcessElement(node);
            }
            reader.ReadEndElement();
        }

        private void ProcessElement(XmlNode node)
        {
            var elementTypeNode = node.SelectSingleNode("./elementType");
            int.TryParse(elementTypeNode.InnerText, out int elementType);
            ActionType? actionType = (ActionType)elementType;

            using (XmlReader readerForSerialization = new XmlNodeReader(node))
            {
                var action = SequenceActionFactory.GetTypeOfAction(actionType!.Value);

                XmlSerializer serializer = new(action);
                var sequenceObject = serializer.Deserialize(readerForSerialization) as SequenceAction;

                if (sequenceObject is SequenceAction sequenceAction)
                {
                    Elements.Add(sequenceAction);
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Elements");
            foreach (var action in Elements)
            {
                var s = new XmlSerializer(action.GetType());
                s.Serialize(writer, action);
            }
            writer.WriteEndElement();
        }
    }
}
