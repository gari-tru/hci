﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using BookingApp.Model;
using BookingApp.Service;
using Microsoft.Win32;

namespace BookingApp.ViewModel.Guide
{
    public class CreateTourViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(_name));
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(_description));
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
            }
        }

        private int _maxTourists = 1;
        public int MaxTourists
        {
            get => _maxTourists;
            set
            {
                _maxTourists = value;
                OnPropertyChanged(nameof(_maxTourists));
            }
        }

        private string _keyPointNames = "";
        public string KeyPointNames
        {
            get => _keyPointNames;
            set
            {
                _keyPointNames = value;
                OnPropertyChanged(nameof(_keyPointNames));
            }
        }

        private string _start;
        public string Start
        {
            get => _start;
            set
            {
                _start = value;
                OnPropertyChanged(nameof(_start));
            }
        }

        private int _duration = 1;
        public int Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(_duration));
            }
        }

        public ObservableCollection<string> Images { get; set; } = new ObservableCollection<string>();

        public bool IsTourValid()
        {
            NameValidator = string.IsNullOrWhiteSpace(Name) ? "Visible" : "Collapsed";
            KeyPointValidatorVisibility = isKeyPointNamesValid ? "Collapsed" : "Visible";
            LocationValidator = !Locations.Contains(Location) ? "Visible" : "Collapsed";
            DescriptionValidator = string.IsNullOrWhiteSpace(Description) ? "Visible" : "Collapsed";
            LanguageValidator = !Languages.Contains(Language) ? "Visible" : "Collapsed";
            ImagesValidator = Images.Count > 0 ? "Collapsed" : "Visible";
            TimeValidator = IsTimeValid() ? "Collapsed" : "Visible";

            return !string.IsNullOrWhiteSpace(Name) &&
            Locations.Contains(Location) &&
            !string.IsNullOrWhiteSpace(Description) &&
            Languages.Contains(Language) &&
            MaxTourists > 0 &&
            isKeyPointNamesValid &&
            Duration > 0 &&
            Images.Count > 0 &&
            IsTimeValid();
        }

        public bool isKeyPointNamesValid => ConvertKeyPointNames(KeyPointNames).Count >= 2;

        private string _tourRequestValidator = "Collapsed";
        public string TourRequestValidator
        {
            get => _tourRequestValidator;
            set
            {
                _tourRequestValidator = value;
                OnPropertyChanged(nameof(TourRequestValidator));
            }
        }

        private string _timeValidator = "Collapsed";
        public string TimeValidator
        {
            get => _timeValidator;
            set
            {
                _timeValidator = value;
                OnPropertyChanged(nameof(TimeValidator));
            }
        }

        private string _imagesValidator = "Collapsed";
        public string ImagesValidator
        {
            get => _imagesValidator;
            set
            {
                _imagesValidator = value;
                OnPropertyChanged(nameof(ImagesValidator));
            }
        }

        private string _languageValidator = "Collapsed";
        public string LanguageValidator
        {
            get => _languageValidator;
            set
            {
                _languageValidator = value;
                OnPropertyChanged(nameof(LanguageValidator));
            }
        }

        private string _descriptionValidator = "Collapsed";
        public string DescriptionValidator
        {
            get => _descriptionValidator;
            set
            {
                _descriptionValidator = value;
                OnPropertyChanged(nameof(DescriptionValidator));
            }
        }

        private string _locationValidator = "Collapsed";
        public string LocationValidator
        {
            get => _locationValidator;
            set
            {
                _locationValidator = value;
                OnPropertyChanged(nameof(LocationValidator));
            }
        }

        private string _nameValidator = "Collapsed";
        public string NameValidator
        {
            get => _nameValidator;
            set
            {
                _nameValidator = value;
                OnPropertyChanged(nameof(NameValidator));
            }
        }

        private string _keyPointValidatorVisibility = "Collapsed";
        public string KeyPointValidatorVisibility
        {
            get => _keyPointValidatorVisibility;
            set
            {
                _keyPointValidatorVisibility = value;
                OnPropertyChanged(nameof(KeyPointValidatorVisibility));
            }
        }


        private bool IsTimeValid()
        {
            List<ScheduledTour> scheduledTours = scheduledTourService.GetAllByStatusAndGuideId(Status.Scheduled, userId);
            List<Tour> tours = tourService.GetAllByScheduledTours(scheduledTours);

            for (int i = 0; i < scheduledTours.Count && i < tours.Count; i++)
            {
                DateTime scheduledTourStart = scheduledTours[i].Start;
                DateTime scheduledTourEnd = scheduledTours[i].Start.AddHours(tours[i].Duration);

                if (IsTimeOverlapping(scheduledTourStart, scheduledTourEnd))
                {
                    return false;
                }
            }

            if (TourRequest != null)
            {
                if (Convert.ToDateTime(Start) < TourRequest.Start || Convert.ToDateTime(Start).AddHours(Duration) > TourRequest.End)
                {
                    TourRequestValidator = "Visible";
                    return false;
                }

                TourRequestValidator = "Collapsed";
            }

            return true;
        }

        private bool IsTimeOverlapping(DateTime scheduledTourStart, DateTime scheduledTourEnd)
        {
            return Convert.ToDateTime(Start) < scheduledTourEnd && Convert.ToDateTime(Start).AddHours(Duration) > scheduledTourStart;
        }

        private List<string> ConvertKeyPointNames(string keyPointNames)
        {
            return keyPointNames.Split(',').Select(name => name.Trim()).Where(name => !string.IsNullOrWhiteSpace(name)).ToList();
        }

        private readonly int userId;

        private TourRequest _tourRequest;
        public TourRequest TourRequest
        {
            get => _tourRequest;
            set
            {
                _tourRequest = value;
                OnPropertyChanged(nameof(TourRequest));
            }
        }

        private TourService tourService = new TourService();
        private ScheduledTourService scheduledTourService = new ScheduledTourService();
        private LanguageService languageService = new LanguageService();
        private LocationService locationService = new LocationService();

        public List<string> Languages { get; }
        public List<string> Locations { get; }

        private string _destinationPath = "../../../Resources/Images/";
        private string _relativePath = "../Resources/Images/";
        private const string _notificationsPath = "../../../Resources/Data/tourRequestNotifications.csv";

        public DateTime Now { get; } = DateTime.Now;

        private string _mostWantedLocation;
        public string MostWantedLocation
        {
            get => _mostWantedLocation;
            set
            {
                _mostWantedLocation = value;
                OnPropertyChanged(nameof(_mostWantedLocation));
            }
        }

        private string _mostWantedLanguage;
        public string MostWantedLanguage
        {
            get => _mostWantedLanguage;
            set
            {
                _mostWantedLanguage = value;
                OnPropertyChanged(nameof(_mostWantedLanguage));
            }
        }

        TourRequestService tourRequestService = new TourRequestService();

        public CreateTourViewModel(TourRequest tourRequest, int userId)
        {
            this.userId = userId;
            Locations = locationService.GetAll();
            Languages = languageService.GetAll();
            TourRequest = tourRequest;

            if (tourRequest != null)
            {
                Location = tourRequest.Location;
                Description = tourRequest.Description;
                Language = tourRequest.Language;
                MaxTourists = tourRequest.TouristNumber;
            }

            (MostWantedLocation, MostWantedLanguage) = tourRequestService.GetMostWantedLocationAndLanguage();
        }

        public void AcceptSystemTour(string label)
        {
            TourRequest tourRequest = new TourRequest();

            if (label == "location")
            {
                Location = MostWantedLocation;
            }
            else if (label == "language")
            {
                Language = MostWantedLanguage;
            }
        }

        public bool CreateTour()
        {
            if (IsTourValid())
            {
                (Tour tour, ScheduledTour scheduledTour) = InitializeTours();

                tourService.Save(tour);
                scheduledTour.TourId = tour.Id;
                scheduledTourService.Save(scheduledTour);

                if (TourRequest != null)
                {
                    string notification = $"{TourRequest.Id}|{Start}";
                    using (StreamWriter writer = File.AppendText(_notificationsPath))
                    {
                        writer.WriteLine(notification);
                    }
                }

                return true;
            }

            return false;
        }

        private (Tour, ScheduledTour) InitializeTours()
        {
            Tour tour = new Tour
            {
                Name = Name,
                Location = Location,
                Description = Description,
                Language = Language,
                MaxTourists = MaxTourists,
                KeyPoints = ConvertKeyPointNames(KeyPointNames).Select(name =>
                {
                    return new KeyPoint(name, false);
                }).ToList(),
                Duration = Duration,
                Images = Images.ToList(),
            };

            ScheduledTour scheduledTour = new ScheduledTour
            {
                GuideId = userId,
                Start = Convert.ToDateTime(Start),
                Tourists = new List<Model.Tourist>(),
                FreeSpots = MaxTourists,
            };

            if (TourRequest != null)
            {
                scheduledTour.Tourists = TourRequest.Tourists;
                scheduledTour.FreeSpots = 0;
            }

            return (tour, scheduledTour);
        }

        public void UploadImages()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string fileName in openFileDialog.FileNames)
                {
                    string destinationPath = Path.Combine(_destinationPath, Path.GetFileName(fileName));
                    File.Copy(fileName, destinationPath, overwrite: true);
                    Images.Add(destinationPath);
                }
            }
        }

    }
}
