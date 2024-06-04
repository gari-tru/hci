using System.Windows.Controls;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for ShowAllRenovationView.xaml
    /// </summary>
    public partial class ShowAllRenovationView : Page
    {
        private readonly ShowAllRenovationsViewModel _showAllRenovationViewModel;
        public ShowAllRenovationView()
        {
            InitializeComponent();
            _showAllRenovationViewModel = new ShowAllRenovationsViewModel();
            DataContext = _showAllRenovationViewModel;
        }
    }
}
