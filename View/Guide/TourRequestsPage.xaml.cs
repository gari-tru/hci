using System.Windows;
using System.Windows.Controls;
using BookingApp.Model;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for TourRequestsPage.xaml
    /// </summary>
    public partial class TourRequestsPage : Page
    {
        private TourRequestsViewModel tourRequestsViewModel;

        public TourRequestsPage(int userId, Frame navigationService)
        {
            InitializeComponent();
            tourRequestsViewModel = new TourRequestsViewModel(userId, navigationService);
            DataContext = tourRequestsViewModel;
        }

        public void btnAcceptTourRequest_Click(object sender, RoutedEventArgs e)
        {
            tourRequestsViewModel.HandleTourRequest((TourRequest)((Button)sender).CommandParameter, TourRequestStatus.Accepted);
        }

        public void btnRejectTourRequest_Click(object sender, RoutedEventArgs e)
        {
            tourRequestsViewModel.HandleTourRequest((TourRequest)((Button)sender).CommandParameter, TourRequestStatus.Invalid);
        }
    }
}
