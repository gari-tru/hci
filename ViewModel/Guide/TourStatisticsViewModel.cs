using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.ObjectModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows;
using BookingApp.Dto;
using BookingApp.Command;
using BookingApp.Model;

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

        public RelayCommand PDFReport { get; set; }
        public TourDto mostVisitedTourDto { get; set; }
        public TourDto TourDtoo { get; set; }
        private ScheduledTourService scheduledTourService = new ScheduledTourService();
        private TourService tourService = new TourService();


        public TourStatisticsViewModel(TourDto tourDto)
        {
            _tourDto = new ObservableCollection<TourDto> { tourDto };
            TourDtoo = tourDto;
            PDFReport = new RelayCommand(PDFReportExcecute);
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
            ScheduledTour scheduledTour = scheduledTourService.GetMostVisitedByYear(3, "Za sva vremena");
            if (scheduledTour != null)
            {
                Tour tour = tourService.GetById(scheduledTour.TourId);
                mostVisitedTourDto = new TourDto(tour, scheduledTour);
            }
        }

        public void PDFReportExcecute(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.Title = "Save PDF Report";
            saveFileDialog.FileName = $"tour_statistics_{DateTime.Now:ddMMyyyy_HHmmss}.pdf";

            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName;

                Document doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.Open();

                Font titleFont = new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD, BaseColor.BLACK);
                Font subtitleFont = new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD, BaseColor.BLACK);
                Font contentFont = new Font(Font.FontFamily.HELVETICA, 14, Font.NORMAL, BaseColor.BLACK);
                Font labelFont = new Font(Font.FontFamily.HELVETICA, 16, Font.NORMAL, BaseColor.BLACK);

                Chunk companyChunk = new Chunk("Booking App", new Font(Font.FontFamily.HELVETICA, 40, Font.BOLD));
                Paragraph companyTitle = new Paragraph(companyChunk);
                companyTitle.Alignment = Element.ALIGN_CENTER;
                doc.Add(companyTitle);

                Paragraph mainTitle = new Paragraph("Statistika o turama", titleFont);
                mainTitle.Alignment = Element.ALIGN_CENTER;
                doc.Add(mainTitle);

                doc.Add(new Paragraph("Statistika o turi", subtitleFont));
                Image img = Image.GetInstance(TourDtoo.Tour.Images[0]);
                img.ScaleToFit(250f, 250f);
                doc.Add(img);
                doc.Add(new Paragraph($"Naziv: {TourDtoo.Tour.Name}", contentFont));
                doc.Add(new Paragraph($"Lokacija: {TourDtoo.Tour.Location}", contentFont));
                doc.Add(new Paragraph($"Datum: {TourDtoo.ScheduledTour.Start}", contentFont));

                doc.Add(new Paragraph("Broj turista po starosnoj grupi", subtitleFont));

                for (int i = 0; i < 3; i++)
                {
                    doc.Add(new Paragraph($"{Labels[i]}: {Values[0].Values[i]}", labelFont));
                }

                doc.Add(new Paragraph("Najposecenija tura", subtitleFont));
                img = Image.GetInstance(mostVisitedTourDto.Tour.Images[0]);
                img.ScaleToFit(250f, 250f);
                doc.Add(img);
                doc.Add(new Paragraph($"Broj turista: {mostVisitedTourDto.ScheduledTour.Tourists.Count}", contentFont));
                doc.Add(new Paragraph($"Naziv: {mostVisitedTourDto.Tour.Name}", contentFont));
                doc.Add(new Paragraph($"Datum: {mostVisitedTourDto.ScheduledTour.Start}", contentFont));

                doc.Close();

                Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
            }
        }

    }
}
