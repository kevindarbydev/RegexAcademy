using RegexAcademy.Models;
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

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();

            try
            {
                Globals.dbContext = new RegexAcademyDbContext();
            }
            catch(SystemException ex) 
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        public void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = TbxUsername.Text;
                string password = TbxPasword.Password;
                string confirmPassword = TbxConfirmPassword.Password;

                if(!User.isUsernameValid(username, out string usernameError)) 
                {
                    MessageBox.Show(this, usernameError, "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(!User.isPasswordValid(password, out string passwordError) || password == null)
                {
                    MessageBox.Show(this, passwordError, "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (confirmPassword == null || confirmPassword != password)
                {
                    MessageBox.Show(this, "Passwords do not match.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                User user = new User { Username = username, Password = PasswordEncryptor.EncryptPassword(password) };

                Globals.dbContext.Users.Add(user);

                Globals.dbContext.SaveChanges(); //SystemException

                MessageBox.Show(this, "Registration Successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Hide();

                Login LoginDlg = new Login();
                LoginDlg.ShowDialog();
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(SystemException ex)
            {
                MessageBox.Show(this, ex.Message, "Database Operation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnLogInPage_Click(object sender, RoutedEventArgs e)
        { 
            this.Hide();
            Login LoginDlg = new Login();
            LoginDlg.ShowDialog();
        }
    }
}
