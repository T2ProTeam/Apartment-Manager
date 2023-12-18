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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

using AM.UI.Utilities;

using AM.UI.ViewModelUI;
using Services.Interface;
using Services.Implement;
using ViewModel.Dtos;
using ViewModel.People;
using Microsoft.EntityFrameworkCore.Diagnostics;
using AM.UI.Model;

namespace AM.UI.View.People
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private Button current = null;
        private ControlObject co = new ControlObject();
        private int Pagecount = 16;
        private int Pagesize = 8;
        private int PageIndex = 1;
        private string keyword = string.Empty;
        private PagedResult<CustomerVM> pagedResultCS = new PagedResult<CustomerVM>();

        public Home()
        {
            InitializeComponent();
            Loaddata();
        }

        private async void Loaddata()
        {
            Button button = new Button();
            button.Content = 1;
            button.Name = "button1";
            button.Style = (Style)FindResource("pagingButtonchange");
            button.Click += Button_Click;
            pagingbutton.Children.Add(button);
            pagingLeft.IsEnabled = false;
            current = button;
            if (pagedResultCS.PageCount>1)
                for (int i = 2; i <= pagedResultCS.PageCount; i++)
                {
                    button = new Button();
                    button.Content = i.ToString();
                    button.Name = "button"+ i.ToString();
                    button.Style = (Style)FindResource("pagingButton");
                    button.Click += Button_Click;
                    pagingbutton.Children.Add(button);
                }
            else pagingRight.IsEnabled = false;
        }

        private Button FindButtonByIndex(int index)
        {
            if (index >= 0 && index < pagingbutton.Children.Count)
            {
                if (pagingbutton.Children[index] is Button button)
                {
                    return button;
                }
            }

            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (current != null)
            {
                current.Style = (Style)FindResource("pagingButton");
            }

            Button clickedButton = sender as Button;
            pagingLeft.IsEnabled = co.EnableLeft(clickedButton);
            pagingRight.IsEnabled = co.EnableRight(clickedButton, "button"+Pagecount.ToString());
            clickedButton.Style =(Style)FindResource("pagingButtonchange");
            current = clickedButton;
            // int so = pagingbutton.Children.IndexOf(clickedButton);
            PageIndex = Convert.ToInt32(clickedButton.Content);
            /*  if (PageIndex>2 && PageIndex< Pagecount-2)
              {
                  for (int i = 1; i<=2; i++)
                  {
                      if (FindButtonByIndex(so+i).Name != ("button"+(PageIndex+i).ToString()))
                      {
                          if (so<6)
                              pagingbutton.Children.RemoveAt(so+i);
                          Button button = new Button();
                          button.Content = (Convert.ToInt32(clickedButton.Content)+i).ToString();
                          button.Name = "button"+ (Convert.ToInt32(clickedButton.Content)+i).ToString();
                          button.Style = (Style)FindResource("pagingButton");
                          button.Click += Button_Click;

                          pagingbutton.Children.Insert(so+i, button);
                      }
                      if (FindButtonByIndex(so-i).Name != ("button"+(PageIndex-i).ToString()))
                      {
                          if (so>2)
                              pagingbutton.Children.RemoveAt(so-i);
                          Button button = new Button();
                          button.Content = (Convert.ToInt32(clickedButton.Content)-i).ToString();
                          button.Name = "button"+ (Convert.ToInt32(clickedButton.Content)-i).ToString();
                          button.Style = (Style)FindResource("pagingButton");
                          button.Click += Button_Click;
                          pagingbutton.Children.Insert(so-i, button);
                      }
                  }
              }*/

            // code  trang

            //code trang
        }

        private void Button_page(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            string buttonValue = clickedButton.Tag as string;
            string sosanh = "button"+Pagecount.ToString();
            if (buttonValue.Equals("right"))
            {
                if (!current.Name.Equals(sosanh))
                {
                    string temp = "button"+ (Convert.ToInt32(current.Content)+1).ToString();
                    Button button = co.FindChildButton(this, temp);
                    if (button != null)
                    {
                        pagingLeft.IsEnabled = co.EnableLeft(button);
                        pagingRight.IsEnabled = co.EnableRight(button, "button"+Pagecount.ToString());
                        button.Style =(Style)FindResource("pagingButtonchange");
                        current.Style = (Style)FindResource("pagingButton");
                        current = button;
                        // code  trang

                        //code trang
                    }
                }
            }
            else if (buttonValue.Equals("left"))
            {
                if (!current.Name.Equals("button1"))
                {
                    string temp = "button"+ (Convert.ToInt32(current.Content)-1).ToString();
                    Button button = co.FindChildButton(this, temp);
                    if (button != null)
                    {
                        pagingLeft.IsEnabled = co.EnableLeft(button);
                        pagingRight.IsEnabled = co.EnableRight(button, "button"+Pagecount.ToString());
                        button.Style =(Style)FindResource("pagingButtonchange");
                        current.Style = (Style)FindResource("pagingButton");
                        current = button;
                        // code  trang

                        //code trang
                    }
                }
            }
        }

        private void txtSearch_MouseEnter(object sender, MouseEventArgs e)
        {
            if (textblock.Text.Equals("search by id......")) textblock.Text="";
        }

        private void txtSearch_MouseLeave(object sender, MouseEventArgs e)
        {
            if (textblock.Text.Equals(""))
                textblock.Text ="search by id......";
        }
    }
}