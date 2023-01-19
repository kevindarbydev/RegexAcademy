using System.Windows;
using System.Windows.Controls;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for TeacherEditDeleteDlg.xaml
    /// </summary>
    public partial class TeacherEditDeleteDlg : Window
    {
        public TeacherEditDeleteDlg()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
