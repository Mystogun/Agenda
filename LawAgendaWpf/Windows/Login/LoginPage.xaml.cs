using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using LawAgendaWpf.Windows.Cases;

namespace LawAgendaWpf.Windows.Login
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();

            DataContext = new LoginViewModel(ShowCasesPage);

        }

        public void ShowCasesPage()
        {
            NavigationService.Navigate(new CasesPage());
        }
    }
}