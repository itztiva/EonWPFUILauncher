using UiDesktopApp1.ViewModels.Pages;
using Wpf.Ui.Controls;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System;
using DiscordRPC;
using Luxury;
using System.Diagnostics;
using System.Security.Policy;
using UiDesktopApp1.Views.Pages;

namespace UiDesktopApp1.Views.Pages
{
    public partial class SettingsPage : INavigableView<SettingsViewModel>
    {
        public SettingsViewModel ViewModel { get; }

        public SettingsPage(SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
            LoadSettings();
        }

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = null;
            this.Cursor = System.Windows.Input.Cursors.Hand;
        }

        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Cursor = System.Windows.Input.Cursors.Arrow;
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void Button_MouseEnterI(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = null;
            this.Cursor = System.Windows.Input.Cursors.IBeam;
        }

        private void Button_MouseLeaveI(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Cursor = System.Windows.Input.Cursors.Arrow;
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create a SaveFileDialog
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();

           
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.DefaultExt = ".txt";

            
            bool? result = saveFileDialog.ShowDialog();

           
            if (result == true)
            {
                
                string filePath = saveFileDialog.FileName;

                try
                {
                   
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                    {
                        file.WriteLine("This is the content that will be saved to the file.");
                        
                    }

                    
                    System.Windows.MessageBox.Show("File saved successfully!");
                }
                catch (Exception ex)
                {
                    
                    System.Windows.MessageBox.Show("An error occurred while saving the file: " + ex.Message);
                }
            }
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void PasswordBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void button12_Click_1(object sender, RoutedEventArgs e)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Eon");
            string filePath = Path.Combine(folderPath, "Settings.ini");
            string email = textBox1.Text;
            string password = textBox2.Text;
            string directory = textBox3.Text;

            if (!Directory.Exists(folderPath))
            {
               
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(filePath) || !File.ReadAllText(filePath).Contains("[Account]\nEmail=\nPassword=\nDirectory=\nAccountId=123"))
            {
               
                File.WriteAllText(filePath, "[Account]\nEmail=\nPassword=\nDirectory=\nAccountId=123");
            }

            var lines = File.ReadAllLines(filePath).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("Email="))
                {
                    lines[i] = "Email=" + email;
                }
                else if (lines[i].StartsWith("Password="))
                {
                    lines[i] = "Password=" + password;
                }
                else if (lines[i].StartsWith("Directory="))
                {
                    lines[i] = "Directory=" + directory;
                }
            }
            File.WriteAllLines(filePath, lines);
        }

        private void LoadSettings()
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Eon");
            string filePath = Path.Combine(folderPath, "Settings.ini");

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].StartsWith("Email="))
                    {
                        textBox1.Text = lines[i].Substring("Email=".Length);
                    }
                    else if (lines[i].StartsWith("Password="))
                    {
                        textBox2.Text = lines[i].Substring("Password=".Length);
                    }
                    else if (lines[i].StartsWith("Directory="))
                    {
                        textBox3.Text = lines[i].Substring("Directory=".Length);
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.SelectedPath = textBox3.Text;

                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    textBox3.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            if (textBox2.Visibility == Visibility.Visible)
            {
                textBox2.Visibility = Visibility.Collapsed;
            }
            else
            {
                textBox2.Visibility = Visibility.Visible;
            }
        }

        private void button16_Click(object sender, RoutedEventArgs e)
        {
     /*       try
            {
                // Create an instance of NewsPage directly
                UiDesktopApp1.Views.Pages.NewsPage newsPage = new UiDesktopApp1.Views.Pages.NewsPage();
                newsPage.ShowDialog();
            }
            catch (Exception ex)
            {
                // Show an error message using MessageBox
     */         //  System.Windows.MessageBox.Show($"An error occurred: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
          //  }
        }




        private void button14_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Eon");
            string filePath = Path.Combine(folderPath, "Settings.ini");
            string directory = "";

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].StartsWith("Directory="))
                    {
                        directory = lines[i].Substring("Directory=".Length);
                    }
                }
            }
            {

            }
        }

        private void textBox2_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
