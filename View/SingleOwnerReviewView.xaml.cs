using System.Windows.Controls;
using System.Windows.Navigation;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    public partial class SingleOwnerReviewView : Page
    {
        private readonly SingleOwnerReviewViewModel _viewModel;
        public SingleOwnerReviewView(int ratingId, NavigationService navService)
        {
            InitializeComponent();
            _viewModel = new SingleOwnerReviewViewModel(ratingId, navService);
            DataContext = _viewModel;
        }
    }
}
