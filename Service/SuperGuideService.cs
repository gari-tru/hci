using BookingApp.Model;
using BookingApp.Repository.Interface;

namespace BookingApp.Service
{
    public class SuperGuideService
    {
        private readonly ISuperGuideRepository _superGuideRepository;

        public SuperGuideService()
        {
            _superGuideRepository = Injector.CreateInstance<ISuperGuideRepository>();
        }

        public SuperGuide GetById(int id)
        {
            return _superGuideRepository.GetById(id);
        }

        public SuperGuide Save(SuperGuide superGuide)
        {
            return _superGuideRepository.Save(superGuide);
        }

        public void Delete(SuperGuide updatedSuperGuide)
        {
            _superGuideRepository.Delete(updatedSuperGuide);
        }
    }
}
