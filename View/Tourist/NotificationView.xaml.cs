using System.Windows;
using BookingApp.Model;
using BookingApp.ViewModel.Tourist;

namespace BookingApp.View.Tourist
{
    public partial class NotificationView : Window
    {
        private NewTourNotificationViewModel notificationViewModel;
        private readonly User user;

        public NotificationView(User user)
        {
            InitializeComponent();
            this.user = user;
            notificationViewModel = new NewTourNotificationViewModel(user);
            DataContext = notificationViewModel;
        }
    }

}
