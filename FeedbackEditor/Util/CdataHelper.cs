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
        public static List<(int, int)> ParseValues(String cdata)
        {
            var list = new List<(int, int)>();

            var match = Regex.Match(cdata, @"CDATA\[([0-9\s-]+)\]");
            try
            {
                var extract = match.Groups[1].Value;
                var split = extract.Split(' ');
                for (int i = 0; i < split.Length; i += 2)
                {
                    var tuple = (int.Parse(split[i]), int.Parse(split[i + 1]));
                    list.Add(tuple);
                }
            }
            catch
            {
                Console.WriteLine($"Error parsing CDATA {cdata}");
            }
            return list; 
        }

        public static String BuildCdataString(List<(int, int)> values)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("CDATA[");
            var first = true;
            foreach (var (key, value) in values)
            {
                if (!first)
                {
                    builder.Append(' ');
                }
                first = false;
                builder.Append(key);
                builder.Append(' ');
                builder.Append(value);
            }
            builder.Append(']');
            return builder.ToString();
        }
    }
}
