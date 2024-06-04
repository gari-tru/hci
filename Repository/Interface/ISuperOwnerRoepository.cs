using System.Collections.Generic;
using BookingApp.Model;

namespace BookingApp.Repository.Interface
{
    public interface ISuperOwnerRepository
    {
        List<SuperOwner> GetAll();
        SuperOwner Save(SuperOwner owner);
        void Delete(SuperOwner owner);
        SuperOwner Update(SuperOwner owner);
        SuperOwner GetByOwnerId(int ownerId);
    }
}
