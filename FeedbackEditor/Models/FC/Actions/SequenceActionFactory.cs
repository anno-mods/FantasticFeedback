using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.Models.FC.Actions
{
    public static class SequenceActionFactory
    {
        public static Type GetTypeOfAction(ActionType actionType)
        {
            return actionType switch
            {
                ActionType.WALK_BETWEEN_DUMMIES => typeof(WalkBetweenDummiesAction),
                ActionType.PLAY_SEQUENCE => typeof(PlaySequenceAction),
                ActionType.BRANCH => typeof(BranchAction),
                ActionType.PLAY_ANY_SEQUENCE => typeof(PlayAnySequenceAction),
                ActionType.FADE => typeof(FadeAction),
                ActionType.WAIT => typeof(WaitAction),
                ActionType.SCALE => typeof(ScaleAction),
                ActionType.BARRIER => typeof(BarrierAction),
                ActionType.TURN_TO => typeof(TurnAction),
                _ => typeof(SequenceAction)
            };
        }
    }
}
