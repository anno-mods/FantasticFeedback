using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.Models.FC
{
    public class AssetVariationList
    {
        public Dictionary<int, int> GuidVariationList { get; set; } = new();
        public string AssetGroupNames { get; set; }
    }
}
