﻿using System;
using System.Linq;
using System.Windows;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for StudentAdd.xaml
    /// </summary>
    public partial class StudentAdd : Window
    {
        //this will stay null unless secondary constructor is called
        public Models.Student currentlySelected = null;


        //primary constructory
        public StudentAdd()
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

        //secondary constructor
        public StudentAdd(Models.Student currentlySelected)
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new RegexAcademyDbContext(); // systemEx, DBExcep
                BtnUpdateStudent.Visibility = Visibility.Visible;
                BtnSaveStudent.Visibility = Visibility.Hidden;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                Environment.Exit(1);
            }
            if (currentlySelected != null)
            {
                this.currentlySelected = currentlySelected;
                TbxFirstName.Text = currentlySelected.FirstName;
                TbxLastName.Text = currentlySelected.LastName;
                DpAddStudent.SelectedDate = currentlySelected.DateOfBirth;
            }
        }

        private void BtnCancelAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSaveStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DpAddStudent.SelectedDate == null)
                {
                    MessageBox.Show("Please select a date from the date picker before continuing.", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                Models.Student newStudent = new Models.Student { FirstName = TbxFirstName.Text, LastName = TbxLastName.Text, DateOfBirth = (DateTime)DpAddStudent.SelectedDate }; // argumentExcep
                Globals.dbContext.Students.Add(newStudent);
                Globals.dbContext.SaveChanges();
                MessageBox.Show(this, "Student saved successfully!", "Student added", MessageBoxButton.OK, MessageBoxImage.None);
                ResetFields();

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, "Error validating inputs\n" + ex.Message, "Invalid input",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex) when (ex is SystemException || ex is InvalidOperationException)
            {
                MessageBox.Show(this, "Error saving to DB\n" + ex.Message, "Saving failed",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetFields()
        {
            TbxFirstName.Text = "";
            TbxLastName.Text = "";
            DpAddStudent.SelectedDate = null;
        }

        private void BtnUpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentlySelected == null) return;
                string updateMsg = $"Original: {currentlySelected}\n-------------\nUpdated: ";
                Models.Student toBeUpdated = Globals.dbContext.Students.Where(s => s.Id == currentlySelected.Id).FirstOrDefault(); //argNull
                toBeUpdated.FirstName = TbxFirstName.Text;
                toBeUpdated.LastName = TbxLastName.Text;
                toBeUpdated.DateOfBirth = (DateTime)DpAddStudent.SelectedDate;
                updateMsg += $"{toBeUpdated}";
                Globals.dbContext.SaveChanges();
                MessageBox.Show(this, updateMsg, "Update success",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error saving to DB\n" + ex.Message, "Update failed",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Something went wrong: " + ex.Message, "Fatal error",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
