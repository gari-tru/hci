﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using BookingApp.Command;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View.Guide;

namespace BookingApp.ViewModel.Guide
{
    public class TourRequestsViewModel : ViewModelBase
    {
        public ObservableCollection<TourRequest> TourRequests { get; set; }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
                FilterTourRequests();
            }
        }

        private string _language;
        public string Language
        {
            get => _language;
            set
            {
                _language = value;
                OnPropertyChanged(nameof(Language));
                FilterTourRequests();
            }
        }

        private int _touristNumber;
        public int TouristNumber
        {
            get => _touristNumber;
            set
            {
                _touristNumber = value;
                OnPropertyChanged(nameof(TouristNumber));
                FilterTourRequests();
            }
        }

        private string _start;
        public string Start
        {
            get => _start;
            set
            {
                _start = value;
                OnPropertyChanged(nameof(Start));
                FilterTourRequests();
            }
        }

        private string _end;
        public string End
        {
            get => _end;
            set
            {
                _end = value;
                OnPropertyChanged(nameof(End));
                FilterTourRequests();
            }
        }

        private readonly int userId;

        private TourRequestService tourRequestService = new TourRequestService();
        private LanguageService languageService = new LanguageService();
        private LocationService locationService = new LocationService();

        public List<string> Languages { get; }
        public List<string> Locations { get; }

        public DateTime Now { get; } = DateTime.Now;

        public Frame NavigationService { get; set; }
        public RelayCommand NavigateComplexTourRequests { get; set; }
        public RelayCommand ClearSearch { get; set; }

        public TourRequestsViewModel(int userId, Frame navigationService)
        {
            TourRequests = new ObservableCollection<TourRequest>(tourRequestService.GetAllByWaiting());
            Locations = locationService.GetAll();
            Languages = languageService.GetAll();
            this.userId = userId;
            NavigationService = navigationService;
            NavigateComplexTourRequests = new RelayCommand(NavigateComplexTourRequestsExecute);
            ClearSearch = new RelayCommand(ClearSearchExecute);
        }

        private void ClearSearchExecute(object parameter)
        {
            Location = "";
            Language = "";
            TouristNumber = 0;
            Start = "";
            End = "";
        }

        public void NavigateComplexTourRequestsExecute(object parameter)
        {
            NavigationService.Navigate(new ComplexTourRequestsPage(userId, NavigationService));
        }

        public void FilterTourRequests()
        {
            TourRequests.Clear();

            foreach (TourRequest tourRequest in tourRequestService.GetAllByWaiting())
            {
                bool matchLocation = string.IsNullOrEmpty(Location) || tourRequest.Location.ToLower().Contains(Location.ToLower());
                bool matchLanguage = string.IsNullOrEmpty(Language) || tourRequest.Language.ToLower().Contains(Language.ToLower());
                bool matchTouristNumber = TouristNumber == 0 || tourRequest.TouristNumber >= TouristNumber;
                bool matchStartDate = string.IsNullOrEmpty(Start) || Convert.ToDateTime(tourRequest.Start) >= Convert.ToDateTime(Start);
                bool matchEndDate = string.IsNullOrEmpty(End) || Convert.ToDateTime(tourRequest.End) <= Convert.ToDateTime(End);

                if (matchLocation && matchLanguage && matchTouristNumber && matchStartDate && matchEndDate)
                {
                    TourRequests.Add(tourRequest);
                }
            }
        }

        public void HandleTourRequest(TourRequest tourRequest, TourRequestStatus status)
        {
            tourRequest.Status = status;
            tourRequestService.Update(tourRequest);
            TourRequests.Remove(tourRequest);

            if (status == TourRequestStatus.Accepted)
            {
                NavigationService.Navigate(new CreateTourPage(tourRequest, userId, NavigationService));
            }
        }
    }
}
