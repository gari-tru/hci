using System.Windows.Controls;
using BookingApp.Dto;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for MarkTouristsPage.xaml
    /// </summary>
    public partial class MarkTouristsPage : Page
    {
        public MarkTouristsPage(TourDto tourDto, Frame navigationService)
        {
            InitializeComponent();
            DataContext = new MarkTouristsViewModel(tourDto, navigationService);
        }
    }
}
