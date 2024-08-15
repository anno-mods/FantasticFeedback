using FeedbackEditor.Models.FC.Dummy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    [XmlRoot("Content")]
    public class FcFile
    {
        [XmlElement]
        public DummyGroup DummyRoot { get; set; } = new();
        [XmlElement]
        public int IdCounter { get; set; }
        [XmlElement]
        public FeedbackDefinition FeedbackDefinition { get; set; } = new();
        [XmlElement]
        public ActorNames ActorNames { get; set; } = new();

        public void AddActor(FeedbackConfig config, String name)
        {
            ActorNames.Names.Add(name);
            FeedbackDefinition.FeedbackConfigs.Add(config);
        }

        public void RemoveActor(FeedbackConfig config)
        {
            if (!FeedbackDefinition.FeedbackConfigs.Contains(config))
            {
                Console.WriteLine($"Cannot remove actor {config} because it does not exist");
                return;
            }
            var index = GetNameIndex(config);
            //index shouldn't be -1 here because we checked contains
            ActorNames.Names.RemoveAt(index);
            var feedbackConfigIndex = FeedbackDefinition.FeedbackConfigs.IndexOf(config);
            FeedbackDefinition.FeedbackConfigs.RemoveAt(feedbackConfigIndex);
        }

        private int GetNameIndex(FeedbackConfig config)
        {
            var index = FeedbackDefinition.FeedbackConfigs.IndexOf(config);
            if (index == -1)
                return -1;
            if (ActorNames.Names.Count != FeedbackDefinition.FeedbackConfigs.Count)
            {
                index += 1;
                //special case where RootObject is a seperate thing
            }
            return index;
        }

        public String? GetActorName(FeedbackConfig config)
        {
            var index = GetNameIndex(config);
            if (index == -1)
                return null;
            return ActorNames.Names.ElementAtOrDefault(index);
        }

        public void TrySetActorName(FeedbackConfig config, String NewName)
        {
            var index = GetNameIndex(config);
            if (index == -1)
            {
                ActorNames.Names.Add(NewName);
                return;
            }

            ActorNames.Names[index] = NewName;
        }

    }

}
