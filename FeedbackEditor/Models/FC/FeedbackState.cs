using FeedbackEditor.Models.FC.Actions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    public class FeedbackState
    {
        public String? DummyName { get; set; } = "";

        [XmlElement(ElementName ="DummyId")]
        public int DummyID { get; set; }

        [XmlIgnore]
        public SequenceID SequenceID { get; set; } = SequenceID.undefined;

        [XmlElement("SequenceID")]
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int SequenceIDForSerializing {
            get => (int)SequenceID;
            set => SequenceID = (SequenceID)value;
        }

        public bool Visible { get; set; }

        public bool FadeVisibility { get; set; }

        public bool ResetToDefaultEveryLoop { get; set; }

        public String StartDummyGroup { get; set; } = "";        
    }
}
