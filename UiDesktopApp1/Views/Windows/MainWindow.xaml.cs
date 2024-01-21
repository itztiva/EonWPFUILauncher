using UiDesktopApp1.ViewModels.Windows;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace UiDesktopApp1.Views.Windows
{
    public partial class MainWindow
    {
        public MainWindowViewModel ViewModel { get; }

        public MainWindow(
            MainWindowViewModel viewModel,
            INavigationService navigationService,
            IServiceProvider serviceProvider,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService
        )
        {
            Wpf.Ui.Appearance.Watcher.Watch(this);

            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            navigationService.SetNavigationControl(NavigationView);
            snackbarService.SetSnackbarPresenter(SnackbarPresenter);
            contentDialogService.SetContentPresenter(RootContentDialog);

            NavigationView.SetServiceProvider(serviceProvider);

            // Add the StateChanged event handler
            StateChanged += Window_StateChanged;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                // Prevent the window from being maximized
                WindowState = WindowState.Normal;
            }
        }
    }
}
