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
        public FcFile GetDummyFcFile() 
        {
            var file = new FcFile()
            {
                FeedbackDefinition = new FeedbackDefinition(),
                ActorNames = new ActorNames()
            };

            file.ActorNames.Names.Add("walking");
            file.ActorNames.Names.Add("standing");

            file.FeedbackDefinition.FeedbackConfigs.Add(GetDummy());
            file.FeedbackDefinition.FeedbackConfigs.Add(GetDummy2());
            return file; 
        }
        public FeedbackConfig GetDummy() {

            return new FeedbackConfig()
            {
                HasValue = true,
                AssetVariationList = new AssetVariationList()
                {
                    GuidVariationList = { ( 100637, -1 ) }
                },
                MainObject = false,
                FeedbackLoops = new FeedbackLoops(),
                SequenceDefinitions = new()
                {
                    new SequenceDefinition()
                    {
                        
                        Loop0 = new Loop() 
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
                            ElementContainer = new()
                            {
                                Elements = new () {
                                    new PlaySequenceAction() {
                                        IdleSequenceID= SequenceID.idle01,
                                        HasValue= true
                                    },
                                    new WalkBetweenDummiesAction()
                                    {
                                        WalkSequence = SequenceID.walk01,
                                        StartDummy = "DummyStart",
                                        TargetDummy = "Dummytarget"
                                    },
                                    new PlaySequenceAction() {
                                        IdleSequenceID= SequenceID.work01,
                                        HasValue= true
                                    }
                                }
                            }
                        }
                    },
                }
            };
        }


        public FeedbackConfig GetDummy2()
        {

            return new FeedbackConfig()
            {
                HasValue = true,
                AssetVariationList = new AssetVariationList()
                {
                    GuidVariationList = { (500, -1) }
                },
                MainObject = false,
                FeedbackLoops = new FeedbackLoops(),
                SequenceDefinitions = new()
                {
                    new SequenceDefinition()
                    {
                        Loop0 =
                        new Loop() 
                        {   
                            HasValue = true,
                            DefaultState = new FeedbackState()
                            {
                                DummyID = 0,
                                DummyName = "kek",
                                FadeVisibility = true,
                                ResetToDefaultEveryLoop = true,
                                SequenceID = SequenceID.work02,
                                StartDummyGroup = "kekgroup",
                                Visible = true,
                            },
                            ElementContainer = new()
                            {
                                Elements = new () {
                                    new PlaySequenceAction() {
                                        IdleSequenceID= SequenceID.idle01,
                                        HasValue= true
                                    },
                                    new PlaySequenceAction() {
                                        IdleSequenceID= SequenceID.work01,
                                        HasValue= true
                                    },
                                    new PlaySequenceAction() {
                                        IdleSequenceID= SequenceID.work01,
                                        HasValue= true
                                    }
                                }
                            }
                        },
                        Loop1 = new Loop() 
                        {
                            ElementContainer = new()
                            {
                                Elements = new () {
                                    new WalkBetweenDummiesAction()
                                    {
                                        WalkSequence = SequenceID.walk01,
                                        StartDummy = "DummyStart",
                                        TargetDummy = "Dummytarget"
                                    },
                                    new PlaySequenceAction() {
                                        IdleSequenceID= SequenceID.idle01,
                                        HasValue= true
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
