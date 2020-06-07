using MVVMAcademy.Lib.UI;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace MVVMAcademy.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        string _loginResult;
        public string LoginResult
        {
            get
            {
                return _loginResult;
            }
            set
            {
                _loginResult = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel()
        {
            Email = "admin@academy.com";

            LoginCommand = new RouteCommand(Login);
        }

        public void Login()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                LoginResult = "El Email o el Password son incorrectos";
            }
            else
            {
                LoginResult = "El login está bien, bienvenid@!";
                Email = string.Empty;
            }
        }

        #region Commands

        public ICommand LoginCommand { get; set; }

        #endregion
    }
}
