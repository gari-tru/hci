using System.Windows;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for GuideMainWindow.xaml
    /// </summary>
    public partial class GuideMainWindow : Window
    {
        private readonly int userId;

        public GuideMainWindow(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            SuperGuideViewModel superGuideViewModel = new SuperGuideViewModel(userId);
        }

        private void btnCreateTourView_Click(object sender, RoutedEventArgs e)
        {
            CreateTourView createTourView = new CreateTourView(null, userId);
            createTourView.ShowDialog();
        }

        private void btnScheduledToursView_Click(object sender, RoutedEventArgs e)
        {
            ScheduledToursView scheduledToursView = new ScheduledToursView(userId);
            scheduledToursView.ShowDialog();
        }

        private void btnFinishedToursView_Click(object sender, RoutedEventArgs e)
        {
            FinishedToursView finishedToursView = new FinishedToursView(userId);
            finishedToursView.ShowDialog();
        }

        private void btnTourRequestsView_Click(object sender, RoutedEventArgs e)
        {
            TourRequestsView tourRequestsView = new TourRequestsView(userId);
            tourRequestsView.ShowDialog();
        }

        private void btnTourRequestStatisticsView_Click(object sender, RoutedEventArgs e)
        {
            TourRequestStatisticsView tourRequestStatisticsView = new TourRequestStatisticsView(userId);
            tourRequestStatisticsView.ShowDialog();
        }

        private void btnDismissalView_Click(object sender, RoutedEventArgs e)
        {
            DismissalViewModel dismissalViewModel = new DismissalViewModel(userId);
            Close();
        }

        private void btnComplexTourRequestsView_Click(object sender, RoutedEventArgs e)
        {
            ComplexTourRequestsView complexTourRequestsView = new ComplexTourRequestsView(userId);
            complexTourRequestsView.ShowDialog();
        }
    }
}
