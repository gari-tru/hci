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
using BookingApp.Dto;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for LiveTourTrackingPage.xaml
    /// </summary>
    public partial class LiveTourTrackingPage : Page
    {
        public LiveTourTrackingPage(TourDto tourDto, Frame navigationService)
        {
            InitializeComponent();
            DataContext = new LiveTourTrackingViewModel(tourDto, navigationService);
        }
    }
}
