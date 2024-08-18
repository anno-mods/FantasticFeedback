using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    public class FeedbackConfig
    {
        [XmlElement(ElementName = "hasValue")]
        public bool HasValue { get; set; }

        public AssetVariationList? AssetVariationList { get; set; } = new();

        public bool MainObject { get; set; } = false;

        public FeedbackLoops FeedbackLoops { get; set; } = new();

        [XmlArrayItem("i")]
        public List<SequenceDefinition> SequenceDefinitions { get; set; } = new();

        public bool IgnoreRootObjectXZRotation { get; set; }

        public bool IsAlwaysVisibleActor { get; set; }

        public bool ApplyScaleToMovementSpeed { get; set; }

        public int ActorCount { get; set; }

        public int MaxActorCount { get; set; }

        public int CreateChance { get; set; }

        public String BoneLink { get; set; } = "BoneLink";

        public int RenderFlags { get; set; }

        public String MultiplyActorByDummyCount { get; set; } = "";

        public bool IgnoreForceActorVariation { get; set; }

        public bool IgnoreDistanceScale { get; set; }

        public String? Description { get; set; }

        public FeedbackConfig() { }

        public SequenceDefinition CreateNewSequenceDefinition()
        {
            var sequenceDefinition = new SequenceDefinition()
            {
                Loop0 = new Loop(),
                Loop1 = new Loop(),
                Loop2 = new Loop()
            };
            var index = SequenceDefinitions.Count;
            //FeedbackLoops.Add(new FeedbackLoops());
            SequenceDefinitions.Add(sequenceDefinition);
            return sequenceDefinition;
        }

        public void RemoveSequenceDefinition(SequenceDefinition sequenceDefinition)
        {
            var index = SequenceDefinitions.IndexOf(sequenceDefinition);
            //FeedbackLoops.RemoveAt(index);
            SequenceDefinitions.Remove(sequenceDefinition);
        }
    }
}
