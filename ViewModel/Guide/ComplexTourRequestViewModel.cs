using System.Collections.ObjectModel;
using System.Windows.Controls;
using BookingApp.Command;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View.Guide;

namespace BookingApp.ViewModel.Guide
{
    public class ComplexTourRequestsViewModel : ViewModelBase
    {
        public ObservableCollection<ComplexTourRequest> ComplexTourRequests { get; set; }

        private readonly int userId;

        ComplexTourRequestService complexTourRequestService = new ComplexTourRequestService();

        public Frame NavigationService { get; set; }
        public RelayCommand NavigateTourRequests { get; set; }

        public ComplexTourRequestsViewModel(int userId, Frame navigationService)
        {
            this.userId = userId;
            NavigationService = navigationService;
            NavigateTourRequests = new RelayCommand(NavigateToTourRequestsExecute);
            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>(complexTourRequestService.GetAllByAcceptable(userId));
        }

        private void NavigateToTourRequestsExecute(object parameter)
        {
            NavigationService.Navigate(new TourRequestsPage(userId, NavigationService));
        }

        public void AcceptTourRequest(ComplexTourRequest complexTourRequest, TourRequest tourRequest)
        {
            int tourRequestIndex = complexTourRequest.TourRequests.IndexOf(tourRequest);
            complexTourRequest.TourRequests[tourRequestIndex].Status = TourRequestStatus.Accepted;
            complexTourRequest.TourRequests[tourRequestIndex].Id = userId;
            complexTourRequestService.Update(complexTourRequest);
            ComplexTourRequests.Remove(complexTourRequest);
            NavigationService.Navigate(new CreateTourPage(tourRequest, userId, NavigationService));
        }
    }
}
