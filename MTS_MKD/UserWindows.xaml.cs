using MTS_MKD.PageUser;
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

namespace MTS_MKD
{
    /// <summary>
    /// Логика взаимодействия для UserWindows.xaml
    /// </summary>
    public partial class UserWindows : Window
    {
        public UserWindows()
        {
            InitializeComponent();
        }
        private Page GetPageFromMenuSelection(string tag)
        {
            switch (tag)
            {
                case "Page1":
                    return new General_Information_About_MKD_Page();
                case "Page2":
                    return new Characteristics_of_Residential_Premises_Page();
                case "Page3":
                    return new Overhaul_Information_MKD_Page();
                case "Page4":
                    return new Operationa_lndicators_of_Common_Property_Page();
                case "Page5":
                    return new Engineering_Equipment_Page();
                case "Page6":
                    return new Temperature_conditions_Page();
                case "Page7":
                    return new Energy_consumption_of_the_building_Page();
                case "Page8":
                    return new Average_daily_expenses_Page();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void btnGeneral_Information_About_MKD_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is string tag)
            {
                var pageNavigator = GetPageFromMenuSelection(tag);
                MainFrame.Navigate(pageNavigator);
            }
        }

        private void btnCharacteristics_of_Residential_Premises_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is string tag)
            {
                var pageNavigator = GetPageFromMenuSelection(tag);
                MainFrame.Navigate(pageNavigator);
            }
        }

        private void btnOverhaul_Information_MKD_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is string tag)
            {
                var pageNavigator = GetPageFromMenuSelection(tag);
                MainFrame.Navigate(pageNavigator);
            }
        }

        private void btnOperationa_lndicators_of_Common_Property_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is string tag)
            {
                var pageNavigator = GetPageFromMenuSelection(tag);
                MainFrame.Navigate(pageNavigator);
            }
        }

        private void btnEngineeringEquipment_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is string tag)
            {
                var pageNavigator = GetPageFromMenuSelection(tag);
                MainFrame.Navigate(pageNavigator);
            }
        }

        private void btnTemperature_conditions_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is string tag)
            {
                var pageNavigator = GetPageFromMenuSelection(tag);
                MainFrame.Navigate(pageNavigator);
            }
        }

        private void btnEnergy_consumption_of_the_building_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is string tag)
            {
                var pageNavigator = GetPageFromMenuSelection(tag);
                MainFrame.Navigate(pageNavigator);
            }
        }

        private void btnAverage_daily_expenses_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is string tag)
            {
                var pageNavigator = GetPageFromMenuSelection(tag);
                MainFrame.Navigate(pageNavigator);
            }
        }

        private void btnAutoriz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow f1 = new MainWindow();
            f1.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
