using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.VisualBasic;

namespace CookieClicker
{
    /// <summary>
    /// Datum: 07/01/2024
	/// Auteur: Senne Martens
	/// Beschrijving: Hierin wordt de code beschreven voor een werkende cookieclicker met buildings en upgrades.
    /// </summary>
	
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
		
		//Timers declareren
        DispatcherTimer CookiesTimer = new DispatcherTimer();
        DispatcherTimer CheckTimer = new DispatcherTimer();
        DispatcherTimer UpgradesTimer = new DispatcherTimer();
        DispatcherTimer GoldenCookieTimer = new DispatcherTimer();
        DispatcherTimer QuestTimer = new DispatcherTimer();
		
		//Random generator
        private Random random = new Random();
		
		// Naam bakkerij
        string bakeryName = "PXL-Bakery";
		
		//Staat de timer aan voor upgrades?
		//Deze gaat getriggered worden bij genoeg geld voor eerste building.
        bool upgradeTimerEnabled = false;
		
		//Bij genoeg geld gaan specifieke buildings zichtbaar worden
        bool cursorUpgradeEnabled = false;
        bool grandmaUpgradeEnabled = false;
        bool farmUpgradeEnabled = false;
        bool mineUpgradeEnabled = false;
        bool factoryUpgradeEnabled = false;
        bool bankUpgradeEnabled = false;
        bool templeUpgradeEnabled = false;
        bool wizardUpgradeEnabled = false;
		
		//Cookies op dit moment en totaat verzamelde cookies
        double totalEverCookies = 0;
        double cookies = 0;
        double totalGoldenCookies = 0;
		
		//Beginprijs voor cookies
        const double cursorBasisPrijs = 15;
        const double grandmaBasisPrijs = 100;
        const double farmBasisPrijs = 1100;
        const double mineBasisPrijs = 12000;
        const double factoryBasisPrijs = 130000;
        const double bankBasisPrijs = 1400000;
        const double templeBasisPrijs = 20000000;
        const double wizardBasisPrijs = 330000000;

        double cursorPrice = 15;
        double grandmaPrice = 100;
        double farmPrice = 1100;
        double minePrice = 12000;
        double factoryPrice = 130000;
        double bankPrice = 1400000;
        double templePrice = 20000000;
        double wizardPrice = 330000000;

        int cursorLevel = 1;
        int grandmaLevel = 1;
        int farmLevel = 1;
        int mineLevel = 1;
        int factoryLevel = 1;
        int bankLevel = 1;
        int templeLevel = 1;
        int wizardLevel = 1;

        double cursorPerSec = 0;
        double grandmaPerSec = 0;
        double farmPerSec = 0;
        double minePerSec = 0;
        double factoryPerSec = 0;
        double bankPerSec = 0;
        double templePerSec = 0;
        double wizardPerSec = 0;
        double totalPerSec = 0;

        double cursorValue = 0.001;
        double grandmaValue = 0.01;
        double farmValue = 0.08;
        double mineValue = 0.47;
        double factoryValue = 2.6;
        double bankValue = 14;
        double templeValue = 78;
        double wizardValue = 440;

        double cursorValueSec = 0.1;
        double grandmaValueSec = 1;
        double farmValueSec = 8;
        double mineValueSec = 47;
        double factoryValueSec = 260;
        double bankValueSec = 1400;
        double templeValueSec = 7800;
        double wizardValueSec = 44000;

        int cursorAmount = 0;
        int grandmaAmount = 0;
        int farmAmount = 0;
        int mineAmount = 0;
        int factoryAmount = 0;
        int bankAmount = 0;
        int templeAmount = 0;
        int wizardAmount = 0;

        bool quest1 = false;
        bool quest2 = false;
        bool quest3 = false;
        bool quest4 = false;
        bool quest5 = false;
        bool quest6 = false;
        bool quest7 = false;
        bool quest8 = false;
        bool quest9 = false;
        bool quest10 = false;
        bool quest11 = false;
        bool quest12 = false;
        bool quest13 = false;
        bool quest14 = false;
        bool quest15 = false;
        bool quest16 = false;
        bool quest17 = false;
        bool quest18 = false;
        bool quest19 = false;
        bool quest20 = false;

        //Bij het opheffen van de muis boven de cookie
        private void ImgCookie_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            cookies += 1;
            totalEverCookies += 1;
            UpdateCookies();
            ImgCookie.Height = 200;
            ImgCookie.Width = 200;
            ImgCookie.Margin = new Thickness(0);
        }
		
		//Bij het neerdalen van de muis boven de cookie
        private void ImgCookie_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ImgCookie.Height = 190;
            ImgCookie.Width = 190;
            ImgCookie.Margin = new Thickness(0,0,0,10);
        }

        //Mouse or hand
        private void MouseEnterVoid(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }
		
		//Als de muis eender welke plaats verlaat
        private void MouseLeaveVoid(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            ImgCookie.Height = 200;
            ImgCookie.Width = 200;
            ImgCookie.Margin = new Thickness(0);
        }

        //Calculate cookies per sec
        private void CookiesPerSec()
        {
            totalPerSec = cursorPerSec + grandmaPerSec + farmPerSec + minePerSec + factoryPerSec + bankPerSec + templePerSec + wizardPerSec;
        }

        //Buy buildings
        private void WrapCursor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cookies > cursorPrice)
            {
                cookies -= cursorPrice;
                cursorAmount += 1;
                cursorPrice *= 1.15;
                LblCursor.Content = cursorAmount;
                LblCursorPrice.Content = Math.Round(cursorPrice);

                cursorPerSec = cursorAmount * cursorValue * 100 * cursorLevel;
                CookiesPerSec();

                WrapCursorAmount.Children.Add(AddImage("./asset/Cursor.png"));
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

                grandmaPerSec = grandmaAmount * grandmaValue * 100 * grandmaLevel;
                CookiesPerSec();

                WrapGrandmaAmount.Children.Add(AddImage("./asset/Grandma.png"));
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

                farmPerSec = farmAmount * farmValue * 100 * farmLevel;
                CookiesPerSec();

                WrapFarmAmount.Children.Add(AddImage("./asset/Farm.png"));
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

                minePerSec = mineAmount * mineValue * 100 * mineLevel;
                CookiesPerSec();

                WrapMineAmount.Children.Add(AddImage("./asset/mine.png"));
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

                factoryPerSec = factoryAmount * factoryValue * 100 * factoryLevel;
                CookiesPerSec();

                WrapFactoryAmount.Children.Add(AddImage("./asset/Factory.png"));
            }
        }

        private void WrapBank_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cookies >= bankPrice)
            {
                cookies -= bankPrice;
                bankAmount += 1;
                bankPrice *= 1.15;
                LblBank.Content = bankAmount;
                LblBankPrice.Content = Math.Round(bankPrice);

                bankPerSec = bankAmount * bankValue * 100 * bankLevel;
                CookiesPerSec();

                WrapBankAmount.Children.Add(AddImage("./asset/Bank.png"));
            }
        }

        private void WrapTemple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cookies >= templePrice)
            {
                cookies -= templePrice;
                templeAmount += 1;
                templePrice *= 1.15;
                LblTemple.Content = templeAmount;
                LblTemplePrice.Content = Math.Round(templePrice);

                templePerSec = templeAmount * templeValue * 100 * templeLevel;
                CookiesPerSec();

                WrapTempleAmount.Children.Add(AddImage("./asset/Temple.png"));
            }
        }

        private void WrapWizard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cookies >= wizardPrice)
            {
                cookies -= wizardPrice;
                wizardAmount += 1;
                wizardPrice *= 1.15;
                LblWizard.Content = wizardAmount;
                LblWizardPrice.Content = Math.Round(wizardPrice);

                wizardPerSec = wizardAmount * wizardValue * 100 * wizardLevel;
                CookiesPerSec();

                WrapWizardAmount.Children.Add(AddImage("./asset/Wizard.png"));
            }
        }

        //Cookies gaan bijopgeteld worden
        private void TimerCookies(object sender, EventArgs e)
        {
            double newCookies = 0;
            newCookies += cursorAmount * cursorValueSec * cursorLevel;
            newCookies += grandmaAmount * grandmaValueSec * grandmaLevel;
            newCookies += farmAmount * farmValueSec * farmLevel;
            newCookies += mineAmount * mineValueSec * mineLevel;
            newCookies += factoryAmount * factoryValueSec * factoryLevel;
            newCookies += bankAmount * bankValueSec * bankLevel;
            newCookies += templeAmount * templeValueSec * templeLevel;
            newCookies += wizardAmount * wizardValueSec * wizardLevel;

            cookies += newCookies;
            totalEverCookies += newCookies;

            if (!upgradeTimerEnabled)
            {
                if (cookies >= cursorBasisPrijs * 100)
                {
                    UpgradesTimer.Tick += new EventHandler(TimerUpgrades);
                    UpgradesTimer.Interval = TimeSpan.FromMilliseconds(10);
                    UpgradesTimer.Start();
                    upgradeTimerEnabled = true;
                    WrapUpdrades.Visibility = Visibility.Visible;
                }
            }

            if (cookies >= 18000000000000000000)
            {
                LblCookies.Content = "Infinity Cookies";
                CookiesTimer.Stop();
            }
            else
            {
                UpdateCookies();
            }      
        }
		
		//Als er genoeg cookies zijn gaan de buildings zichtbaar worden.
        private void TimerCheck(object sender, EventArgs e)
        {
            if (totalEverCookies >= cursorPrice)
            {
                WrapCursor.Visibility = Visibility.Visible;
            }

            if (totalEverCookies >= grandmaPrice)
            {
                WrapGrandma.Visibility = Visibility.Visible;
            }

            if (totalEverCookies >= farmPrice)
            {
                WrapFarm.Visibility = Visibility.Visible;
            }

            if (totalEverCookies >= minePrice)
            {
                WrapMine.Visibility = Visibility.Visible;
            }

            if (totalEverCookies >= factoryPrice)
            {
                WrapFactory.Visibility = Visibility.Visible;
            }

            if (totalEverCookies >= bankPrice)
            {
                WrapBank.Visibility = Visibility.Visible;
            }

            if (totalEverCookies >= templePrice)
            {
                WrapTemple.Visibility = Visibility.Visible;
            }

            if (totalEverCookies >= wizardPrice)
            {
                WrapWizard.Visibility = Visibility.Visible;
                CheckTimer.Stop();
            }
        }
		
		//Triggered specifieke building upgrades
        private void TimerUpgrades(object sender, EventArgs e)
        {
            CursorUpgrade();
            GrandmaUpgrade();
            FarmUpgrade();
            MineUpgrade();
            FactoryUpgrade();
            BankUpgrade();
            TempleUpgrade();
            WizardUpgrade();
            CookiesPerSec();
        }

        //Kijkt of er genoeg cookies zijn voor een building upgrade.
        private void CursorUpgrade()
        {
            if (cursorUpgradeEnabled == false)
            {
                if ((cookies >= cursorBasisPrijs * 100 * cursorLevel) && (cursorAmount >= 1) && ImgCursorUpgrade.Visibility != Visibility.Visible)
                {
                    ImgCursorUpgrade.Visibility = Visibility.Visible;
                    ImgCursorUpgrade.ToolTip = $"Upgrade cursor naar x{cursorLevel * 2} voor {Math.Round(cursorBasisPrijs * 100 * cursorLevel)} cookies";
                    cursorUpgradeEnabled = true;
                }
                else
                {
                    ImgCursorUpgrade.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (cookies < cursorBasisPrijs * 100 * cursorLevel)
                {
                    ImgCursorUpgrade.Visibility = Visibility.Collapsed;
                    cursorUpgradeEnabled = false;
                }
            }
        }

        private void GrandmaUpgrade()
        {
            if (grandmaUpgradeEnabled == false)
            {
                if ((cookies >= grandmaBasisPrijs * 100 * grandmaLevel) && (grandmaAmount >= 1) && ImgGrandmaUpgrade.Visibility != Visibility.Visible)
                {
                    ImgGrandmaUpgrade.Visibility = Visibility.Visible;
                    ImgGrandmaUpgrade.ToolTip = $"Upgrade grandma naar x{grandmaLevel * 2} voor {Math.Round(grandmaBasisPrijs * 100 * grandmaLevel)} cookies";
                    grandmaUpgradeEnabled = true;
                }
                else
                {
                    ImgGrandmaUpgrade.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (cookies < grandmaBasisPrijs * 100 * grandmaLevel)
                {
                    ImgGrandmaUpgrade.Visibility = Visibility.Collapsed;
                    grandmaUpgradeEnabled = false;
                }
            }
        }

        private void FarmUpgrade()
        {
            if (farmUpgradeEnabled == false)
            {
                if ((cookies >= farmBasisPrijs * 100 * farmLevel) && (farmAmount >= 1) && ImgFarmUpgrade.Visibility != Visibility.Visible)
                {
                    ImgFarmUpgrade.Visibility = Visibility.Visible;
                    ImgFarmUpgrade.ToolTip = $"Upgrade farm naar x{farmLevel * 2} voor {Math.Round(farmBasisPrijs * 100 * farmLevel)} cookies";
                    farmUpgradeEnabled = true;
                }
                else
                {
                    ImgFarmUpgrade.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (cookies < farmBasisPrijs * 100 * farmLevel)
                {
                    ImgFarmUpgrade.Visibility = Visibility.Collapsed;
                    farmUpgradeEnabled = false;
                }
            }
        }

        private void MineUpgrade()
        {
            if (mineUpgradeEnabled == false)
            {
                if ((cookies >= mineBasisPrijs * 100 * mineLevel) && (mineAmount >= 1) && ImgMineUpgrade.Visibility != Visibility.Visible)
                {
                    ImgMineUpgrade.Visibility = Visibility.Visible;
                    ImgMineUpgrade.ToolTip = $"Upgrade mine naar x{mineLevel * 2} voor {Math.Round(mineBasisPrijs * 100 * mineLevel)} cookies";
                    mineUpgradeEnabled = true;
                }
                else
                {
                    ImgMineUpgrade.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (cookies < mineBasisPrijs * 100 * mineLevel)
                {
                    ImgMineUpgrade.Visibility = Visibility.Collapsed;
                    mineUpgradeEnabled = false;
                }
            }
        }

        private void FactoryUpgrade()
        {
            if (factoryUpgradeEnabled == false)
            {
                if ((cookies >= factoryBasisPrijs * 100 * factoryLevel) && (factoryAmount >= 1) && ImgFactoryUpgrade.Visibility != Visibility.Visible)
                {
                    ImgFactoryUpgrade.Visibility = Visibility.Visible;
                    ImgFactoryUpgrade.ToolTip = $"Upgrade factory naar x{factoryLevel * 2} voor {Math.Round(factoryBasisPrijs * 100 * factoryLevel)} cookies";
                    factoryUpgradeEnabled = true;
                }
                else
                {
                    ImgFactoryUpgrade.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (cookies < factoryBasisPrijs * 100 * factoryLevel)
                {
                    ImgFactoryUpgrade.Visibility = Visibility.Collapsed;
                    factoryUpgradeEnabled = false;
                }
            }
        }

        private void BankUpgrade()
        {
            if(bankUpgradeEnabled == false)
            {
                if ((cookies >= bankBasisPrijs * 100 * bankLevel) && (bankAmount >= 1) && ImgBankUpgrade.Visibility != Visibility.Visible)
                {
                    ImgBankUpgrade.Visibility = Visibility.Visible;
                    ImgBankUpgrade.ToolTip = $"Upgrade factory naar x{bankLevel * 2} voor {Math.Round(bankBasisPrijs * 100 * bankLevel)} cookies";
                    bankUpgradeEnabled = true;
                }
                else
                {
                    ImgBankUpgrade.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (cookies < factoryBasisPrijs * 100 * factoryLevel)
                {
                    ImgBankUpgrade.Visibility = Visibility.Collapsed;
                    bankUpgradeEnabled = false;
                }
            }
        }

        private void TempleUpgrade()
        {
            if (templeUpgradeEnabled == false)
            {
                if ((cookies >= templeBasisPrijs * 100 * templeLevel) && (templeAmount >= 1) && ImgTempleUpgrade.Visibility != Visibility.Visible)
                {
                    ImgTempleUpgrade.Visibility = Visibility.Visible;
                    ImgTempleUpgrade.ToolTip = $"Upgrade factory naar x{templeLevel * 2} voor {Math.Round(templeBasisPrijs * 100 * templeLevel)} cookies";
                    templeUpgradeEnabled = true;
                }
                else
                {
                    ImgTempleUpgrade.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (cookies < templeBasisPrijs * 100 * templeLevel)
                {
                    ImgTempleUpgrade.Visibility = Visibility.Collapsed;
                    templeUpgradeEnabled = false;
                }
            }
        }

        private void WizardUpgrade()
        {
            if (wizardUpgradeEnabled == false)
            {
                if ((cookies >= wizardBasisPrijs * 100 * wizardLevel) && (wizardAmount >= 1) && ImgWizardUpgrade.Visibility != Visibility.Visible)
                {
                    ImgWizardUpgrade.Visibility = Visibility.Visible;
                    ImgWizardUpgrade.ToolTip = $"Upgrade factory naar x{wizardLevel * 2} voor {Math.Round(wizardBasisPrijs * 100 * wizardLevel)} cookies";
                    wizardUpgradeEnabled = true;
                }
                else
                {
                    ImgWizardUpgrade.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (cookies < wizardBasisPrijs * 100 * wizardLevel)
                {
                    ImgWizardUpgrade.Visibility = Visibility.Collapsed;
                    wizardUpgradeEnabled = false;
                }
            }
        }

        //opstarten
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCookies();

            CookiesTimer.Tick += new EventHandler(TimerCookies);
            CookiesTimer.Interval = TimeSpan.FromMilliseconds(1000);
            CookiesTimer.Start();

            CheckTimer.Tick += new EventHandler(TimerCheck);
            CheckTimer.Interval = TimeSpan.FromMilliseconds(100);
            CheckTimer.Start();

            GoldenCookieTimer.Tick += new EventHandler(TimerGoldenCookie);
            GoldenCookieTimer.Interval = TimeSpan.FromMinutes(1);
            GoldenCookieTimer.Start();

            QuestTimer.Tick += new EventHandler(TimerQuest);
            QuestTimer.Interval = TimeSpan.FromMilliseconds(100);
            QuestTimer.Start();

            WrapCursor.Visibility = Visibility.Collapsed;
            WrapGrandma.Visibility = Visibility.Collapsed;
            WrapFarm.Visibility = Visibility.Collapsed;
            WrapMine.Visibility = Visibility.Collapsed;
            WrapFactory.Visibility = Visibility.Collapsed;
            WrapBank.Visibility = Visibility.Collapsed;
            WrapTemple.Visibility = Visibility.Collapsed;
            WrapWizard.Visibility = Visibility.Collapsed;

            ImgCursorUpgrade.Visibility = Visibility.Collapsed;
            ImgGrandmaUpgrade.Visibility = Visibility.Collapsed;
            ImgFarmUpgrade.Visibility = Visibility.Collapsed;
            ImgMineUpgrade.Visibility = Visibility.Collapsed;
            ImgFactoryUpgrade.Visibility = Visibility.Collapsed;
            ImgBankUpgrade.Visibility = Visibility.Collapsed;
            ImgTempleUpgrade.Visibility = Visibility.Collapsed;
            ImgWizardUpgrade.Visibility = Visibility.Collapsed;

            LblBakeryName.Content = bakeryName;
        }

        //30% kans op spawnen golden cookie
        private void TimerGoldenCookie(object sender, EventArgs e)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 100);
            if(randomNumber <= 30)
            {
                GoldenCookie();
            }
        }
		
		//Golden cookie wordt aangemaakt en krijgt random locatie
        private void GoldenCookie()
        {
            Image imgGoldenCookie = new Image();
            imgGoldenCookie.Source = new BitmapImage(new Uri("./asset/GoldenCookie.png", UriKind.Relative));
            imgGoldenCookie.Width = 50;
            imgGoldenCookie.Height = 50;
            imgGoldenCookie.Margin = new Thickness(10, 10, 10, 10);
            Canvas.SetLeft(imgGoldenCookie, random.Next(0, Convert.ToInt32(mainWindow.Width - imgGoldenCookie.Width)));
            Canvas.SetTop(imgGoldenCookie, random.Next(0, Convert.ToInt32(mainWindow.Height - imgGoldenCookie.Height)));
            grd.Children.Add(imgGoldenCookie);
            imgGoldenCookie.MouseDown += new MouseButtonEventHandler(GoldenCookie_Click);
        }
		
		//Golden cookie verdwijnt bij het klikken en speler krijgt 15 minuten aan cookies
        private void GoldenCookie_Click(object sender, MouseButtonEventArgs e)
        {
            cookies += totalPerSec * 60 * 15;
            totalEverCookies += totalPerSec * 60 * 15;
            grd.Children.Remove((Image)sender);
            totalGoldenCookies += 1;
        }

        //Image toevoegen
        private Image AddImage(string path)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            image.Width = 50;
            image.Height = 50;
            image.Margin = new Thickness(10, 10, 10, 10);
            
            return image;
        }

        //Nummers checken
        private string Nummers(double num)
        {
            double i = (double)Math.Pow(10, (int)Math.Max(0, Math.Log10(num) - 2));
            num = num / i * i;
            if (num >= 1000000000000000000)
                return (num / 1000000000000000000D).ToString("0.##") + "Quintillion";
            if (num >= 1000000000000000)
                return (num / 1000000000000000D).ToString("0.##") + "Quadrillion";
            if (num >= 1000000000000)
                return (num / 1000000000000D).ToString("0.##") + "Trillion";
            if (num >= 1000000000)
                return (num / 1000000000D).ToString("0.##") + "Billion";
            if (num >= 1000000)
                return (num / 1000000D).ToString("0.##") + "Million";
            if (num >= 1000)
                return string.Format("{0:n0}", num);

            return string.Format("{0:0.00}", num);
        }

        //Cookies updaten
        private void UpdateCookies() 
        {
            LblCookies.Content = $"{Nummers(cookies)} Cookies";
            LblCookiesPerSec.Content = $"{Nummers(totalPerSec)} cps";
            this.Title = $"Cookie Clicker - {Nummers(cookies)} Cookies";
        }

        //Bakery naam veranderen
        private void LblBakeryName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string naam = Interaction.InputBox($"Huidige naam: {bakeryName}", "Nieuwe naam voor bakkerij");
            if (naam != "")
            {
                LblBakeryName.Content = naam;
                bakeryName = naam;
            }
        }

        //Upgrades buy
        private void ImgCursorUpgrade_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cookies -= cursorBasisPrijs * 100 * cursorLevel;
            cursorLevel *= 2;
            cursorPerSec = cursorAmount * cursorValue * 100 * cursorLevel;
            LblCursorAmountSec.Content = $"{Nummers(cursorPerSec)} /sec";
            ImgCursorUpgrade.ToolTip = $"Upgrade cursor naar x{cursorLevel * 2} voor {Math.Round(cursorBasisPrijs * 100 * cursorLevel)} cookies";
        }

        private void ImgGrandmaUpgrade_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cookies -= grandmaBasisPrijs * 100 * grandmaLevel;
            grandmaLevel *= 2;
            grandmaPerSec = grandmaAmount * grandmaValue * 100 * grandmaLevel;
            LblGrandmaAmountSec.Content = $"{Nummers(grandmaPerSec)} /sec";
            ImgGrandmaUpgrade.ToolTip = $"Upgrade grandma naar x{grandmaLevel * 2} voor {Math.Round(grandmaBasisPrijs * 100 * grandmaLevel)} cookies";
        }

        private void ImgFarmUpgrade_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cookies -= farmBasisPrijs * 100 * farmLevel;
            farmLevel *= 2;
            farmPerSec = farmAmount * farmValue * 100 * farmLevel;
            LblFarmAmountSec.Content = $"{Nummers(farmPerSec)} /sec";
            ImgFarmUpgrade.ToolTip = $"Upgrade farm naar x{farmLevel * 2} voor {Math.Round(farmBasisPrijs * 100 * farmLevel)} cookies";
        }

        private void ImgMineUpgrade_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cookies -= mineBasisPrijs * 100 * mineLevel;
            mineLevel *= 2;
            minePerSec = mineAmount * mineValue * 100 * mineLevel;
            LblMineAmountSec.Content = $"{Nummers(minePerSec)} /sec";
            ImgMineUpgrade.ToolTip = $"Upgrade mine naar x{mineLevel * 2} voor {Math.Round(mineBasisPrijs * 100 * mineLevel)} cookies";
        }

        private void ImgFactoryUpgrade_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cookies -= factoryBasisPrijs * 100 * factoryLevel;
            factoryLevel *= 2;
            factoryPerSec = factoryAmount * factoryValue * 100 * factoryLevel;
            LblFactoryAmountSec.Content = $"{Nummers(factoryPerSec)} /sec";
            ImgFactoryUpgrade.ToolTip = $"Upgrade factory naar x{factoryLevel * 2} voor {Math.Round(factoryBasisPrijs * 100 * factoryLevel)} cookies";
        }

        private void ImgBankUpgrade_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cookies -= bankBasisPrijs * 100 * bankLevel;
            bankLevel *= 2;
            bankPerSec = bankAmount * bankValue * 100 * bankLevel;
            LblBankAmountSec.Content = $"{Nummers(bankPerSec)} /sec";
            ImgBankUpgrade.ToolTip = $"Upgrade bank naar x{bankLevel * 2} voor {Math.Round(bankBasisPrijs * 100 * bankLevel)} cookies";
        }

        private void ImgTempleUpgrade_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cookies -= templeBasisPrijs * 100 * templeLevel;
            templeLevel *= 2;
            templePerSec = templeAmount * templeValue * 100 * templeLevel;
            LblTempleAmountSec.Content = $"{Nummers(templePerSec)} /sec";
            ImgTempleUpgrade.ToolTip = $"Upgrade temple naar x{templeLevel * 2} voor {Math.Round(templeBasisPrijs * 100 * templeLevel)} cookies";
        }

        private void ImgWizardUpgrade_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cookies -= wizardBasisPrijs * 100 * wizardLevel;
            wizardLevel *= 2;
            wizardPerSec = wizardAmount * wizardValue * 100 * wizardLevel;
            LblWizardAmountSec.Content = $"{Nummers(wizardPerSec)} /sec";
            ImgWizardUpgrade.ToolTip = $"Upgrade wizard naar x{wizardLevel * 2} voor {Math.Round(wizardBasisPrijs * 100 * wizardLevel)} cookies";
        }
		
		//Kijkt of er een quest behaald is.
        private void TimerQuest(object sender, EventArgs e)
        {
            if(totalEverCookies == 1 && quest1 == false)
            {
                MessageBox.Show("Je hebt de quest: 'First ever cookie' behaald!", "First quest!");
                quest1 = true;
            }
            if(cursorAmount == 1 && quest2 == false)
            {
                MessageBox.Show("Je hebt de quest: 'Automatisatie' behaald!", "Second quest!");
                quest2 = true;
            }
            if(totalEverCookies >= 100 && quest3 == false)
            {
                MessageBox.Show("Je hebt de quest: '100 cookies' behaald!", "Third quest!");
                quest3 = true;
            }
            if(grandmaAmount >= 1 && quest4 == false)
            {
                MessageBox.Show("Je hebt de quest: 'Grandma' behaald!", "Fourth quest!");
                quest4 = true;
            }
            if(totalPerSec > 20 && quest5 == false)
            {
                MessageBox.Show("Je hebt de quest: '20 cookies per seconde' behaald!", "Fifth quest!");
                quest5 = true;
            }
            if(totalGoldenCookies >= 1 && quest6 == false)
            {
                MessageBox.Show("Je hebt de quest: 'First golden cookie' behaald!", "Sixth quest!");
                quest6 = true;
            }
            if(totalEverCookies >= 1000 && quest7 == false)
            {
                MessageBox.Show("Je hebt de quest: '1000 cookies' behaald!", "Seventh quest!");
                quest7 = true;
            }
            if(farmAmount >= 1 && quest8 == false)
            {
                MessageBox.Show("Je hebt de quest: 'Farm' behaald!", "Eighth quest!");
                quest8 = true;
            }
            if(totalPerSec > 100 && quest9 == false)
            {
                MessageBox.Show("Je hebt de quest: '100 cookies per seconde' behaald!", "Ninth quest!");
                quest9 = true;
            }
            if(totalGoldenCookies >= 10 && quest10 == false)
            {
                MessageBox.Show("Je hebt de quest: '10 golden cookies' behaald!", "Tenth quest!");
                quest10 = true;
            }
            if(totalEverCookies >= 100000 && quest11 == false)
            {
                MessageBox.Show("Je hebt de quest: '100k cookies' behaald!", "Eleventh quest!");
                quest11 = true;
            }
            if(mineAmount >= 1 && quest12 == false)
            {
                MessageBox.Show("Je hebt de quest: 'Mine' behaald!", "Twelfth quest!");
                quest12 = true;
            }
            if(totalPerSec > 1000 && quest13 == false)
            {
                MessageBox.Show("Je hebt de quest: '1000 cookies per seconde' behaald!", "Thirteenth quest!");
                quest13 = true;
            }
            if(totalGoldenCookies >= 20 && quest14 == false)
            {
                MessageBox.Show("Je hebt de quest: '20 golden cookies' behaald!", "Fourteenth quest!");
                quest14 = true;
            }
            if(totalEverCookies >= 1000000 && quest15 == false)
            {
                MessageBox.Show("Je hebt de quest: '1M cookies' behaald!", "Fifteenth quest!");
                quest15 = true;
            }
            if(factoryAmount >= 1 && quest16 == false)
            {
                MessageBox.Show("Je hebt de quest: 'Factory' behaald!", "Sixteenth quest!");
                quest16 = true;
            }
            if(totalPerSec > 10000 && quest17 == false)
            {
                MessageBox.Show("Je hebt de quest: '10k cookies per seconde' behaald!", "Seventeenth quest!");
                quest17 = true;
            }
            if(totalGoldenCookies >= 50 && quest18 == false)
            {
                MessageBox.Show("Je hebt de quest: '50 golden cookies' behaald!", "Eighteenth quest!");
                quest18 = true;
            }
            if(totalEverCookies >= 100000000 && quest19 == false)
            {
                MessageBox.Show("Je hebt de quest: '100M cookies' behaald!", "Nineteenth quest!");
                quest19 = true;
            }

        }
		
		//Opent 2de form met de behaalde quest list
        private void ImgQuests_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Achievements achievements = new Achievements();
            achievements.quest1 = quest1;
            achievements.quest2 = quest2;
            achievements.quest3 = quest3;
            achievements.quest4 = quest4;
            achievements.quest5 = quest5;
            achievements.quest6 = quest6;
            achievements.quest7 = quest7;
            achievements.quest8 = quest8;
            achievements.quest9 = quest9;
            achievements.quest10 = quest10;
            achievements.quest11 = quest11;
            achievements.quest12 = quest12;
            achievements.quest13 = quest13;
            achievements.quest14 = quest14;
            achievements.quest15 = quest15;
            achievements.quest16 = quest16;
            achievements.quest17 = quest17;
            achievements.quest18 = quest18;
            achievements.quest19 = quest19;
            achievements.quest20 = quest20;
            achievements.Show();
        }
    }
}
