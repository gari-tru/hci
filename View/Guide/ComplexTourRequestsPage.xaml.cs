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
