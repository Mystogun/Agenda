using System.ComponentModel;
using System.Windows;
using PropertyChanged;

namespace LawAgendaWpf.Utilities
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModelBase : INotifyPropertyChanged
    {
        private bool _isLoading = false;

        public Visibility IsLoadingVisible { get; set; } = Visibility.Hidden;

        public bool IsInteractionEnabled { get; set; } = true;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                this._isLoading = value;
                this.IsLoadingVisible = value ? Visibility.Visible : Visibility.Hidden;
                IsInteractionEnabled = !value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, args) => { };
    }
}