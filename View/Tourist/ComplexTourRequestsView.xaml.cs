using System.Windows;
using BookingApp.Model;
using BookingApp.ViewModel.Tourist;

namespace BookingApp.View.Tourist
{
    public partial class ComplexTourRequestsView : Window
    {
        private readonly User user;
        private readonly ComplexTourRequestViewModel _complexTourRequestsViewModel;

        public ComplexTourRequestsView(User user)
        {
            InitializeComponent();
            this.user = user;
            _complexTourRequestsViewModel = new ComplexTourRequestViewModel(user);
            DataContext = _complexTourRequestsViewModel;
        }
    }
}
