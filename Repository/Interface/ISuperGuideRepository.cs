using BookingApp.Model;

namespace BookingApp.Repository.Interface
{
    public interface ISuperGuideRepository
    {
        SuperGuide GetById(int id);
        SuperGuide Save(SuperGuide superGuide);
        void Delete(SuperGuide superGuide);
    }
}
