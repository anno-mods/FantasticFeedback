using FeedbackEditor.Models.FC.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    public class FeedbackState
    {
        public String? DummyName { get; set; } = "";

        [XmlAttribute(AttributeName ="DummyId")]
        public int DummyID { get; set; }

        public SequenceID SequenceID { get; set; } = SequenceID.undefined;

        public bool Visible { get; set; }

        public bool FadeVisibility { get; set; }

        public bool ResetToDefaultEveryLoop { get; set; }

        public String StartDummyGroup { get; set; } = "";        
    }
}
