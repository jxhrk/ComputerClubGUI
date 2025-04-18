using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace CompClubGUICore.Views.Controls
{
    internal static class NavigationControlUtils
    {
        public static void Bind(Control control)
        {
            control.KeyDown += OnKeyDown;
        }

        private static void OnKeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    Navigate(sender, true, true);
                    break;
                case Key.Down:
                    Navigate(sender, true, false);
                    break;
                case Key.Up:
                    Navigate(sender, false, false);
                    break;
            }
        }

        private static void Navigate(object? sender, bool down, bool raiseClick)
        {
            if (sender is not Control control) { return; }

            NavigationDirection direction = down ? NavigationDirection.Next : NavigationDirection.Previous;
            IInputElement? next = KeyboardNavigationHandler.GetNext(control, direction);
            if (next == null) { return; }

            if (next is TextBox)
            {
                next.Focus();
            }
            else if (raiseClick && next is Button b)
            {
                b.Focus();
                b.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
    }
}
