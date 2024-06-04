using System.Collections.Generic;
using BookingApp.Model;

namespace BookingApp.Repository.Interface
{
    internal interface IRenovationRepository
    {

        List<Renovation> GetActiveByAccommodationId(int accommodationId);
        List<Renovation> GetAll();
        Renovation GetById(int id);
        Renovation Save(Renovation renovation);
        int NextId();
        void LoadRenovations();
        Renovation GetByAccommodation(int accommodationId);
        Renovation Update(Renovation renovation);
    }
}
