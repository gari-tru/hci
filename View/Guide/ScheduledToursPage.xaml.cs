using System.Windows.Controls;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for ScheduledToursPage.xaml
    /// </summary>
    public partial class ScheduledToursPage : Page
    {
        public ScheduledToursPage(int userId, Frame navigationService)
        {
            InitializeComponent();
            DataContext = new ScheduledToursViewModel(userId, navigationService);
        }
    }
}
