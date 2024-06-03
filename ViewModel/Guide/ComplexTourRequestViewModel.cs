using System.Collections.ObjectModel;
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

        public ComplexTourRequestsViewModel(int userId)
        {
            this.userId = userId;
            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>(complexTourRequestService.GetAllByAcceptable(userId));
        }

        public void AcceptTourRequest(ComplexTourRequest complexTourRequest, TourRequest tourRequest)
        {
            int tourRequestIndex = complexTourRequest.TourRequests.IndexOf(tourRequest);
            complexTourRequest.TourRequests[tourRequestIndex].Status = TourRequestStatus.Accepted;
            complexTourRequest.TourRequests[tourRequestIndex].Id = userId;
            complexTourRequestService.Update(complexTourRequest);
            ComplexTourRequests.Remove(complexTourRequest);
            CreateTourView createTourView = new CreateTourView(tourRequest, userId);
            createTourView.ShowDialog();
        }
    }
}
