using System.Windows.Controls;
using BookingApp.Dto;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for TourStatisticsPage.xaml
    /// </summary>
    public partial class TourStatisticsPage : Page
    {
        public TourStatisticsPage(TourDto tourDto)
        {
            InitializeComponent();
            DataContext = new TourStatisticsViewModel(tourDto);
        }
    }
}
