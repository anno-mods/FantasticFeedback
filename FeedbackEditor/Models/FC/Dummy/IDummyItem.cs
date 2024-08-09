using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.Models.FC.Dummy
{
    public enum DummyItemType { Group, Dummy }
    public interface IDummyItem
    {
        String Name { get; }

        DummyItemType DummyType { get; }

        IList<IDummyItem> Children { get; }
    }
}
