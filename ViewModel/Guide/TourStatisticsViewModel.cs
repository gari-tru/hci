using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using BookingApp.Dto;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;

namespace BookingApp.ViewModel.Guide
{
    public class TourStatisticsViewModel : ViewModelBase
    {
        public ObservableCollection<TourDto> _tourDto { get; set; }

        public int Kids { get; set; }
        public int Adults { get; set; }
        public int Seniors { get; set; }

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

        public TourStatisticsViewModel(TourDto tourDto)
        {
            _tourDto = new ObservableCollection<TourDto> { tourDto };
            (Kids, Adults, Seniors) = tourDto.ScheduledTour.CalculateTouristStatistics();
            Values = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Broj turista:",
                    Values = new ChartValues<int> { Kids, Adults, Seniors },
                    DataLabels = true
                }
            };
            OnPropertyChanged(nameof(Values));
            Labels = new ObservableCollection<string> { "<18", "18-50", ">50" };
        }

        public void GeneratePdfReport()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.Title = "Save PDF Report";
            saveFileDialog.FileName = $"tour_statistics_{DateTime.Now}.pdf";

            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName;

                Document doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.Open();

                Chunk companyChunk = new Chunk("BOOKING APP", new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD));
                Paragraph companyTitle = new Paragraph(companyChunk);
                doc.Add(companyTitle);

                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));

                Paragraph title = new Paragraph("Tour Requests Statistics", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));

                string yearText = Year == 0 ? "Year: Overall" : $"Year: {Year}";
                doc.Add(new Paragraph(yearText));
                doc.Add(new Paragraph($"Total Requests: {TotalRequests}"));
                doc.Add(new Paragraph($"Accepted Requests: {AcceptedRequests}"));
                doc.Add(new Paragraph($"Rejected Requests: {RejectedRequests}"));
                doc.Add(new Paragraph($"Accepted Percentage: {AcceptedPercentage:F2}%"));
                doc.Add(new Paragraph($"Rejected Percentage: {RejectedPercentage:F2}%"));
                doc.Add(new Paragraph($"Average Participants in Accepted Requests: {AverageParticipantsInAcceptedRequests:F2}"));

                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("----------------------------------------------"));
                doc.Add(new Paragraph(" "));

                Chunk overallRequestsByLanguageTitleChunk = new Chunk("Overall Requests by Language: ", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
                Paragraph overallRequestsByLanguageTitle = new Paragraph(overallRequestsByLanguageTitleChunk);
                doc.Add(overallRequestsByLanguageTitle);

                foreach (var entry in RequestsByLanguage)
                {
                    doc.Add(new Paragraph($"{entry.Key}: {entry.Value}"));
                }

                doc.Add(new Paragraph(" "));

                Chunk overallRequestsByLocationTitleChunk = new Chunk("Overall Requests by Location: ", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
                Paragraph overallRequestsByLocationTitle = new Paragraph(overallRequestsByLocationTitleChunk);
                doc.Add(overallRequestsByLocationTitle);

                foreach (var entry in RequestsByLocation)
                {
                    doc.Add(new Paragraph($"{entry.Key}: {entry.Value}"));
                }

                doc.Close();

                Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("PDF generation cancelled.", "Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
