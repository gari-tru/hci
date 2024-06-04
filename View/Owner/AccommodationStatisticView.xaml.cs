using System.Windows.Controls;
using System.Windows.Navigation;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    public partial class AccommodationStatisticView : Page
    {
        private readonly AccommodationStatisticViewModel _accommodationStatisticViewModel;
        public AccommodationStatisticView(NavigationService navService)
        {
            InitializeComponent();
            _accommodationStatisticViewModel = new AccommodationStatisticViewModel(navService);
            DataContext = _accommodationStatisticViewModel;
        }
    }
}
