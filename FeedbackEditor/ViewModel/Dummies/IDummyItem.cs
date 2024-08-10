using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FeedbackEditor.ViewModel.Dummies
{
    public enum DummyItemType { Group, Dummy }
    public interface IDummyItem
    {
        String Name { get; }

        DummyItemType DummyType { get; }

        IList<IDummyItem> Children { get; }
    }
}
