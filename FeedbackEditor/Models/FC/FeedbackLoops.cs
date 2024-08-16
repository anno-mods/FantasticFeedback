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
        public List<int> KeyIndices
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

        public IReadOnlyDictionary<int, int> ReadonlyValues 
        { 
            get => KeyIndices.Zip(ValueIndices).ToDictionary(x => x.First, y => y.Second);
        }

        public bool ContainsKey(int Key) => KeyIndices.Contains(Key);

        public int GetValue(int Key) => ValueIndices.ElementAt(KeyIndices.IndexOf(Key));

        public void SetValue(int Key, int Value)
        {
            if (ContainsKey(Key))
            {
                var index = KeyIndices.IndexOf(Key);
                ValueIndices.RemoveAt(index);
                ValueIndices.Insert(index, Value);
                return;
            }

            KeyIndices.Add(Key);
            ValueIndices.Add(Value);
        }
    }
}
