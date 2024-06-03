﻿using BookingApp.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.ViewModel
{
    public class GuestMainWindowModel : ViewModelBase
    {
        private readonly User _user;

        public GuestMainWindowModel(User user)
        {
            _user = user;
        }

        public string Username
        {
            get { return _user.Username; }
        }

        public User User
        {
            get { return _user; }
        }
    }
}