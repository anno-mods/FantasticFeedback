using FeedbackEditor.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Joins;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC
{
    public class AssetVariationList
    {
        [XmlIgnore]
        public List<(int, int)> GuidVariationList { get; set; } = new();

        [XmlElement("GuidVariationList")]
        public String GuidVariationListForSerialization
        {
            get
            {
                var list = new List<int>();
                foreach (var tuple in GuidVariationList)
                {
                    list.Add(tuple.Item1);
                    list.Add(tuple.Item2);
                }
                return CdataHelper.BuildCdataString(list);
            }
            set
            {
                GuidVariationList = new();
                if (value.Count() % 2 != 0)
                {
                    Console.WriteLine("Attempted to set GUIDVariationlist with a non-even number of CDATA entries | " + value);
                }
                var values = CdataHelper.ParseValues(value);
                for (int i = 0; i < values.Count(); i += 2)
                {
                    var tuple = (values[i], values[i + 1]);
                    GuidVariationList.Add(tuple);
                }
            }
        }

        public string AssetGroupNames { get; set; }
    }
}
