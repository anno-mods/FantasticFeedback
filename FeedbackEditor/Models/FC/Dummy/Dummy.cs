using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC.Dummy
{
    public struct Vector3
    { 
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }

    public struct Quaternion
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float w { get; set; }
    }

    public class Dummy
    {
        [XmlElement(ElementName = "hasValue")]
        public bool HasValue { get; set; } = true;

        public String Name { get; set; }

        public Vector3 Position { get; set; }

        public Quaternion Orientation { get; set; }

        public Vector3 Extents { get; set; }

        public float RotationY { get; set; }

        public int Id { get; set; }

        public int HeightAdaptationMode { get; set; }

    }
}
