using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using BookingApp.Command;
using BookingApp.Dto;
using BookingApp.Model;

namespace BookingApp.ViewModel.Guide
{
    public class FinishedToursViewModel : ViewModelBase
    {
        public ObservableCollection<TourDto> TourDtos { get; set; }

        public ObservableCollection<string> Years { get; set; }

        private string _selectedYear = "Za sva vremena";
        public string SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged(nameof(_selectedYear));
                MostVisitedTourExecute(null);
            }
        }

        public ObservableCollection<TourDto> MostVisitedTourDto { get; set; } = new ObservableCollection<TourDto>();

        private readonly int userId;

        private TourService tourService = new TourService();
        private ScheduledTourService scheduledTourService = new ScheduledTourService();

        public Frame NavigationService { get; set; }
        public RelayCommand MostVisitedTour { get; set; }

        public FinishedToursViewModel(int userId, Frame navigationService)
        {
            this.userId = userId;
            NavigationService = navigationService;
            MostVisitedTour = new RelayCommand(MostVisitedTourExecute);
            MostVisitedTourExecute(null);
            InitializeTourDtos();
            FilterUniqueYears();
            Years.Insert(0, "Za sva vremena");
        }

        private void InitializeTourDtos()
        {
            TourDtos = new ObservableCollection<TourDto>();
            List<ScheduledTour> scheduledTours = scheduledTourService.GetAllByStatusAndGuideId(Status.Finished, userId);
            List<Tour> tours = tourService.GetAllByScheduledTours(scheduledTours);

            for (int i = 0; i < scheduledTours.Count && i < tours.Count; i++)
            {
                TourDtos.Add(new TourDto(tours[i], scheduledTours[i]));
            }
        }

        public void FilterUniqueYears()
        {
            Years = new ObservableCollection<string>(
               TourDtos
                   .Select(tourDto => tourDto.ScheduledTour.Start.Year.ToString())
                   .Distinct()
                   .ToList()
           );
        }

        private void MostVisitedTourExecute(object parameter)
        {
            ScheduledTour scheduledTour = scheduledTourService.GetMostVisitedByYear(userId, SelectedYear);
            if (scheduledTour != null)
            {
                Tour tour = tourService.GetById(scheduledTour.TourId);
                MostVisitedTourDto.Clear();
                MostVisitedTourDto.Add(new TourDto(tour, scheduledTour));
            }
        }
    }
}
