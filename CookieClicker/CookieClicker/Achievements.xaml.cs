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

namespace CookieClicker
{
    /// <summary>
    /// Datum: 07/01/2024
	/// Auteur: Senne Martens
	/// Beschrijving: Extra form voor alle behaalde achievements aan te duiden.
    /// </summary>
    public partial class Achievements : Window
    {
        public Achievements()
        {
            InitializeComponent();
        }

        public bool quest1;
        public bool quest2;
        public bool quest3;
        public bool quest4;
        public bool quest5;
        public bool quest6;
        public bool quest7;
        public bool quest8;
        public bool quest9;
        public bool quest10;
        public bool quest11;
        public bool quest12;
        public bool quest13;
        public bool quest14;
        public bool quest15;
        public bool quest16;
        public bool quest17;
        public bool quest18;
        public bool quest19;
        public bool quest20;
		
		//Behaalde achievements worden in de listbox toegevoegd.
        private void Achievements1_Loaded(object sender, RoutedEventArgs e)
        {
            if (quest1 == true)
            {
                LstAchievements.Items.Add("First ever cookie");
            }
            if (quest2 == true)
            {
                LstAchievements.Items.Add("Automatisatie");
            }
            if (quest3 == true)
            {
                LstAchievements.Items.Add("100 cookies");
            }
            if (quest4 == true)
            {
                LstAchievements.Items.Add("Grandma");
            }
            if (quest5 == true)
            {
                LstAchievements.Items.Add("20 cookies per seconde");
            }
            if (quest6 == true)
            {
                LstAchievements.Items.Add("First golden cookie");
            }
            if (quest7 == true)
            {
                LstAchievements.Items.Add("1000 cookies");
            }
            if (quest8 == true)
            {
                LstAchievements.Items.Add("Farm");
            }
            if (quest9 == true)
            {
                LstAchievements.Items.Add("100 cookies per seconde");
            }
            if (quest10 == true)
            {
                LstAchievements.Items.Add("10 golden cookies");
            }
            if (quest11 == true)
            {
                LstAchievements.Items.Add("100k cookies");
            }
            if (quest12 == true)
            {
                LstAchievements.Items.Add("Mine");
            }
            if (quest13 == true)
            {
                LstAchievements.Items.Add("1000 cookies per seconde");
            }
            if (quest14 == true)
            {
                LstAchievements.Items.Add("20 golden cookies");
            }
            if (quest15 == true)
            {
                LstAchievements.Items.Add("1M cookies");
            }
            if (quest16 == true)
            {
                LstAchievements.Items.Add("Factory");
            }
            if (quest17 == true)
            {
                LstAchievements.Items.Add("10k cookies per seconde");
            }
            if (quest18 == true)
            {
                LstAchievements.Items.Add("50 golden cookies");
            }
            if (quest19 == true)
            {
                LstAchievements.Items.Add("100M cookies");
            }
            if (quest20 == true)
            {
                LstAchievements.Items.Add("Mine");
            }

            if(LstAchievements.Items.Count == 0)
            {
                MessageBox.Show("You have no achievements yet!");
                this.Close();
            }
        }
    }
}
