using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    [Serializable]
    public class FeedbackLoops
    {
        [XmlElement("k")]
        //Index of the SequenceDefinition that should be active
        public List<int> FeedbackSequences
        {
            get;
            set;
        } = new();

        [XmlElement("v")]
        //Index of the SequenceDefinition that is played when k is active
        public List<int> ValueIndices
        {
            get;
            set;
        } = new();

        public IReadOnlyDictionary<FeedbackSequenceType, int> ReadonlyValues
        {
            get => FeedbackSequences
                .Where(x => Enum.IsDefined(typeof(FeedbackSequenceType), x))
                .Cast<FeedbackSequenceType>()
                .Zip(ValueIndices)
                .ToDictionary(x => x.First, y => y.Second);
        }

        public bool ContainsKey(int Key) => FeedbackSequences.Contains(Key);

        public int GetValue(int Key) => ValueIndices.ElementAt(FeedbackSequences.IndexOf(Key));

        public void SetValue(FeedbackSequenceType Key, int Value)
        {
            if (ContainsKey((int)Key))
            {
                var index = FeedbackSequences.IndexOf((int)Key);
                ValueIndices.RemoveAt(index);
                ValueIndices.Insert(index, Value);
                return;
            }

            FeedbackSequences.Add((int)Key);
            ValueIndices.Add(Value);
        }

        public void Remove(FeedbackSequenceType Key)
        { 
            if(ContainsKey((int)Key))
            { 
                FeedbackSequences.Remove((int)Key);
            }
        }

        public void Clear()
        {
            FeedbackSequences.Clear();
            ValueIndices.Clear();
        }
    }
}
