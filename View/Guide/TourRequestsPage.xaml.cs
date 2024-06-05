using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
