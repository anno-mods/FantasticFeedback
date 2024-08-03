using FeedbackEditor.Models.FC;
using FeedbackEditor.Models.FC.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.Util
{
    public class DummyData
    {
        public FeedbackConfig GetDummy() {

            return new FeedbackConfig()
            {
                HasValue = true,
                AssetVariationList = new AssetVariationList()
                {
                    GuidVariationList = { { 100637, -1 } }
                },
                MainObject = false,
                FeedbackLoops = new() {
                    new FeedbackLoop() { k = false, v = false }
                },
                SequenceDefinitions = new()
                {
                    new SequenceDefinition()
                    {
                        HasValue = true,
                        DefaultState = new FeedbackState()
                        {
                            DummyID = 0,
                            DummyName = "kek",
                            FadeVisibility = true,
                            ResetToDefaultEveryLoop = true,
                            SequenceID = SequenceID.idle01,
                            StartDummyGroup = "kekgroup",
                            Visible = true,
                        },
                        Loops = new()
                        { 
                            new Loop() {
                                ElementContainer = new()
                                {
                                    new PlaySequenceAction() {
                                        IdleSequenceID= SequenceID.idle01,
                                        HasValue= true
                                    },
                                    new WalkBetweenDummiesAction()
                                    {
                                        WalkSequence = SequenceID.walk01,
                                        StartDummy = "DummyStart",
                                        TargetDummy = "Dummytarget"
                                    }
                                }
                            }
                        }                        
                    },
                }
            };
        }
    }
}
