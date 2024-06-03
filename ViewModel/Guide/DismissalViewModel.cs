using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Model;
using BookingApp.Service;

namespace BookingApp.ViewModel.Guide
{
    public class DismissalViewModel
    {
        private readonly int userId;

        ScheduledTourService scheduledTourService = new ScheduledTourService();
        private VoucherService voucherService = new VoucherService();

        public DismissalViewModel(int userId)
        {
            this.userId = userId;
            CancelScheduledTours();
        }

        public void CancelScheduledTours()
        {
            List<ScheduledTour> scheduledTours = scheduledTourService.GetAllByStatusAndGuideId(Status.Scheduled, userId);

            foreach (ScheduledTour scheduledTour in scheduledTours)
            {
                SendVouchers(scheduledTour);
                scheduledTourService.Delete(scheduledTour);
            }
        }

        private void SendVouchers(ScheduledTour scheduledTour)
        {
            List<int> uniqueTouristIds = FilterUniqueTouristIds(scheduledTour);

            foreach (int touristId in uniqueTouristIds)
            {
                voucherService.Save(new Voucher(touristId, DateTime.Now.AddYears(2)));
            }
        }

        private List<int> FilterUniqueTouristIds(ScheduledTour scheduledTour)
        {
            return scheduledTour.Tourists.Select(t => t.TouristId).Distinct().ToList();
        }
    }
}
