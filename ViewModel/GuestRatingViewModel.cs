using System;
using System.Windows.Input;
using BookingApp.Model;
using BookingApp.Service;

namespace BookingApp.ViewModel
{
    public class GuestRatingViewModel : ViewModelBase
    {
        private readonly GuestRatingService _guestRatingService;
        private readonly ReservationService _reservationService;
        private GuestRating _guestRating;

        public int CleanlinessRating
        {
            get => _guestRating.CleanlinessRating;
            set
            {
                _guestRating.CleanlinessRating = value + 1;
                OnPropertyChanged(nameof(CleanlinessRating));
            }
        }

        public int RuleAdherenceRating
        {
            get => _guestRating.RuleAdherenceRating;
            set
            {
                _guestRating.RuleAdherenceRating = value + 1;
                OnPropertyChanged(nameof(RuleAdherenceRating));
            }
        }

        public string AdditionalComment
        {
            get => _guestRating.AdditionalComment;
            set
            {
                _guestRating.AdditionalComment = value;
                OnPropertyChanged(nameof(AdditionalComment));
            }
        }

        public ICommand SubmitRatingCommand { get; }

        public GuestRatingViewModel(int reservationId, int ownerId)
        {
            _guestRatingService = new GuestRatingService();
            _reservationService = new ReservationService();
            Reservation reservation = _reservationService.GetReservationById(reservationId);
            _guestRating = new GuestRating(reservationId, ownerId, reservation.Guest!.Id, 1, 1, "", DateTime.Now, true);
            SubmitRatingCommand = null!;
        }

        public void SubmitRating(object parameter)
        {
            _guestRatingService.Save(_guestRating);
        }
    }
}
