using System.Windows.Controls;
using System.Windows.Navigation;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    public partial class SingleRescheduleRequestPage : Page
    {
        private readonly SingleRescheduleRequestViewModel _viewModel;
        public SingleRescheduleRequestPage(int rescheduleReservationRequest, NavigationService navService)
        {
            InitializeComponent();
            _viewModel = new SingleRescheduleRequestViewModel(rescheduleReservationRequest, navService);
            _viewModel.OnNavigatedTo();
            DataContext = _viewModel;
        }
    }
}
