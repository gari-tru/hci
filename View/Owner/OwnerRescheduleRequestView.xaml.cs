using System.Windows.Controls;
using System.Windows.Navigation;
using BookingApp.Model;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    public partial class OwnerRescheduleRequestView : Page
    {
        private readonly OwnerRescheduleRequestViewModel _ownerRescheduleRequestViewModel;
        public OwnerRescheduleRequestView(User user, NavigationService navService)
        {
            InitializeComponent();
            _ownerRescheduleRequestViewModel = new OwnerRescheduleRequestViewModel(user, navService);
            DataContext = _ownerRescheduleRequestViewModel;
        }
    }
}
