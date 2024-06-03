using System.Windows;
using BookingApp.Model;
using System.Windows.Controls;
using BookingApp.ViewModel.Guide;
using BookingApp.ViewModel.Tourist;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for ComplexTourRequestsView.xaml
    /// </summary>
    public partial class ComplexTourRequestsView : Window
    {
        private ComplexTourRequestsViewModel complexTourRequestsViewModel;

        public ComplexTourRequestsView(int userId)
        {
            InitializeComponent();
            complexTourRequestsViewModel = new ComplexTourRequestsViewModel(userId);
            DataContext = complexTourRequestsViewModel;
        }

        public void btnAcceptTourRequest_Click(object sender, RoutedEventArgs e)
        {
            complexTourRequestsViewModel.AcceptTourRequest((ComplexTourRequest)((Button)sender).Tag, (TourRequest)((Button)sender).CommandParameter);
        }
    }
}
