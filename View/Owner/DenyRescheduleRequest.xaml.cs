using System.Windows.Controls;
using System.Windows.Navigation;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    public partial class DenyRescheduleRequest : Page
    {
        private readonly DenyRescheduleRequestViewModel _denyRescheduleRequestViewModel;
        public DenyRescheduleRequest(int rescheduleReservationRequestId, NavigationService navService)
        {
            InitializeComponent();
            _denyRescheduleRequestViewModel = new DenyRescheduleRequestViewModel(rescheduleReservationRequestId, navService);
            DataContext = _denyRescheduleRequestViewModel;
        }
    }
}
