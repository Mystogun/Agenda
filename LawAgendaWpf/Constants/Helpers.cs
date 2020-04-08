using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Flurl.Http;
using LawAgendaWpf.Data.Models;
using LawAgendaWpf.Resources;
using LawAgendaWpf.Utilities.Dialogs.General;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;

namespace LawAgendaWpf.Constants
{
    public static class Helpers
    {
        public static NavigationService NavigationService { set; get; }


        public static async Task<object> ShowError(string dialogIdentifier, Exception exception)
        {
            var error = exception.Message;
            if (exception is FlurlHttpException flurlHttpException)
                error = flurlHttpException.Message;

            return DialogHost.Show(new ErrorDialog
            {
                DataContext = new DialogContent(Strings.Error, error, DialogType.Error)
                {
                    Error = Regex.Replace(exception.Message,
                        @"((http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)",
                        "")
                }
            }, dialogIdentifier);
        }
        
        
        
    }
}