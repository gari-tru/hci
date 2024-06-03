using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class ComplexTourRequest : ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public List<TourRequest> TourRequests { get; set; }
        public TourRequestStatus Status { get; set; }

        public ComplexTourRequest() { }

        public ComplexTourRequest(int touristId, List<TourRequest> tourRequests, TourRequestStatus status)
        {
            TouristId = touristId;
            TourRequests = tourRequests;
            Status = status;
        }

        public string[] ToCSV()
        {
            string[] tourRequestsCSV = TourRequests.SelectMany(tr => tr.ToCSV()).ToArray();
            string tourRequestsSerialized = string.Join(";", tourRequestsCSV);

            return new string[]
            {
                Id.ToString(),
                TouristId.ToString(),
                tourRequestsSerialized,
                Status.ToString()
            };
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);

            string[] tourRequestsCSV = values[2].Split(';');
            TourRequests = new List<TourRequest>();

            for (int i = 0; i < tourRequestsCSV.Length; i += 10)
            {
                string[] tourRequestValues = tourRequestsCSV.Skip(i).Take(10).ToArray();
                TourRequest tourRequest = new TourRequest();
                tourRequest.FromCSV(tourRequestValues);
                TourRequests.Add(tourRequest);
            }

            Status = Enum.Parse<TourRequestStatus>(values[3]);
        }
    }
}
