using System;
using LawAgendaWpf.Annotations;

namespace LawAgendaWpf.Data.Models
{
    public class DialogContent
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DialogType Type { get; set; }

        public DialogContent(string title, string content, DialogType type)
        {
            Title = title;
            Content = content;
            Type = type;
        }

        [CanBeNull] public string PositiveButtonText { get; set; }
        [CanBeNull] public string NegativeButtonText { get; set; }

        public string Error { get; set; }
    }

    public enum DialogType
    {
        Loading,
        Error,
        Info
    }

    public class DialogResult<T>
    {
        public T Content { get; set; }
        public bool HasError { get; set; }
        public Exception Exception { get; set; }
    }
}