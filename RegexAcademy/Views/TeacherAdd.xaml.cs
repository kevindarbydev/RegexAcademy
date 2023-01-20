using Microsoft.Win32;
using RegexAcademy.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Windows.Forms.AxHost;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for TeacherEditDeleteDlg.xaml
    /// </summary>
    public partial class TeacherAdd : Window
    {
        private string imagePath = null;

        public TeacherAdd()
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new RegexAcademyDbContext(); // Exceptions
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                // Close();
                Environment.Exit(1);
            }
        }

        private void BtnAddTeacher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool availability = RetrieveRbnValue();
                byte[] profileImage = ValidateProfileImage(); 

                Teacher t1 = new Teacher(TbxFirstName.Text, TbxLastName.Text, TbxEmail.Text, profileImage, availability); 

                Globals.dbContext.Teachers.Add(t1);
                int results = Globals.dbContext.SaveChanges(); // ex SystemException
                if (results > 0)
                {
                    this.DialogResult = true;
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Database error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private byte[] ValidateProfileImage()
        {
            byte[] profileImage = null;
            if(imagePath == null)
            {
                return profileImage;
            } else
            {
                profileImage = File.ReadAllBytes(imagePath); // ex SystemException
                return profileImage;
            }
           
        }

        private bool RetrieveRbnValue()
        {

            if (RbnYes.IsChecked == true)
            {
                return true;
            }
            else if (RbnNo.IsChecked == true)
            {
                return false;
            }
            else
            { // internal error
                MessageBox.Show(this, "Error reading radio buttons state", "Internal error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void BtnUploadImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                var profileImage = new BitmapImage(new Uri(op.FileName)); // cropping image
                ImgProfileImage.Source = new CroppedBitmap(profileImage, new Int32Rect(0, 0, 120, 120));
                imagePath = op.FileName;
            }
        }
    }
}
