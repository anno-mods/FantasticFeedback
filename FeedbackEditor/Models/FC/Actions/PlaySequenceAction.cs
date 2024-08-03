using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.Models.FC.Actions
{
    public class PlaySequenceAction : SequenceAction
    {
        public SequenceID IdleSequenceID { get; set; } = SequenceID.idle01;

        public int MinPlayCount { get; set; } = 1;
        public int MaxPlayCount { get; set; } = 0;
        public int MinPlayTime { get; set; } = 0; 
        public int MaxPlayTime { get; set; } = 0;
        public bool ResetStartTime { get; set; } = false;
    }
}
