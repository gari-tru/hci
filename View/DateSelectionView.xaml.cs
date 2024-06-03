using BookingApp.Model;
using BookingApp.ViewModel;
using System.Collections.Generic;
using System;
using System.Windows;

namespace BookingApp.View
{
    public partial class DateSelectionView : Window
    {
        private readonly DateSelectionViewModel _viewModel;

        public DateSelectionView(List<DateTime> availableDates, int numberOfDays)
        {
            InitializeComponent();
            _viewModel = new DateSelectionViewModel(availableDates, numberOfDays);
            DataContext = _viewModel;
        }

        public DateTime GetSelectedStartDate()
        {
            return _viewModel.GetSelectedStartDate();
        }

        public DateTime GetSelectedEndDate()
        {
            return _viewModel.GetSelectedEndDate();
        }

        private void ReserveSelectedDates(object sender, RoutedEventArgs e)
        {
            _viewModel.ReserveSelectedDates();
            DialogResult = true;
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}