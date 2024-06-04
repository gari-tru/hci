using System.Windows;
using BookingApp.Command;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for GuideMainWindowView.xaml
    /// </summary>
    public partial class GuideMainWindowView : Window
    {
        public GuideMainWindowView(int userId)
        {
            InitializeComponent();
            DataContext = new GuideMainWindowViewModel(userId, NavigationService);
        }
    }
}
