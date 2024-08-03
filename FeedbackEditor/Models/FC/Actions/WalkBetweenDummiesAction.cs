using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.Models.FC.Actions
{
    public class WalkBetweenDummiesAction : SequenceAction
    {
        public SequenceID WalkSequence { get; set; } = SequenceID.walk01;

        public float SpeedFactorF { get; set; }

        public int StartDummyId { get; set; }

        public int TargetDummyId { get; set; }

        public String StartDummy { get; set; }

        public String TargetDummy { get; set; }

        public bool WalkFromCurrentPosition { get; set; }

        public bool UseTargetDummyDirection { get; set; }

        public String DummyGroup { get; set; } = "CDATA[-1 - 1 - 1]";
    }
}
