using BookingApp.Model;

namespace BookingApp.Repository.Interface
{
    public interface IUserRepository
    {
        User FindById(int id);
        User GetByUsername(string username);
    }
}
