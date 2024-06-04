using System.Windows.Controls;
using System.Windows.Input;
using BookingApp.Model;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for LastCheckoutsView.xaml
    /// </summary>
    public partial class LastCheckoutsView : Page
    {
        public readonly LastCheckoutsViewModel _lastCheckoutsViewModel;
        public LastCheckoutsView(User user)
        {
            InitializeComponent();
            _lastCheckoutsViewModel = new LastCheckoutsViewModel(user);
            DataContext = _lastCheckoutsViewModel;
            checkout.MouseDoubleClick += ShowGuestRatingWindow;
        }

        private void ShowGuestRatingWindow(object sender, MouseButtonEventArgs e)
        {
            _lastCheckoutsViewModel.ShowGuestRatingWindow(sender, e, checkout);
        }
    }
}
