using System.Windows;
using System.Windows.Input;
using BookingApp.Model;
using BookingApp.ViewModel;

namespace BookingApp.View
{
    public partial class GuestMainWindow : Window
    {
        private readonly User user;
        private readonly GuestMainWindowModel _viewModel;
        private readonly RescheduleRequestNotificationViewModel _rescheduleRequestNotificationViewModel;

        public GuestMainWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            _viewModel = new GuestMainWindowModel(user);
            DataContext = _viewModel;
            _rescheduleRequestNotificationViewModel = new RescheduleRequestNotificationViewModel(user);
            Loaded += GuestMainWindow_Loaded;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }

        private void GuestMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int unreadNotifications = _rescheduleRequestNotificationViewModel.UnreadNotifications;
            if (unreadNotifications > 0)
            {
                string message = "Imate " + unreadNotifications + " ne pročitano/a obaveštenja.";
                MessageBox.Show(message, "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ShowAccommodationOverview(object sender, RoutedEventArgs e)
        {
            titleBar.ShowAccommodationOverview(sender, e);
        }

        private void OpenManageReservationsView(object sender, RoutedEventArgs e)
        {
            titleBar.OpenManageReservationsView(sender, e);
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void OpenOwnerRatingsView(object sender, RoutedEventArgs e)
        {
            titleBar.OpenOwnerRatingsView(sender, e);
        }
    }
}
