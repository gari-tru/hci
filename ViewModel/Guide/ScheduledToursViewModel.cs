using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using BookingApp.Command;
using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View.Guide;

namespace BookingApp.ViewModel.Guide
{
    public class ScheduledToursViewModel : ViewModelBase
    {
        public ObservableCollection<TourDto> TourDtos { get; set; }

        private readonly int userId;

        private TourDto _selectedTourDto;

        private TourService tourService = new TourService();
        private ScheduledTourService scheduledTourService = new ScheduledTourService();
        private VoucherService voucherService = new VoucherService();

        public Frame NavigationService { get; set; }
        public RelayCommand StartTour { get; set; }
        public RelayCommand CancelTour { get; set; }

        public ScheduledToursViewModel(int userId, Frame navigationService)
        {
            this.userId = userId;
            InitializeTourDtos();
            NavigationService = navigationService;
            StartTour = new RelayCommand(StartTourExecute);
            CancelTour = new RelayCommand(CancelTourExecute);
        }

        private void InitializeTourDtos()
        {
            TourDtos = new ObservableCollection<TourDto>();
            List<ScheduledTour> scheduledTours = scheduledTourService.GetAllByStatusAndGuideId(Status.Scheduled, userId);
            List<Tour> tours = tourService.GetAllByScheduledTours(scheduledTours);

            for (int i = 0; i < scheduledTours.Count && i < tours.Count; i++)
            {
                TourDtos.Add(new TourDto(tours[i], scheduledTours[i]));
            }
        }

        private void StartTourExecute(object parameter)
        {
            _selectedTourDto = (TourDto)parameter;
            _selectedTourDto.Tour.KeyPoints[0].IsMarked = true;
            _selectedTourDto.ScheduledTour.Status = Status.Live;

            TourDtos.Remove(_selectedTourDto);
            tourService.Update(_selectedTourDto.Tour);
            scheduledTourService.Update(_selectedTourDto.ScheduledTour);

            NavigationService.Navigate(new LiveTourTrackingPage(_selectedTourDto, NavigationService));
        }

        private void CancelTourExecute(object parameter)
        {
            _selectedTourDto = (TourDto)parameter;
            SendVouchers();
            TourDtos.Remove(_selectedTourDto);
            scheduledTourService.Delete(_selectedTourDto.ScheduledTour);
        }

        private void SendVouchers()
        {
            List<int> uniqueTouristIds = FilterUniqueTouristIds();

            foreach (int touristId in uniqueTouristIds)
            {
                voucherService.Save(new Voucher(touristId, DateTime.Now.AddYears(1)));
            }
        }

        private List<int> FilterUniqueTouristIds()
        {
            return _selectedTourDto.ScheduledTour.Tourists.Select(t => t.TouristId).Distinct().ToList();
        }
    }
}
