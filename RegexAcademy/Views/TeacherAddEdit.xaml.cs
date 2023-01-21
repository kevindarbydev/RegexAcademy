using Microsoft.Win32;
using RegexAcademy.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for TeacherAddEdit.xaml
    /// </summary>
    public partial class TeacherAddEdit : Window
    {
        private string imagePath = null;
        private Teacher currSelTeacher = null;

        // Constructor for AddTeacher Window
        public TeacherAddEdit()
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

        // Constructor for EditTeacher Window
        public TeacherAddEdit(Teacher currSelTeacher)
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new RegexAcademyDbContext(); // Exceptions
                BtnUpdateTeacher.Visibility = Visibility.Visible;
                BtnAddTeacher.Visibility = Visibility.Hidden;

            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                // Close();
                Environment.Exit(1);
            }
            if (currSelTeacher != null)
            {
                this.currSelTeacher = currSelTeacher;
                TbxFirstName.Text = currSelTeacher.FirstName;
                TbxLastName.Text = currSelTeacher.LastName;
                TbxEmail.Text = currSelTeacher.Email;
                SetRadioButton(currSelTeacher);
                ImgProfileImage.Source = currSelTeacher.ProfileImageToShow;
            }
        }


        // ----------------------- CRUD ----------------------- 
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
                    MessageBox.Show(this, "Teacher added successfully!", "Update success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

        private void BtnUpdateTeacher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool availability = RetrieveRbnValue();
                byte[] profileImage = ValidateProfileImage();

                if (currSelTeacher == null) return;
                Teacher teacherToUpdate = Globals.dbContext.Teachers.Where(s => s.Id == currSelTeacher.Id).FirstOrDefault();
                teacherToUpdate.FirstName = TbxFirstName.Text;
                teacherToUpdate.LastName = TbxLastName.Text;
                teacherToUpdate.Email = TbxEmail.Text;
                teacherToUpdate.Availability = availability;
                if (profileImage != null)
                {
                    teacherToUpdate.ProfileImage = profileImage;
                }


                int results = Globals.dbContext.SaveChanges(); // ex SystemException
                if (results > 0)
                {
                    MessageBox.Show(this, "Teacher updated successfully!", "Update success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

        // ----------------------- VALIDATION/RETRIEVAL ----------------------- 

        private byte[] ValidateProfileImage()
        {
            byte[] profileImage = null;
            if (imagePath == null)
            {
                return profileImage;
            }
            else
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

        private void SetRadioButton(Teacher currSelTeacher)
        {
            if (currSelTeacher.Availability == true)
            {
                RbnYes.IsChecked = true;
            }
            else if (currSelTeacher.Availability == false)
            {
                RbnNo.IsChecked = true;
            }
            else
            { // internal error
                MessageBox.Show(this, "Error reading radio buttons state", "Internal error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ----------------------- IMAGE UPLOAD ----------------------- 
        private void BtnUploadImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imagePath = op.FileName;
                //FIXME: image cropping is not really working well
                //var profileImage = new BitmapImage(new Uri(op.FileName)); 
                //ImgProfileImage.Source = new CroppedBitmap(profileImage, new Int32Rect(120, 120, 240, 240));

                var profileImage = new BitmapImage(new Uri(op.FileName));
                var croppedProfileImage = Globals.CropsImage(profileImage);
                if (croppedProfileImage != null)
                {
                    MessageBox.Show("Image was cropped because it is too large. \n 400px by 400px is the cutoff! \n Select another if you don't like it!", "Image Cropped",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    ImgProfileImage.Source = croppedProfileImage;
                }
                else
                {
                    ImgProfileImage.Source = profileImage;
                }

            }
        }


    }
}
