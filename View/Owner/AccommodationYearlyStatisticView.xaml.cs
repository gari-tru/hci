using System.Windows.Controls;
using System.Windows.Navigation;
using BookingApp.Dto;
using BookingApp.ViewModel.Owner;


namespace BookingApp.View
{
    public partial class AccommodationYearlyStatisticView : Page
    {
        private readonly AccommodationYearlyStatisticViewModel _accommodationYearlyStatisticViewModel;
        public AccommodationYearlyStatisticView(AccommodationStatisticDto accommodationStatisticDto, NavigationService navService)
        {
            InitializeComponent();
            _accommodationYearlyStatisticViewModel = new AccommodationYearlyStatisticViewModel(accommodationStatisticDto, navService);
            DataContext = _accommodationYearlyStatisticViewModel;
        }
    }
}
