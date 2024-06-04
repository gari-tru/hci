using System.Windows;
using System.Windows.Controls;
using BookingApp.Model;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    public partial class AddAccommodationView : Page
    {
        public readonly AddAccommodationViewModel _viewModel;

        public AddAccommodationView(User user)
        {
            InitializeComponent();
            _viewModel = new AddAccommodationViewModel(user);
            DataContext = _viewModel;

        }

        private void AddAccommodation(object sender, RoutedEventArgs e)
        {
            _viewModel.AddAccommodation();
        }

        private void AddImages(object sender, RoutedEventArgs e)
        {
            _viewModel.AddImages();
        }

    }
}
