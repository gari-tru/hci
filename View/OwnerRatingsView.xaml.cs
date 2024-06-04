using System.Windows;
using System.Windows.Input;
using BookingApp.Model;
using BookingApp.ViewModel;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for OwnerRatingView.xaml
    /// </summary>
    public partial class OwnerRatingsView : Window
    {
        private User user;
        //private readonly RateAccommodationViewModel viewModel;
        private readonly OwnerRatingsViewModel _viewModel;
        public OwnerRatingsView(User user)
        {
            InitializeComponent();
            this.user = user;
            _viewModel = new OwnerRatingsViewModel(user);
            DataContext = _viewModel;

        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }

    }
}
