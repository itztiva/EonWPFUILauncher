using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using UiDesktopApp1.ViewModels.Pages;

namespace UiDesktopApp1.Views.Pages
{
    public partial class NewsPage : Page
    {
        public NewsPageViewModel ViewModel { get; }
        public NewsPage()
        {
            InitializeComponent();
        }
        public NewsPage(NewsPageViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
        }
        internal void Show()
        {
            NavigationService?.Navigate(this);
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.ShowDialog();
            FileTextBox.Text = sfd.FileName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            client.DownloadFileAsync(new Uri(URLTextBox.Text), FileTextBox.Text);
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DownloadProgressBar.Value = e.ProgressPercentage;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
           System.Windows.MessageBox.Show("File Download Complete!");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        internal void ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}
