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
    /// Логика взаимодействия для Characteristics_of_Residential_Premises_Page.xaml
    /// </summary>
    public partial class Characteristics_of_Residential_Premises_Page : Page
    {
        public Characteristics_of_Residential_Premises_Page()
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
            cmd.CommandText = "select * from [Characteristics_of_Residential_Premises]";
            cmd.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Characteristics_of_Residential_Premises");
            adapter.Fill(dt);

            characteristics_of_Residential_PremisesDataGrid.ItemsSource = dt.DefaultView;
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (characteristics_of_Residential_PremisesDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите действительную строку для удаления.");
                return;
            }
            if (characteristics_of_Residential_PremisesDataGrid.SelectedItem is DataRowView selectedRow)
            {
                int selectedIndex = characteristics_of_Residential_PremisesDataGrid.SelectedIndex;
                DataTable dt = ((DataView)characteristics_of_Residential_PremisesDataGrid.ItemsSource).Table;
                dt.Rows[selectedIndex].Delete();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [Characteristics_of_Residential_Premises]", conn))
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
            DataTable changes = ((DataView)characteristics_of_Residential_PremisesDataGrid.ItemsSource).Table.GetChanges();

            if (changes != null)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [Characteristics_of_Residential_Premises]", conn))
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