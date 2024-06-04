using System.Windows.Controls;
using System.Windows.Navigation;
using BookingApp.Model;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    public partial class OwnerReviews : Page
    {
        private readonly OwnerReviewsViewModel _ownerReviewsViewModel;
        public OwnerReviews(User user, NavigationService navService)
        {
            InitializeComponent();
            _ownerReviewsViewModel = new OwnerReviewsViewModel(user, navService);
            DataContext = _ownerReviewsViewModel;
        }
    }
}