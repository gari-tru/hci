using System.Windows;
using System.Windows.Controls;
using BookingApp.Model;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for ComplexTourRequestsPage.xaml
    /// </summary>
    public partial class ComplexTourRequestsPage : Page
    {
        private ComplexTourRequestsViewModel complexTourRequestsViewModel;

        public ComplexTourRequestsPage(int userId, Frame navigationService)
        {
            InitializeComponent();
            complexTourRequestsViewModel = new ComplexTourRequestsViewModel(userId, navigationService);
            DataContext = complexTourRequestsViewModel;
        }

        public void btnAcceptTourRequest_Click(object sender, RoutedEventArgs e)
        {
            complexTourRequestsViewModel.AcceptTourRequest((ComplexTourRequest)((Button)sender).Tag, (TourRequest)((Button)sender).CommandParameter);
        }
    }
}
