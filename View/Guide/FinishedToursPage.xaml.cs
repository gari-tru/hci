using System.Windows.Controls;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for FinishedToursPage.xaml
    /// </summary>
    public partial class FinishedToursPage : Page
    {
        public FinishedToursPage(int userId, Frame navigationService)
        {
            InitializeComponent();
            DataContext = new FinishedToursViewModel(userId, navigationService);
        }
    }
}
