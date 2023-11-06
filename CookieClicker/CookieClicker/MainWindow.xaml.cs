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

namespace CookieClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LblCursor.Content = $"X{cursorAmount}";
            LblCursorPrice.Content = $"{cursorPrice} cookies";
            LblGrandma.Content = $"X{grandmaAmount}";
            LblGrandmaPrice.Content = $"{grandmaPrice} cookies";
            LblCookies.Content = Math.Round(cookies) + " Cookies";
        }

        double cookies = 0;
        double cursorPrice = 10;
        int cursorAmount = 1;
        double grandmaPrice = 1000;
        int grandmaAmount = 0;

        private void ImgCookie_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            cookies += cursorAmount;
            LblCookies.Content = Math.Round(cookies) + " Cookies";
        }

        private void ImgCursor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(cookies > cursorPrice)
            {
                cookies -= cursorPrice;
                cursorAmount += 1;
                cursorPrice *= 1.8;
                LblCursor.Content = $"X{cursorAmount}";
                LblCursorPrice.Content = $"{Math.Round(cursorPrice)} cookies";
                LblCookies.Content = Math.Round(cookies) + " Cookies";
            }
            else
            {
                MessageBox.Show("Not enough cookies");
            }
        }

        private void ImgGrandma_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(cookies >= grandmaPrice)
            {
                cookies -= grandmaPrice;
                grandmaAmount += 1;
                grandmaPrice *= 1.8;
                LblGrandma.Content = $"X{grandmaAmount}";
                LblGrandmaPrice.Content = $"{Math.Round(grandmaPrice)} cookies";
                LblCookies.Content = Math.Round(cookies) + " Cookies";
            }
        }
    }
}
