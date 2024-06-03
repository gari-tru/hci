using System.Collections.Generic;
using BookingApp.Model;

namespace BookingApp.Repository.Interface
{
    public interface IComplexTourRequestRepository
    {
        List<ComplexTourRequest> GetAllByAcceptable(int guideId);
        List<ComplexTourRequest> GetAllByTouristId(int touristId);
        ComplexTourRequest Save(ComplexTourRequest complexTourRequest);
        void Update(ComplexTourRequest updatedComplexTourRequest);
    }
}
