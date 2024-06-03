using BookingApp.Model;
using BookingApp.ViewModel;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BookingApp.View
{
    public partial class ReserveAccommodationView : Window
    {
        private readonly User user;
        private readonly ReserveAccommodationViewModel _viewModel;

        public ReserveAccommodationView(User user, Accommodation selectedAccommodation)
        {
            this.user = user;
            InitializeComponent();
            _viewModel = new ReserveAccommodationViewModel(selectedAccommodation, user);
            DataContext = _viewModel;
        }

        private void PreviousImage_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.PreviousImage();
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.NextImage();
        }

        private void ReserveAccommodation(object sender, RoutedEventArgs e)
        {
            _viewModel.ReserveAccommodation();
        }


        private void ShowAccommodationOverview(object sender, RoutedEventArgs e)
        {
            titleBar.ShowAccommodationOverview(sender, e);
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }
    }
}