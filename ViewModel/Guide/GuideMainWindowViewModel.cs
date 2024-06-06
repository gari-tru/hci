using System.Windows;
using System.Windows.Controls;
using BookingApp.Command;
using BookingApp.View.Guide;

namespace BookingApp.ViewModel.Guide
{
    class GuideMainWindowViewModel : ViewModelBase
    {
        private readonly int userId;
        public Frame NavigationService { get; set; }
        public RelayCommand NavigateBack { get; set; }
        public RelayCommand NavigateScheduledTours { get; set; }
        public RelayCommand NavigateCreateTour { get; set; }
        public RelayCommand NavigateFinishedTours { get; set; }
        public RelayCommand NavigateTourRequests { get; set; }
        public RelayCommand ToggleMenu { get; set; }
        public RelayCommand Exit { get; set; }

        private bool _isMenuOpen = false;
        public bool IsMenuOpen
        {
            get => _isMenuOpen;
            set
            {
                _isMenuOpen = value;
                OnPropertyChanged(nameof(IsMenuOpen));
            }
        }

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
            ToggleMenu = new RelayCommand(ToggleMenuExecute);
            Exit = new RelayCommand(ExitExecute);
        }

        private void ToggleMenuExecute(object parameter)
        {
            IsMenuOpen = !IsMenuOpen;
        }

        private void NavigateBackExecute(object parameter)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
                IsMenuOpen = false;
            }
        }
        private void NavigateScheduledToursExecute(object parameter)
        {
            NavigationService.Navigate(new ScheduledToursPage(userId, NavigationService));
            IsMenuOpen = false;
        }

        private void NavigateCreateTourExecute(object parameter)
        {
            NavigationService.Navigate(new CreateTourPage(null, userId, NavigationService));
            IsMenuOpen = false;
        }

        private void NavigateFinishedToursExecute(object parameter)
        {
            NavigationService.Navigate(new FinishedToursPage(userId, NavigationService));
            IsMenuOpen = false;
        }

        private void NavigateTourRequestsExecute(object parameter)
        {
            NavigationService.Navigate(new TourRequestsPage(userId, NavigationService));
            IsMenuOpen = false;
        }

        private void ExitExecute(object parameter)
        {
            Window window = Window.GetWindow(NavigationService);
            window?.Close();
        }
    }
}
