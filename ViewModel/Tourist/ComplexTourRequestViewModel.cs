using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Model;
using BookingApp.Service;

namespace BookingApp.ViewModel.Tourist
{
    public class ComplexTourRequestViewModel : ViewModelBase
    {
        private readonly User user;

        private readonly ComplexTourRequestService _complexTourRequestService = new ComplexTourRequestService();

        private ComplexTourRequest complexTourRequest;

        private List<ComplexTourRequest> _complexTourRequests;
        public List<ComplexTourRequest> ComplexTourRequests
        {
            get => _complexTourRequests;
            set
            {
                _complexTourRequests = value;
                OnPropertyChanged(nameof(_complexTourRequests));
            }
        }

        public ComplexTourRequestViewModel(User user)
        {
            this.user = user;
            ComplexTourRequests = new List<ComplexTourRequest>();
            LoadComplexTourRequests(user);

        }

        private void LoadComplexTourRequests(User user)
        {
            List<ComplexTourRequest> complexTourRequests = _complexTourRequestService.GetAllByTouristId(user.Id);

            foreach (var complexTourRequest in complexTourRequests)
            {
                DateTime firstStartDate = GetFirstStartDate(complexTourRequest.TourRequests);
                DateTime timeLimit = firstStartDate.AddDays(-2);

                if (DateTime.Now >= timeLimit && complexTourRequest.Status == TourRequestStatus.Waiting)
                {
                    complexTourRequest.Status = TourRequestStatus.Invalid;
                    _complexTourRequestService.Update(complexTourRequest);
                }

                bool allPartsAccepted = complexTourRequest.TourRequests.All(tr => tr.Status == TourRequestStatus.Accepted);

                if (allPartsAccepted)
                {
                    complexTourRequest.Status = TourRequestStatus.Accepted;
                    _complexTourRequestService.Update(complexTourRequest);
                }
            }

            ComplexTourRequests = complexTourRequests;
        }

        private DateTime GetFirstStartDate(List<TourRequest> tourRequests)
        {
            return tourRequests.Min(tr => tr.Start);
        }

    }
}
