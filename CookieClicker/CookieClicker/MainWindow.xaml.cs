using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            CookiesTimer.Tick += new EventHandler(TimerCookies);
            CookiesTimer.Interval = TimeSpan.FromMilliseconds(10);
            CookiesTimer.Start();
        }

        private DispatcherTimer CursorTimer = new DispatcherTimer();
        private DispatcherTimer GrandmaTimer = new DispatcherTimer();
        private DispatcherTimer FarmTimer = new DispatcherTimer();
        private DispatcherTimer MineTimer = new DispatcherTimer();
        private DispatcherTimer FactoryTimer = new DispatcherTimer();
        private DispatcherTimer CookiesTimer = new DispatcherTimer();

        double cookies = 0;

        double cursorPrice = 15;
        double grandmaPrice = 100;
        double farmPrice = 1100;
        double minePrice = 12000;
        double factoryPrice = 130000;

        double cursorValue = 1;
        double grandmaValue = 1;
        double farmValue = 1;
        double mineValue = 1;
        double factoryValue = 1;

        int cursorAmount = 0;
        int grandmaAmount = 0;
        int farmAmount = 0;
        int mineAmount = 0;
        int factoryAmount = 0;

        private void ImgCookie_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            cookies += 1;
            LblCookies.Content = Math.Round(cookies) + " Cookies";
        }

        private void ImgCursor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ImgGrandma_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
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

                if (cursorAmount == 1)
                {
                    CursorTimer.Tick += new EventHandler(TimerCursor);
                    CursorTimer.Interval = TimeSpan.FromMilliseconds(10);
                    CursorTimer.Start();
                }
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
            }

            if (grandmaAmount == 1)
            {
                GrandmaTimer.Tick += new EventHandler(TimerGrandma);
                GrandmaTimer.Interval = TimeSpan.FromMilliseconds(10);
                GrandmaTimer.Start();
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
            }

            if (farmAmount == 1)
            {
                FarmTimer.Tick += new EventHandler(TimerFarm);
                FarmTimer.Interval = TimeSpan.FromMilliseconds(10);
                FarmTimer.Start();
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
            }

            if (mineAmount == 1)
            {
                MineTimer.Tick += new EventHandler(TimerMine);
                MineTimer.Interval = TimeSpan.FromMilliseconds(10);
                MineTimer.Start();
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
            }

            if (factoryAmount == 1)
            {
                FactoryTimer.Tick += new EventHandler(TimerFactory);
                FactoryTimer.Interval = TimeSpan.FromMilliseconds(10);
                FactoryTimer.Start();
            }
        }

        private void TimerCursor(object sender, EventArgs e)
        {
            cookies += cursorAmount * 0.001;
        }

        private void TimerGrandma(object sender, EventArgs e)
        {
            cookies += grandmaAmount * 0.01;
        }

        private void TimerFarm(object sender, EventArgs e)
        {
            cookies += farmAmount * 0.08;
        }

        private void TimerMine(object sender, EventArgs e)
        {
            cookies += mineAmount * 0.47;
        }

        private void TimerFactory(object sender, EventArgs e)
        {
            cookies += factoryAmount * 2.60;
        }

        private void TimerCookies(object sender, EventArgs e)
        {
            LblCookies.Content = $"{Math.Round(cookies)} Cookies";
        }
    }
}
