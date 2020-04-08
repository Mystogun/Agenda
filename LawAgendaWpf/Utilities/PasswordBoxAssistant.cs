using System;
using System.Windows;
using System.Windows.Controls;

namespace LawAgendaWpf.Utilities
{
    public static class PasswordBoxAssistant
    {
        public static readonly DependencyProperty BoundPassword =
            DependencyProperty.Register("BoundPassword", typeof(string), typeof(PasswordBoxAssistant),
                new PropertyMetadata(String.Empty, OnBoundPasswordChanged));

        public static readonly DependencyProperty BindPassword =
            DependencyProperty.Register("BindPassword", typeof(string), typeof(PasswordBoxAssistant),
                new PropertyMetadata(false, OnBindPasswordChanged));

        public static readonly DependencyProperty UpdatingPassword =
            DependencyProperty.Register("UpdatingPassword", typeof(string), typeof(PasswordBoxAssistant),
                new PropertyMetadata(false));

        private static void OnBoundPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = dp as PasswordBox;

            // only handle this event when the property is attached to a PasswordBox
            // and when the BindPassword attached property has been set to true
            if (dp == null || !GetBindPassword(dp))
            {
                return;
            }

            // avoid recursive updating by ignoring the box's changed event
            passwordBox.PasswordChanged -= HandlePasswordChanged;

            var newPassword = (string) e.NewValue;

            if (!GetUpdatingPassword(passwordBox))
            {
                passwordBox.Password = newPassword;
            }

            passwordBox.PasswordChanged += HandlePasswordChanged;
        }

        private static void OnBindPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            // when the BindPassword attached property is set on a PasswordBox,
            // start listening to its PasswordChanged event

            var passwordBox = dp as PasswordBox;

            if (passwordBox == null)
            {
                return;
            }

            var wasBound = (bool) e.OldValue;
            var needToBind = (bool) e.NewValue;

            if (wasBound)
            {
                passwordBox.PasswordChanged -= HandlePasswordChanged;
            }

            if (needToBind)
            {
                passwordBox.PasswordChanged += HandlePasswordChanged;
            }
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;

            // set a flag to indicate that we're updating the password
            SetUpdatingPassword(passwordBox, true);
            // push the new password into the BoundPassword property
            SetBoundPassword(passwordBox, passwordBox.Password);
            SetUpdatingPassword(passwordBox, false);
        }

        public static void SetBindPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(BindPassword, value);
        }

        public static bool GetBindPassword(DependencyObject dp)
        {
            return (bool) dp.GetValue(BindPassword);
        }

        public static string GetBoundPassword(DependencyObject dp)
        {
            return (string) dp.GetValue(BoundPassword);
        }

        public static void SetBoundPassword(DependencyObject dp, string value)
        {
            dp.SetValue(BoundPassword, value);
        }
        
        private static bool GetUpdatingPassword(DependencyObject dp)
        {
            return (bool) dp.GetValue(UpdatingPassword);
        }
        
        private static void SetUpdatingPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(UpdatingPassword, value);
        }
        
    }
}