using System.Windows.Controls;
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
