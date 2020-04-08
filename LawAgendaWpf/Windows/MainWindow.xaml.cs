using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LawAgendaWpf.Constants;
using LawAgendaWpf.Windows.Cases;
using LawAgendaWpf.Windows.Login;

namespace LawAgendaWpf.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Main.Content = new LoginPage();
            Helpers.NavigationService = Main.NavigationService;

            DataContext = new MainViewModel();
        }
    }
}