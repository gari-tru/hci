using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View.Guide;
using LiveCharts;
using LiveCharts.Wpf;

namespace BookingApp.ViewModel.Guide
{
    public class TourRequestStatisticsViewModel : ViewModelBase
    {
        public ObservableCollection<KeyValuePair<string, int>> Statistics { get; set; } = new ObservableCollection<KeyValuePair<string, int>>();

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(nameof(_location));
                FilterStatistics();
            }
        }

        private string _language;
        public string Language
        {
            get => _language;
            set
            {
                _language = value;
                OnPropertyChanged(nameof(_language));
                FilterStatistics();
            }
        }

        public List<string> Years { get; set; }

        private string _selectedYear = "Na nivou godina";
        public string SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
                FilterStatistics();
            }
        }

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

        private string _label = "Uneti lokaciju/jezik";
        public string Label
        {
            get => _label;
            set
            {
                _label = value;
                OnPropertyChanged(nameof(Label));
            }
        }

        private string _plotVisibility = "Hidden";
        public string PlotVisibility
        {
            get => _plotVisibility;
            set
            {
                _plotVisibility = value;
                OnPropertyChanged(nameof(PlotVisibility));
            }
        }

        private readonly int userId;

        private TourRequestService tourRequestService = new TourRequestService();
        private LanguageService languageService = new LanguageService();
        private LocationService locationService = new LocationService();

        private List<TourRequest> tourRequests;
        public List<string> Languages { get; }
        public List<string> Locations { get; }

        public Frame NavigationService { get; set; }

        public SeriesCollection Values { get; set; }
        private ObservableCollection<string> _labels;
        public ObservableCollection<string> Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }

        public TourRequestStatisticsViewModel(int userId, Frame navigationService)
        {
            this.userId = userId;
            NavigationService = navigationService;
            tourRequests = tourRequestService.GetAll();
            Languages = languageService.GetAll();
            Locations = locationService.GetAll();
            FilterUniqueYears();
            Years.Insert(0, "Na nivou godina");
            (MostWantedLocation, MostWantedLanguage) = tourRequestService.GetMostWantedLocationAndLanguage();
        }

        public void FilterUniqueYears()
        {
            Years = tourRequests.Select(tr => tr.Start.Year.ToString())
                                .Distinct()
                                .ToList();
        }

        public void FilterStatistics()
        {
            if (Locations.Contains(Location))
            {
                CalculateStatistics(Location);
            }
            else if (Languages.Contains(Language))
            {
                CalculateStatistics(Language);
            }
        }

        public void CalculateStatistics(string label)
        {
            Statistics.Clear();
            foreach (var statistic in tourRequestService.GetStatisticsByLabelAndYear(label, SelectedYear))
            {
                Statistics.Add(statistic);
            }

            Values = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Broj zahteva za turu:",
                    Values = new ChartValues<int>(Statistics.Select(s => s.Value)),
                    DataLabels = true
                }
            };
            OnPropertyChanged(nameof(Values));
            Labels = new ObservableCollection<string>(Statistics.Select(s => s.Key));

            Label = label;
            PlotVisibility = "Visible";
        }

        public void CreateTour(string label)
        {
            TourRequest tourRequest = new TourRequest();

            if (label == "location")
            {
                tourRequest.Location = MostWantedLocation;
            }
            else if (label == "language")
            {
                tourRequest.Language = MostWantedLanguage;
            }

            CreateTourView createTourView = new CreateTourView(tourRequest, userId);
            createTourView.ShowDialog();
        }
    }
}
