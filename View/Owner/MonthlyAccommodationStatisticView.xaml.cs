using System.Windows.Controls;
using System.Windows.Navigation;
using BookingApp.Dto;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for MonthlyAccommodationStatisticView.xaml
    /// </summary>
    public partial class MonthlyAccommodationStatisticView : Page
    {
        private readonly MonthlyAccommodationStatisticViewModel _monthlyAccommodationStatisticViewModel;
        public MonthlyAccommodationStatisticView(AccommodationStatisticDto accommodationStatistic, NavigationService navService)
        {
            InitializeComponent();
            _monthlyAccommodationStatisticViewModel = new MonthlyAccommodationStatisticViewModel(accommodationStatistic, navService);
            DataContext = _monthlyAccommodationStatisticViewModel;
        }
    }
}
