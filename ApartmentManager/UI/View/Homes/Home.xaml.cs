using AM.UI.ViewModelUI;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.View.Homes
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private HomeVM asd = new HomeVM();

        public Home()
        {
            InitializeComponent();
            ObservableCollection<Nemmber> nember = new ObservableCollection<Nemmber>();
            nember.Add(new Nemmber() { ID =1, Charactic="T", Name="Thien", Old="2", Year ="1" });
            nember.Add(new Nemmber() { ID =2, Charactic="T", Name="Thien", Old="2", Year ="2" });
            nember.Add(new Nemmber() { ID =3, Charactic="T", Name="Thien", Old="2", Year ="3" });
            nember.Add(new Nemmber() { ID =4, Charactic="T", Name="Thien", Old="2", Year ="4" });
            nember.Add(new Nemmber() { ID =5, Charactic="T", Name="Thien", Old="2", Year ="5" });
            nember.Add(new Nemmber() { ID =6, Charactic="T", Name="Thien", Old="2", Year ="6" });
            nember.Add(new Nemmber() { ID =7, Charactic="T", Name="Thien", Old="2", Year ="7" });
            nember.Add(new Nemmber() { ID =8, Charactic="T", Name="Thien", Old="2", Year ="8" });
            this.DataContext = asd;
            homedata.ItemsSource=nember;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }

    public class Nemmber
    {
        public int ID { get; set; }
        public string Charactic { get; set; }
        public string Name { get; set; }
        public string Old { get; set; }
        public string Year { get; set; }
    }
}