using System.Windows;
using System.Windows.Navigation;
using BookingApp.Model;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    public partial class OwnerMainWindow : Window
    {
        private readonly OwnerMainWindowViewModel _ownerMainWindowViewModel;
        public OwnerMainWindow(User user)
        {
            InitializeComponent();
            _ownerMainWindowViewModel = new OwnerMainWindowViewModel(user, this.MainFrame);
            DataContext = _ownerMainWindowViewModel;
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
