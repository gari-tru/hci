﻿using System.Collections.Generic;
using BookingApp.Model;

namespace BookingApp.Repository.Interface
{
    internal interface IAccommodationRepository
    {
        List<Accommodation> FindByType(AccommodationType type);
        List<Accommodation> FindByMaxGuests(int maxGuests);
        List<Accommodation> FindByMinReservationDays(int minReservationDays);
        List<Accommodation> FindByName(string name);
        List<Accommodation> FindByCity(string city);
        List<Accommodation> FindByCountry(string country);
        List<Accommodation> GetAll();
        List<Accommodation> GetAllByOwner(int ownerId);
        bool CheckIfExist(string name, (string City, string Country) location, AccommodationType type);
        Accommodation Save(Accommodation accommodation);
        Accommodation FindById(int id);
        int NextId();

    }
}