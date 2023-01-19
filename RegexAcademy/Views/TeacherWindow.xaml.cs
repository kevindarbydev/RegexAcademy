using System.Windows;
using System.Windows.Controls;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Page
    {
        public TeacherWindow()
        {
            InitializeComponent();
        }

        private void LvTeachers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnAddTeacher_Click(object sender, RoutedEventArgs e)
        {
            TeacherAdd inputDialog = new TeacherAdd();
            if (inputDialog.ShowDialog() == true)
            {
                MessageBox.Show("Add Teacher Window was closed");
            }
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    TeacherEditDeleteDlg inputDialog = new TeacherEditDeleteDlg();
        //    if (inputDialog.ShowDialog() == true)
        //    {
        //        MessageBox.Show("Hello World");
        //    }
        //}

        //private void btnOpen_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog op = new OpenFileDialog();
        //    op.Title = "Select a picture";
        //    op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
        //      "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
        //      "Portable Network Graphic (*.png)|*.png";
        //    if (op.ShowDialog() == true)
        //    {
        //        imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
        //    }

        //}
    }
}
