using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
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

        public GuideMainWindowViewModel(int userId, Frame navigationService)
        {
            this.userId = userId;
            NavigationService = navigationService;
            NavigationService.Navigate(new ScheduledToursPage(userId, navigationService));
            NavigateBack = new RelayCommand(NavigateBackExecute);
            NavigateScheduledTours = new RelayCommand(NavigateScheduledToursExecute);
            NavigateCreateTour = new RelayCommand(NavigateCreateTourExecute);
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
            NavigationService.Navigate(new CreateTourPage());
        }
    }
}
