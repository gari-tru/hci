using System.Collections.Generic;
using System.Linq;
using BookingApp.Model;
using BookingApp.Repository.Interface;
using BookingApp.Serializer;

namespace BookingApp.Repository
{
    public class ComplexTourRepository : IComplexTourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/complexTourRequests.csv";

        private readonly Serializer<ComplexTourRequest> _serializer;

        private List<ComplexTourRequest> _complexTourRequests;

        public ComplexTourRepository()
        {
            _serializer = new Serializer<ComplexTourRequest>();
            _complexTourRequests = _serializer.FromCSV(FilePath);
        }

        public List<ComplexTourRequest> GetAllByAcceptable(int guideId)
        {
            _complexTourRequests = _serializer.FromCSV(FilePath);
            return _complexTourRequests.Where(ctr => ctr.Status == TourRequestStatus.Waiting)
                                       .Where(ctr => ctr.TourRequests.All(tr => tr.Id != guideId))
                                       .ToList();
        }

        public List<ComplexTourRequest> GetAllByTouristId(int touristId)
        {
            _complexTourRequests = _serializer.FromCSV(FilePath);
            return _complexTourRequests.Where(ctr => ctr.TouristId == touristId).ToList();
        }

        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest)
        {
            complexTourRequest.Id = NextId();
            _complexTourRequests = _serializer.FromCSV(FilePath);
            _complexTourRequests.Add(complexTourRequest);
            _serializer.ToCSV(FilePath, _complexTourRequests);
            return complexTourRequest;
        }

        public int NextId()
        {
            _complexTourRequests = _serializer.FromCSV(FilePath);
            return _complexTourRequests.Any() ? _complexTourRequests.Max(c => c.Id) + 1 : 1;
        }

        public void Update(ComplexTourRequest updatedComplexTourRequest)
        {
            _complexTourRequests = _serializer.FromCSV(FilePath);
            int index = _complexTourRequests.FindIndex(ctr => ctr.Id == updatedComplexTourRequest.Id);
            if (index != -1)
            {
                _complexTourRequests[index] = updatedComplexTourRequest;
                _serializer.ToCSV(FilePath, _complexTourRequests);
            }
        }
    }
}
