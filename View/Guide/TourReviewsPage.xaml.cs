using System.Windows.Controls;
using BookingApp.Dto;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for TourReviewsPage.xaml
    /// </summary>
    public partial class TourReviewsPage : Page
    {
        public TourReviewsPage(TourDto tourDto)
        {
            InitializeComponent();
            DataContext = new TourReviewsViewModel(tourDto);
        }
    }
}
