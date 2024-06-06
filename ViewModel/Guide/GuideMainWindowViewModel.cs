using System.Windows.Controls;
using BookingApp.Command;
using BookingApp.View.Guide;

namespace BookingApp.ViewModel.Guide
{
    class GuideMainWindowViewModel
    {
        private readonly int userId;
        public Frame NavigationService { get; set; }
        public RelayCommand NavigateBack { get; set; }
        public RelayCommand NavigateScheduledTours { get; set; }
        public RelayCommand NavigateCreateTour { get; set; }
        public RelayCommand NavigateFinishedTours { get; set; }
        public RelayCommand NavigateTourRequests { get; set; }

        public GuideMainWindowViewModel(int userId, Frame navigationService)
        {
            this.userId = userId;
            NavigationService = navigationService;
            NavigationService.Navigate(new ScheduledToursPage(userId, navigationService));
            NavigateBack = new RelayCommand(NavigateBackExecute);
            NavigateScheduledTours = new RelayCommand(NavigateScheduledToursExecute);
            NavigateCreateTour = new RelayCommand(NavigateCreateTourExecute);
            NavigateFinishedTours = new RelayCommand(NavigateFinishedToursExecute);
            NavigateTourRequests = new RelayCommand(NavigateTourRequestsExecute);
        }

        private void NavigateBackExecute(object parameter)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void NavigateScheduledToursExecute(object parameter)
        {
            NavigationService.Navigate(new ScheduledToursPage(userId, NavigationService));
        }

        private void NavigateCreateTourExecute(object parameter)
        {
            NavigationService.Navigate(new CreateTourPage(null, userId, NavigationService));
        }

        private void NavigateFinishedToursExecute(object parameter)
        {
            NavigationService.Navigate(new FinishedToursPage(userId, NavigationService));
        }

        private void NavigateTourRequestsExecute(object parameter)
        {
            NavigationService.Navigate(new TourRequestsPage(userId, NavigationService));
        }
    }
}
