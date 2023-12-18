using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AM.UI.View.Dialog
{
    /// <summary>
    /// Interaction logic for MessageBoxCustom.xaml
    /// </summary>
    public partial class MessageBoxCustom : Window
    {
        public string Input { get; private set; }

        public MessageBoxCustom(string Message, MessageType Type, MessageButtons Buttons)
        {
            InitializeComponent();
            txtMessage.Content = Message;
            switch (Type)
            {
                case MessageType.Info:
                    txtTitle.Text = "Info";
                    break;

                case MessageType.Confirmation:
                    txtTitle.Text = "Confirmation";
                    break;

                case MessageType.Success:
                    {
                        string defaultColor = "#4527a0";
                        Color bkColor = (Color)ColorConverter.ConvertFromString(defaultColor);
                        changeBackgroundThemeColor(Colors.Green);
                        txtTitle.Text = "Success";
                    }
                    break;

                case MessageType.Warning:
                    txtTitle.Text = "Warning";
                    break;

                case MessageType.Input:
                    {
                        string defaultColor = "#4527a0";

                        Color bkColor = (Color)ColorConverter.ConvertFromString(defaultColor);
                        changeBackgroundThemeColor(Colors.Green);
                        txtTitle.Text = "Input";
                        txtMessage.Content = "Enter your Text: ";
                        Istextbox.Visibility = Visibility.Visible;
                    }
                    break;

                case MessageType.Error:
                    {
                        string defaultColor = "#F44336";
                        Color bkColor = (Color)ColorConverter.ConvertFromString(defaultColor);
                        changeBackgroundThemeColor(bkColor);
                        changeBackgroundThemeColor(Colors.Red);
                        txtTitle.Text = "Error";
                    }
                    break;
            }

            switch (Buttons)
            {
                case MessageButtons.OkCancel:
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;

                case MessageButtons.YesNo:
                    btnOk.Visibility = Visibility.Collapsed; btnCancel.Visibility = Visibility.Collapsed;
                    break;

                case MessageButtons.Ok:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        public void changeBackgroundThemeColor(Color newColor)
        {
            cardHeader.Background = new SolidColorBrush(newColor);
            btnClose.Foreground = new SolidColorBrush(newColor);
            btnYes.Background = new SolidColorBrush(newColor);
            btnNo.Background = new SolidColorBrush(newColor);

            btnOk.Background = new SolidColorBrush(newColor);
            btnCancel.Background = new SolidColorBrush(newColor);
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Input= textbox.Text;
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?$");
            if (regex.IsMatch(Input)) { this.Close(); this.DialogResult = true; }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }

    public enum MessageType
    {
        Info,
        Confirmation,
        Success,
        Warning,
        Error,
        Input
    }

    public enum MessageButtons
    {
        OkCancel,
        YesNo,
        Ok,
    }
}