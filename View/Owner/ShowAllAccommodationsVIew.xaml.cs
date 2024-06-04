using System.Windows.Controls;
using System.Windows.Navigation;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    public partial class ShowAllAccommodationsVIew : Page
    {
        private readonly ShowAllAccommodationsViewModel _showAllAccommodationsViewModel;
        public ShowAllAccommodationsVIew(int currentUserId, NavigationService navService)
        {
            InitializeComponent();
            _showAllAccommodationsViewModel = new ShowAllAccommodationsViewModel(currentUserId, navService);
            DataContext = _showAllAccommodationsViewModel;
        }
    }
}
