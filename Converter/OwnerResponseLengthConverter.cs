﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace BookingApp.Converter
{
    public class OwnerResponseLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ownerResponse = value as string;
            if (ownerResponse != null && ownerResponse.Length > 100)
            {
                return ownerResponse.Substring(0, 100) + "...";
            }
            return ownerResponse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
