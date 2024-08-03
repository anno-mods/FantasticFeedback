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
        [XmlAttribute(AttributeName ="hasValue")]
        public bool HasValue { get; set; }

        public AssetVariationList? AssetVariationList { get; set; }

        public bool? MainObject { get; set; }

        public List<FeedbackLoop> FeedbackLoops { get; set; } = new();

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
    }
}
