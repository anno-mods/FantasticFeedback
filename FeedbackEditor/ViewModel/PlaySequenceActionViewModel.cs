using FeedbackEditor.Models.FC.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.ViewModel
{
    public class PlaySequenceActionViewModel : SequenceActionViewModel
    {
        public String SequenceID { get; set; }
        
        public PlaySequenceActionViewModel(PlaySequenceAction sequenceAction, int msOffset) : base(sequenceAction, msOffset)
        {
            SequenceID = $"{sequenceAction.IdleSequenceID.ToString()} ({((int)sequenceAction.IdleSequenceID)})";  
        }
    }
}
