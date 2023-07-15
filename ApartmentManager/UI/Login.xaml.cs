using AM.UI.StartupHelper;
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
using UI;

namespace AM.UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly IAbstractFactory<MainWindow> _childform;

        public Login(IAbstractFactory<MainWindow> childform)
        {
            InitializeComponent();
            _childform = childform;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _childform.Create().Show();
        }
    }
}