using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FeedbackEditor.Views
{
    /// <summary>
    /// Interaktionslogik für GenericOkayPopup.xaml
    /// </summary>
    public partial class GenericOkayPopup : Window
    {
        public String MESSAGE { get; set; }
        public String OK_TEXT { get; set; }
        public String CANCEL_TEXT { get; set; }

        public Action? OkayAction { get; set; } = null;
        public Action? CancelAction { get; set; } = null;

        public bool HasOkayButton { get; set; } = true;
        public bool HasCancelButton { get; set; } = true;

        public GenericOkayPopup()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OkayButtonClick(object sender, RoutedEventArgs e)
        {
            OkayAction?.Invoke();
            DialogResult = true;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            CancelAction?.Invoke();
            DialogResult = false;
        }
    }
}
