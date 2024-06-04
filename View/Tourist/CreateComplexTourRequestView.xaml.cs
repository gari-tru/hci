using System;
using System.Windows;
using System.Windows.Input;
using BookingApp.Model;
using BookingApp.ViewModel.Tourist;

namespace BookingApp.View.Tourist
{
    public partial class CreateComplexTourRequestView : Window
    {
        private readonly User user;
        private readonly CreateComplexTourRequestViewModel _createComplexTourRequestViewModel;

        public CreateComplexTourRequestView(User user)
        {
            InitializeComponent();
            this.user = user;
            _createComplexTourRequestViewModel = new CreateComplexTourRequestViewModel(user);
            DataContext = _createComplexTourRequestViewModel;
        }

        private void CreateComplexTourRequestButton_Click(object sender, RoutedEventArgs e)
        {
            _createComplexTourRequestViewModel.CreateComplexTourRequest();
            Close();
        }

        private void AddRequestButton_Click(object sender, RoutedEventArgs e)
        {
            _createComplexTourRequestViewModel.AddTourRequest();
        }

        private void OpenComplexTourRequestsPage_Click(object sender, RoutedEventArgs e)
        {
            ComplexTourRequestsView complexTourRequestesView = new ComplexTourRequestsView(user);
            complexTourRequestesView.Show();
        }

        private void DatePicker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            DateTime parsedDateTime;
            if (!DateTime.TryParse(e.Text, out parsedDateTime))
            {
                e.Handled = true;
            }
        }

        private void AddParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            _createComplexTourRequestViewModel.AddParticipant();
        }
    }
}
