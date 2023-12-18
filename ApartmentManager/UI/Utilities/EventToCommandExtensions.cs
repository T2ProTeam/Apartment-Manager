using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AM.UI.Utilities
{
    public static class EventToCommandExtensions
    {
        public static readonly DependencyProperty MouseLeftButtonDownCommandProperty =
       DependencyProperty.RegisterAttached("MouseLeftButtonDownCommand", typeof(ICommand), typeof(EventToCommandExtensions), new PropertyMetadata(OnMouseLeftButtonDownCommandChanged));

        public static ICommand GetMouseLeftButtonDownCommand(DependencyObject obj) => (ICommand)obj.GetValue(MouseLeftButtonDownCommandProperty);

        public static void SetMouseLeftButtonDownCommand(DependencyObject obj, ICommand value) => obj.SetValue(MouseLeftButtonDownCommandProperty, value);

        private static void OnMouseLeftButtonDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                element.MouseLeftButtonDown += (sender, args) =>
                {
                    var command = GetMouseLeftButtonDownCommand(element);
                    if (command != null && command.CanExecute(null))
                    {
                        command.Execute(null);
                        args.Handled = true;
                    }
                };
            }
        }
    }
}