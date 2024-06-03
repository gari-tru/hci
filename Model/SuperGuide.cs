using System;
using System.Globalization;
using System.Windows;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class SuperGuide : ISerializable
    {
        public int Id { get; set; }
        public int GuideId { get; set; }
        public DateTime Start { get; set; }
        public string Language { get; set; }

        public SuperGuide() { }

        public SuperGuide(int guideId, DateTime start, string language)
        {
            GuideId = guideId;
            Start = start;
            Language = language;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuideId.ToString(), Start.ToString((string)Application.Current.Resources["DateTimeFormat"]), Language };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuideId = Convert.ToInt32(values[1]);
            Start = DateTime.ParseExact(values[2], (string)Application.Current.Resources["DateTimeFormat"], CultureInfo.InvariantCulture);
            Language = values[3];
        }
    }
}
