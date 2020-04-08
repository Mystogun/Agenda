using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Flurl.Http;
using LawAgendaWpf.Constants;
using LawAgendaWpf.Data.Apis;
using LawAgendaWpf.Utilities;
using LawAgendaWpf.Windows.Cases;

namespace LawAgendaWpf.Windows.Login
{
    public class LoginViewModel : ViewModelBase
    {
        private RelayCommand<object> _commandLogin;

        public LoginViewModel(Action action)
        {
            this.Action = action;
        }

        public string Username { get; set; } = "yolanda";
        public string Password { get; set; } = "password";

        public Action Action { get; set; }

        public ICommand CmdLoginExecution
        {
            get
            {
                if (_commandLogin == null)
                {
                    _commandLogin = new RelayCommand<object>(param => Login(), param => CanAttemptLogin());
                }

                return _commandLogin;
            }
        }

        public string DialogIdentifier { get; set; } = "LoginErrorDialog";

        private bool CanAttemptLogin()
        {
            return !IsLoading && !string.IsNullOrEmpty(Username) && !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrEmpty(Password) && !string.IsNullOrWhiteSpace(Password);
        }

        private async void Login()
        {
            IsLoading = true;
            try
            {
                var loginResponse = await AuthApi.Login(Username, Password);

                Action.Invoke();
            }
            catch (FlurlHttpException e)
            {
                await Helpers.ShowError(DialogIdentifier, e);
            }

            IsLoading = false;
        }
    }
}