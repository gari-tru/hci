using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Model;
using BookingApp.Service;

namespace BookingApp.ViewModel.Guide
{
    public class SuperGuideViewModel
    {
        private readonly int userId;
        private readonly SuperGuideService superGuideService = new SuperGuideService();
        private readonly ScheduledTourService scheduledTourService = new ScheduledTourService();
        private readonly TourService tourService = new TourService();
        private readonly TourReviewService tourReviewService = new TourReviewService();

        private readonly List<ScheduledTour> finishedTours;
        private readonly List<Tour> tours;

        public SuperGuideViewModel(int userId)
        {
            this.userId = userId;
            finishedTours = scheduledTourService.GetAllByStatusAndGuideId(Status.Finished, userId)
                                                .Where(ft => ft.Start.Date >= DateTime.Now.AddYears(-1).Date)
                                                .ToList();
            tours = tourService.GetAllByScheduledTours(finishedTours);
            UpdateSuperGuide();
        }

        public void UpdateSuperGuide()
        {
            var superGuide = superGuideService.GetById(userId);
            string language = CheckSuperGuideStatus(superGuide?.Language ?? string.Empty);

            if (superGuide == null && !string.IsNullOrEmpty(language))
            {
                superGuideService.Save(new SuperGuide(userId, DateTime.Today, language));
            }
            else if (superGuide != null && superGuide.Start.AddYears(1).Date == DateTime.Today)
            {
                if (string.IsNullOrEmpty(CheckSuperGuideStatus(superGuide.Language)))
                {
                    superGuideService.Delete(superGuide);
                }
            }
        }

        public string CheckSuperGuideStatus(string superGuideLanguage)
        {
            if (!string.IsNullOrEmpty(superGuideLanguage))
            {
                return CalculateAverageRating(superGuideLanguage) >= 4 && FilterLanguages(superGuideLanguage).Count == 1
                    ? superGuideLanguage
                    : string.Empty;
            }
            else
            {
                return FilterLanguages(string.Empty).FirstOrDefault(language => CalculateAverageRating(language) >= 4) ?? string.Empty;
            }
        }

        public List<string> FilterLanguages(string superGuideLanguage)
        {
            if (!string.IsNullOrEmpty(superGuideLanguage))
            {
                int count = tours.Count(t => t.Language == superGuideLanguage);

                return count >= 20 ? new List<string> { superGuideLanguage } : new List<string>();
            }
            else
            {
                var languageCounts = tours.GroupBy(t => t.Language)
                                          .ToDictionary(g => g.Key, g => g.Count())
                                          .Where(kvp => kvp.Value >= 20)
                                          .Select(kvp => kvp.Key)
                                          .ToList();

                return languageCounts;
            }
        }

        public double CalculateAverageRating(string language)
        {
            var reviews = finishedTours
                .Where(ft => tours.Any(t => t.Language == language && t.Id == ft.TourId))
                .SelectMany(ft => tourReviewService.GetAllByScheduledTourId(ft.Id))
                .ToList();

            return reviews.Count == 0 ? 0 : reviews.Average(r => r.AverageRating);
        }
    }
}
