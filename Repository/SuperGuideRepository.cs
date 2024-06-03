using System.Collections.Generic;
using System.Linq;
using BookingApp.Model;
using BookingApp.Repository.Interface;
using BookingApp.Serializer;

namespace BookingApp.Repository
{
    public class SuperGuideRepository : ISuperGuideRepository
    {
        private const string FilePath = "../../../Resources/Data/superGuides.csv";

        private readonly Serializer<SuperGuide> _serializer;

        private List<SuperGuide> _superGuides;

        public SuperGuideRepository()
        {
            _serializer = new Serializer<SuperGuide>();
            _superGuides = _serializer.FromCSV(FilePath);
        }
        public SuperGuide GetById(int id)
        {
            _superGuides = _serializer.FromCSV(FilePath);
            return _superGuides.FirstOrDefault(sg => sg.GuideId == id);
        }

        public SuperGuide Save(SuperGuide superGuide)
        {
            superGuide.Id = NextId();
            _superGuides = _serializer.FromCSV(FilePath);
            _superGuides.Add(superGuide);
            _serializer.ToCSV(FilePath, _superGuides);
            return superGuide;
        }

        public int NextId()
        {
            _superGuides = _serializer.FromCSV(FilePath);
            return _superGuides.Any() ? _superGuides.Max(c => c.Id) + 1 : 1;
        }

        public void Delete(SuperGuide superGuide)
        {
            _superGuides = _serializer.FromCSV(FilePath);
            _superGuides.RemoveAll(st => st.Id == superGuide.Id);
            _serializer.ToCSV(FilePath, _superGuides);
        }
    }
}
