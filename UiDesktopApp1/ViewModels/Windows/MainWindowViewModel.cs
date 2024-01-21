using System.Collections.ObjectModel;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace UiDesktopApp1.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "Luxury Launcher";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new ObservableCollection<object>
        {
            new NavigationViewItem()
            {
                Content = "Home",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DashboardPage)
            },
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new ObservableCollection<object>
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _newsFooterMenuItems = new ObservableCollection<object>
        {
            new NavigationViewItem()
            {
                Content = "News",
                Icon = new SymbolIcon { Symbol = SymbolRegular.News20 },
                TargetPageType = typeof(Views.Pages.NewsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new ObservableCollection<MenuItem>
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    } 
};
