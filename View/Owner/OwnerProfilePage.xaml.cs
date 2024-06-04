using System.Windows.Controls;
using BookingApp.Model;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    public partial class OwnerProfilePage : Page
    {
        private readonly OwnerProfileViewModel _ownerProfileViewModel;
        public OwnerProfilePage(User user)
        {
            InitializeComponent();
            _ownerProfileViewModel = new OwnerProfileViewModel(user);
            DataContext = _ownerProfileViewModel;
        }
    }
}
