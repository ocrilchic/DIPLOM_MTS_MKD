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
using System.ComponentModel;

namespace MTS_MKD.PageUser
{
    /// <summary>
    /// Логика взаимодействия для General_Information_About_MKD_Page.xaml
    /// </summary>
    public partial class General_Information_About_MKD_Page : Page
    {
        public General_Information_About_MKD_Page()
        {
            InitializeComponent();
            ListHomeDG();
            FillComboBoxFromDatabase();

        }

        private void ListHomeDG() //подлючение к бд
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from [General_Information_About_MKD]";
            cmd.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("General_Information_About_MKD");
            adapter.Fill(dt);

            general_Information_About_MKDDataGrid.ItemsSource = dt.DefaultView;
            
        }
        
        private DataTable allData; //для хранения данных

        private void FillComboBoxFromDatabase() //для сортировки
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM [General_Information_About_MKD]";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                allData = new DataTable("General_Information_About_MKD");
                adapter.Fill(allData);

                sortComboBox.Items.Clear();

                // Заполнение ComboBox
                foreach (DataRow row in allData.Rows)
                {
                    sortComboBox.Items.Add(row["Full_Name_of_the_Management_Company"].ToString());
                }
            }
        }
        //кнопка удаления
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            //проверка на пустую строку
            if (general_Information_About_MKDDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите действительную строку для удаления.");
                return;
            }
            if (general_Information_About_MKDDataGrid.SelectedItem is DataRowView selectedRow)
            {
                int selectedIndex = general_Information_About_MKDDataGrid.SelectedIndex;
                DataTable dt = ((DataView)general_Information_About_MKDDataGrid.ItemsSource).Table;
                dt.Rows[selectedIndex].Delete();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [General_Information_About_MKD]", conn))
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
        //кнопка сохранения
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DataTable changes = ((DataView)general_Information_About_MKDDataGrid.ItemsSource).Table.GetChanges();

            if (changes != null)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [General_Information_About_MKD]", conn))
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
        //TextBox для поиска
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = tbSearch.Text.ToLower();
            DataTable dt = ((DataView)general_Information_About_MKDDataGrid.ItemsSource).Table;
            DataView dv = dt.DefaultView;
            if(!string.IsNullOrEmpty(searchText) )
            {
                dv.RowFilter = $"District LIKE '%{searchText}%' OR Street LIKE '%{searchText}%'";
            }
            else
            {
                dv.RowFilter = "";
            }
            general_Information_About_MKDDataGrid.ItemsSource = dv;
        }
        //ComboBox для сортировки
        private void sortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sortComboBox.SelectedItem != null)
            {
                string selectCompany = sortComboBox.SelectedItem.ToString();

                if (selectCompany != "")
                {
                    DataRow[] filteredRows = allData.Select($"Full_Name_of_the_Management_Company = '{selectCompany}'");

                    DataTable filteredData = allData.Clone();
                    foreach (DataRow row in filteredRows)
                    {
                        filteredData.ImportRow(row);
                    }

                    general_Information_About_MKDDataGrid.ItemsSource = filteredData.DefaultView;
                }
                else
                {
                    MessageBox.Show("Выберите компанию для сортировки.");
                }
            }
        }
    }
}
