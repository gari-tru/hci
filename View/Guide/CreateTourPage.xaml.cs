using System.Windows;
using System.Windows.Controls;
using BookingApp.Model;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for CreateTourPage.xaml
    /// </summary>
    public partial class CreateTourPage : Page
    {
        private CreateTourViewModel createTourViewModel;
        private Frame _navigationService;
        private readonly int _userId;

        public CreateTourPage(TourRequest tourRequest, int userId, Frame navigationService)
        {
            InitializeComponent();
            createTourViewModel = new CreateTourViewModel(tourRequest, userId);
            DataContext = createTourViewModel;
            _navigationService = navigationService;
            _userId = userId;
        }

        private void btnCreateTour_Click(object sender, RoutedEventArgs e)
        {
            if (createTourViewModel.CreateTour())
            {
                _navigationService.Navigate(new ScheduledToursPage(_userId, _navigationService));
            }
        }

        private void btnUploadImages_Click(object sender, RoutedEventArgs e)
        {
            createTourViewModel.UploadImages();
        }
    }
}
