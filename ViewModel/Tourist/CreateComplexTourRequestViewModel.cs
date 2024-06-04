using System;
using System.Collections.Generic;
using System.Windows;
using BookingApp.Model;
using BookingApp.Service;

namespace BookingApp.ViewModel.Tourist
{
    public class CreateComplexTourRequestViewModel : ViewModelBase
    {
        private readonly User user;
        private readonly ComplexTourRequestService _complexTourRequestService = new ComplexTourRequestService();
        private int _participantsCount;
        private int _requestsCount;

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(nameof(_location));
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
                OnPropertyChanged(nameof(_language));
            }
        }

        private int _touristNumber;
        public int TouristNumber
        {
            get => _touristNumber;
            set
            {
                _touristNumber = value;
                OnPropertyChanged(nameof(_touristNumber));
            }
        }

        private int _requestNumber;
        public int RequestNumber
        {
            get => _requestNumber;
            set
            {
                _requestNumber = value;
                OnPropertyChanged(nameof(_requestNumber));
            }
        }

        private List<TourRequest> _complexTourRequest;
        public List<TourRequest> ComplexTourRequest
        {
            get => _complexTourRequest;
            set
            {
                _complexTourRequest = value;
                OnPropertyChanged(nameof(_complexTourRequest));
            }
        }

        private List<Model.Tourist> _participants;
        public List<Model.Tourist> Participants
        {
            get => _participants;
            set
            {
                _participants = value;
                OnPropertyChanged(nameof(_participants));
            }
        }

        private string _participantName;
        public string ParticipantName
        {
            get => _participantName;
            set
            {
                _participantName = value;
                OnPropertyChanged(nameof(ParticipantName));
            }
        }

        private string _participantSurname;
        public string ParticipantSurname
        {
            get => _participantSurname;
            set
            {
                _participantSurname = value;
                OnPropertyChanged(nameof(ParticipantSurname));
            }
        }

        private int _participantAge;
        public int ParticipantAge
        {
            get => _participantAge;
            set
            {
                _participantAge = value;
                OnPropertyChanged(nameof(ParticipantAge));
            }
        }

        public DateTime Now { get; } = DateTime.Now;

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
        private string _end;
        public string End
        {
            get => _end;
            set
            {
                _end = value;
                OnPropertyChanged(nameof(_end));
            }
        }

        private LanguageService languageService = new LanguageService();
        private LocationService locationService = new LocationService();

        public List<string> Languages { get; }
        public List<string> Locations { get; }

        private bool _isParticipantInputEnabled = true;
        public bool IsParticipantInputEnabled
        {
            get => _isParticipantInputEnabled;
            set
            {
                _isParticipantInputEnabled = value;
                OnPropertyChanged(nameof(IsParticipantInputEnabled));
            }
        }


        public CreateComplexTourRequestViewModel(User user)
        {
            this.user = user;
            Locations = locationService.GetAll();
            Languages = languageService.GetAll();
            Participants = new List<Model.Tourist>();
            ComplexTourRequest = new List<TourRequest>();
            _participantsCount = 0;
            _requestsCount = 0;
        }

        public void CreateComplexTourRequest()
        {
            if (_requestsCount >= RequestNumber)
            {
                ComplexTourRequest ComplexRequest = new ComplexTourRequest(user.Id, ComplexTourRequest, TourRequestStatus.Waiting);
                _complexTourRequestService.Save(ComplexRequest);
                TouristNumber = 0;
                RequestNumber = 0;
                Participants.Clear();
                _participantsCount = 0;
                _requestsCount = 0;
                IsParticipantInputEnabled = true;
                MessageBox.Show("Complex tour request created successfully!");
            }
            else
                MessageBox.Show("You have not reached the maximum number of requests.");
        }

        public void AddTourRequest()
        {

            if (!string.IsNullOrWhiteSpace(Location) && !string.IsNullOrWhiteSpace(Description) && !string.IsNullOrWhiteSpace(Language) && TouristNumber > 0 && Participants.Count == TouristNumber && !string.IsNullOrWhiteSpace(Start) && !string.IsNullOrWhiteSpace(End) && _requestsCount < RequestNumber)
            {
                DateTime startDate = Convert.ToDateTime(Start);
                DateTime endDate = Convert.ToDateTime(End);

                if (endDate < startDate)
                {
                    MessageBox.Show("End date cannot be before start date.");
                    return;
                }

                TourRequest newTourRequest = new TourRequest(Location, Description, Language, TouristNumber, Participants, Convert.ToDateTime(Start), Convert.ToDateTime(End), TourRequestStatus.Waiting);
                ComplexTourRequest.Add(newTourRequest);
                _requestsCount++;
                ClearRequestFields();

                MessageBox.Show("Tour request created successfully!");
            }
            else
            {
                MessageBox.Show("Please fill all fields correctly.");
            }
        }

        public void AddParticipant()
        {

            if (_participantsCount < TouristNumber && !string.IsNullOrWhiteSpace(ParticipantName) && !string.IsNullOrWhiteSpace(ParticipantSurname) && ParticipantAge > 0)
            {
                Participants.Add(new Model.Tourist(user.Id, ParticipantName, ParticipantSurname, ParticipantAge, "", false));
                ClearParticipantFields();
                OnPropertyChanged(nameof(Participants));
                _participantsCount++;

                if (_participantsCount >= TouristNumber)
                {
                    IsParticipantInputEnabled = false;
                }

                MessageBox.Show("Participant added successfully!");
            }
            else if (_participantsCount >= TouristNumber)
            {
                MessageBox.Show("You have reached the maximum number of participants.");
            }
            else if (string.IsNullOrWhiteSpace(ParticipantName) || string.IsNullOrWhiteSpace(ParticipantSurname) || ParticipantAge <= 0)
            {
                MessageBox.Show("Please fill all fields correctly.");
            }

        }

        private void ClearRequestFields()
        {
            Location = string.Empty;
            OnPropertyChanged(nameof(Location));
            Description = string.Empty;
            OnPropertyChanged(nameof(Description));
            Language = string.Empty;
            OnPropertyChanged(nameof(Language));
            Start = string.Empty;
            OnPropertyChanged(nameof(Start));
            End = string.Empty;
            OnPropertyChanged(nameof(End));
        }

        private void ClearParticipantFields()
        {
            ParticipantName = "";
            ParticipantSurname = "";
            ParticipantAge = 0;
        }
    }
}
