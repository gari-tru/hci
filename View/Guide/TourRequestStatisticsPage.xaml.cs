using System.Windows;
using System.Windows.Controls;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for TourRequestStatisticsPage.xaml
    /// </summary>
    public partial class TourRequestStatisticsPage : Page
    {
        TourRequestStatisticsViewModel tourRequestStatisticsViewModel;

        public TourRequestStatisticsPage(int userId, Frame navigationService)
        {
            InitializeComponent();
            tourRequestStatisticsViewModel = new TourRequestStatisticsViewModel(userId, navigationService);
            DataContext = tourRequestStatisticsViewModel;
        }

        private void LocationComboBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            LanguageComboBox.SelectedItem = null;
        }

        private void LanguageComboBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            LocationComboBox.SelectedItem = null;
        }

        private void btnMostWantedLocation_Click(object sender, RoutedEventArgs e)
        {
            tourRequestStatisticsViewModel.CreateTour("location");
        }

        private void btnMostWantedLanguage_Click(object sender, RoutedEventArgs e)
        {
            tourRequestStatisticsViewModel.CreateTour("language");
        }
    }
}
