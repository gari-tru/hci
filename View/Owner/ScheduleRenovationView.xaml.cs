using System.Windows.Controls;
using System.Windows.Navigation;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for ScheduleRenovationView.xaml
    /// </summary>
    public partial class ScheduleRenovationView : Page
    {
        private readonly ScheduleRenovationViewModel _viewModel;
        public ScheduleRenovationView(int accommodationId, NavigationService navService)
        {
            InitializeComponent();
            _viewModel = new ScheduleRenovationViewModel(accommodationId, navService);
            DataContext = _viewModel;
        }
    }
}
