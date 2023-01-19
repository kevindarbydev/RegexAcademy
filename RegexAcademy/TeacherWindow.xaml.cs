using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegexAcademy
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
                MessageBox.Show("Hello World");
            }
        }

        private void BtnEditTeacher_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteTeacher_Click(object sender, RoutedEventArgs e)
        {

        }



        
    }
}
