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
using System.Collections.ObjectModel;

namespace MTS_MKD.PageUser
{
    /// <summary>
    /// Логика взаимодействия для Engineering_Equipment_Page.xaml
    /// </summary>
    /// <DataGridTextColumn x:Name="heatingColumn" Binding="{Binding Heating}" Header="Тип отопление" Width="SizeToHeader"/>
    public partial class Engineering_Equipment_Page : Page
    {
        public Engineering_Equipment_Page()
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
            cmd.CommandText = "select * from [Engineering_Equipment]";
            cmd.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Engineering_Equipment");
            adapter.Fill(dt);

            engineering_EquipmentDataGrid.ItemsSource = dt.DefaultView;

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (engineering_EquipmentDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите действительную строку для удаления.");
                return;
            }
            if (engineering_EquipmentDataGrid.SelectedItem is DataRowView selectedRow)
            {
                int selectedIndex = engineering_EquipmentDataGrid.SelectedIndex;
                DataTable dt = ((DataView)engineering_EquipmentDataGrid.ItemsSource).Table;
                dt.Rows[selectedIndex].Delete();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [Engineering_Equipment]", conn))
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
            DataTable changes = ((DataView)engineering_EquipmentDataGrid.ItemsSource).Table.GetChanges();

            if (changes != null)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [Engineering_Equipment]", conn))
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

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = tbSearch.Text.ToLower();
            DataTable dt = ((DataView)engineering_EquipmentDataGrid.ItemsSource).Table;
            DataView dv = dt.DefaultView;
            if (!string.IsNullOrEmpty(searchText))
            {
                dv.RowFilter = $"Heating LIKE '%{searchText}%'";
            }
            else
            {
                dv.RowFilter = "";
            }
            engineering_EquipmentDataGrid.ItemsSource = dv;
        }


    }
}
