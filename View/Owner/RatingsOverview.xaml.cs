using System.Windows.Controls;
using BookingApp.Model;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    public partial class RatingsOverview : Page
    {
        public readonly RatingsOverViewModel _ratingsOverViewModel;
        public RatingsOverview(User user)
        {
            InitializeComponent();
            _ratingsOverViewModel = new RatingsOverViewModel(user);
            DataContext = _ratingsOverViewModel;
        }
    }
}
