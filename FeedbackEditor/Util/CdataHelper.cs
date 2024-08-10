using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FeedbackEditor.Util
{
    class CdataHelper
    {
        public static List<int> ParseValues(String cdata)
        {
            var list = new List<int>();

            var match = Regex.Match(cdata, @"CDATA\[([0-9\s-]+)\]");
            try
            {
                var extract = match.Groups[1].Value;
                var split = extract.Split(' ');
                for (int i = 0; i < split.Length; i ++)
                {
                    list.Add(int.Parse(split[i]));
                }
            }
            catch
            {
                Console.WriteLine($"Error parsing CDATA {cdata}");
            }
            return list; 
        }

        public static String BuildCdataString(IEnumerable<int> values)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("CDATA[");
            var first = true;
            foreach (var value in values)
            {
                if (!first)
                {
                    builder.Append(' ');
                }
                first = false;
                builder.Append(value);
            }
            builder.Append(']');
            return builder.ToString();
        }
    }
}
