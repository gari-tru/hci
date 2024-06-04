using System.Collections.ObjectModel;
using System.Linq;
using BookingApp.Command;
using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;

namespace BookingApp.ViewModel.Guide
{
    public class TourReviewsViewModel : ViewModelBase
    {
        public ObservableCollection<TourDto> _tourDto { get; set; }

        public ObservableCollection<TourReview> TourReviews { get; set; }

        private int _selectedReviewIndex = -1;
        public int SelectedReviewIndex
        {
            get => _selectedReviewIndex;
            set
            {
                _selectedReviewIndex = value;
                OnPropertyChanged(nameof(_selectedReviewIndex));
            }
        }

        private TourReviewService _tourReviewService = new TourReviewService();

        public RelayCommand ReportReview { get; set; }

        public TourReviewsViewModel(TourDto tourDto)
        {
            _tourDto = new ObservableCollection<TourDto> { tourDto };
            TourReviews = new ObservableCollection<TourReview>(_tourReviewService.GetAllByScheduledTourId(tourDto.ScheduledTour.Id));
            TourReviews = new ObservableCollection<TourReview>(TourReviews.Where(tr => tr.IsValid != false).ToList());
            ReportReview = new RelayCommand(ReportReviewExecute);
        }

        private void ReportReviewExecute(object parameter)
        {
            TourReview tourReview = (TourReview)parameter;
            tourReview.IsValid = false;
            _tourReviewService.Update(tourReview);
            int index = TourReviews.IndexOf(tourReview);
            TourReviews.RemoveAt(index);
            //TourReviews.Insert(index, tourReview);
        }
    }
}
