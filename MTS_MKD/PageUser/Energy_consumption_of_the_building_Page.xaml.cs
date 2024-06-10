using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
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

namespace MTS_MKD.PageUser
{
    /// <summary>
    /// Логика взаимодействия для Energy_consumption_of_the_building_Page.xaml
    /// </summary>
    public partial class Energy_consumption_of_the_building_Page : Page
    {
        public Energy_consumption_of_the_building_Page()
        {
            InitializeComponent();
            ListHomeDG();
        }
        private void ListHomeDG()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from [Energy_consumption_of_the_building]";
            cmd.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Energy_consumption_of_the_building");
            adapter.Fill(dt);

            energy_consumption_of_the_buildingDataGrid.ItemsSource = dt.DefaultView;
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (energy_consumption_of_the_buildingDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите действительную строку для удаления.");
                return;
            }
            if (energy_consumption_of_the_buildingDataGrid.SelectedItem is DataRowView selectedRow)
            {
                int selectedIndex = energy_consumption_of_the_buildingDataGrid.SelectedIndex;
                DataTable dt = ((DataView)energy_consumption_of_the_buildingDataGrid.ItemsSource).Table;
                dt.Rows[selectedIndex].Delete();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [Energy_consumption_of_the_building]", conn))
                    {
                        using (new SqlCommandBuilder(adapter))
                        {
                            adapter.Update(dt);
                        }
                    }
                }
                ListHomeDG();
            }
            else
            {
                MessageBox.Show("Выберите действительную строку для удаления.");
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DataTable changes = ((DataView)energy_consumption_of_the_buildingDataGrid.ItemsSource).Table.GetChanges();

            if (changes != null)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [Energy_consumption_of_the_building]", conn))
                    {
                        using (new SqlCommandBuilder(adapter))
                        {
                            adapter.Update(changes);
                        }
                    }
                }
            }

            ListHomeDG();
        }
    }
}