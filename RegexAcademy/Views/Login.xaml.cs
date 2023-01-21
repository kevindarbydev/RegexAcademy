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
using RegexAcademy.Models;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new RegexAcademyDbContext();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        private void BtnRegisterWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Register RegDlg = new Register();
            RegDlg.ShowDialog();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = TbxUsername.Text;

                if(username == null)
                {
                    throw new ArgumentException("Username is required.");
                }
                if(TbxPasword.Password == null)
                {
                    throw new ArgumentException("Password is required.");
                }

                User foundUser = Globals.dbContext.Users.Find(username);

                if (foundUser == null)
                {
                    throw new ArgumentException($"{username} does not exist.");
                }

                string password = PasswordEncryptor.EncryptPassword(TbxPasword.Password);

                if (foundUser.Password != password)
                {
                    TbxPasword.Password = "";
                    throw new ArgumentException("Invalid Password, please try again.");
                }

                MessageBox.Show(this, "Login Successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();    
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
