using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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
        }

        private DispatcherTimer CookiesTimer = new DispatcherTimer();

        double cookies = 0;

        double cursorPrice = 15;
        double grandmaPrice = 100;
        double farmPrice = 1100;
        double minePrice = 12000;
        double factoryPrice = 130000;

        double cursorPerSec = 0;
        double grandmaPerSec = 0;
        double farmPerSec = 0;
        double minePerSec = 0;
        double factoryPerSec = 0;
        double totalPerSec = 0;

        double cursorValue = 0.001;
        double grandmaValue = 0.01;
        double farmValue = 0.08;
        double mineValue = 0.47;
        double factoryValue = 2.6;

        double cursorValueSec = 0.1;
        double grandmaValueSec = 1;
        double farmValueSec = 8;
        double mineValueSec = 47;
        double factoryValueSec = 260;

        int cursorAmount = 0;
        int grandmaAmount = 0;
        int farmAmount = 0;
        int mineAmount = 0;
        int factoryAmount = 0;

        private void ImgCookie_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            cookies += 1;
            LblCookies.Content = Math.Round(cookies) + " Cookies";
            ImgCookie.Height = 200;
            ImgCookie.Width = 200;
            ImgCookie.Margin = new Thickness(80, 10, 0, 0);
        }

        private void CookiesPerSec()
        {
            totalPerSec = cursorPerSec + grandmaPerSec + farmPerSec + minePerSec + factoryPerSec;
        }

        private void WrapCursor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cookies > cursorPrice)
            {
                cookies -= cursorPrice;
                cursorAmount += 1;
                cursorPrice *= 1.15;
                LblCursor.Content = cursorAmount;
                LblCursorPrice.Content = Math.Round(cursorPrice);

                cursorPerSec = cursorAmount * cursorValue * 100;
                CookiesPerSec();
            }
        }

        private void WrapGrandma_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cookies >= grandmaPrice)
            {
                
                cookies -= grandmaPrice;
                grandmaAmount += 1;
                grandmaPrice *= 1.15;
                LblGrandma.Content = grandmaAmount;
                LblGrandmaPrice.Content = Math.Round(grandmaPrice);

                grandmaPerSec = grandmaAmount * grandmaValue * 100;
                CookiesPerSec();
            }
        }

        private void WrapFarm_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cookies >= farmPrice)
            {
                cookies -= farmPrice;
                farmAmount += 1;
                farmPrice *= 1.15;
                LblFarm.Content = farmAmount;
                LblFarmPrice.Content = Math.Round(farmPrice);

                farmPerSec = farmAmount * farmValue * 100;
                CookiesPerSec();
            }
        }

        private void WrapMine_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cookies >= minePrice)
            {
                cookies -= minePrice;
                mineAmount += 1;
                minePrice *= 1.15;
                LblMine.Content = mineAmount;
                LblMinePrice.Content = Math.Round(minePrice);

                minePerSec = mineAmount * mineValue * 100;
                CookiesPerSec();
            }
        }

        private void WrapFactory_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cookies >= factoryPrice)
            {
                cookies -= factoryPrice;
                factoryAmount += 1;
                factoryPrice *= 1.15;
                LblFactory.Content = factoryAmount;
                LblFactoryPrice.Content = Math.Round(factoryPrice);

                factoryPerSec = factoryAmount * factoryValue * 100;
                CookiesPerSec();
            }
        }

        private void TimerCookies(object sender, EventArgs e)
        {
            cookies += cursorAmount * cursorValue;
            cookies += grandmaAmount * grandmaValue;
            cookies += farmAmount * farmValue;
            cookies += mineAmount * mineValue;
            cookies += factoryAmount * factoryValue;

            LblCookies.Content = $"{Math.Round(cookies)} Cookies";
            LblCookiesPerSec.Content = $"{totalPerSec} cps";
        }

        private void ImgCookie_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ImgCookie.Height = 190;
            ImgCookie.Width = 190;
            ImgCookie.Margin = new Thickness(85, 15, 5, 5);
        }

        private void MouseEnterVoid(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void MouseLeaveVoid(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CookiesTimer.Tick += new EventHandler(TimerCookies);
            CookiesTimer.Interval = TimeSpan.FromMilliseconds(10);
            CookiesTimer.Start();
        }

        private void WrapBank_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void WrapWizard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void WrapTemple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
