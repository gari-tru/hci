using BookingApp.Model;
using System.Collections.Generic;
using BookingApp.Repository.Interface;

namespace BookingApp.Service
{
    public class ComplexTourRequestService
    {
        private readonly IComplexTourRequestRepository _complexTourRequestRepository;

        public ComplexTourRequestService()
        {
            _complexTourRequestRepository = Injector.CreateInstance<IComplexTourRequestRepository>();
        }

        public List<ComplexTourRequest> GetAllByAcceptable(int guideId)
        {
            return _complexTourRequestRepository.GetAllByAcceptable(guideId);
        }

        public List<ComplexTourRequest> GetAllByTouristId(int touristId)
        {
            return _complexTourRequestRepository.GetAllByTouristId(touristId);
        }

        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest)
        {
            return _complexTourRequestRepository.Save(complexTourRequest);
        }

        public void Update(ComplexTourRequest updatedComplexTourRequest)
        {
            _complexTourRequestRepository.Update(updatedComplexTourRequest);
        }
    }
}
