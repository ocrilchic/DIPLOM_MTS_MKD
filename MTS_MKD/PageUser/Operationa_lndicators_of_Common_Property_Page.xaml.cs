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
    /// Логика взаимодействия для Operationa_lndicators_of_Common_Property_Page.xaml
    /// </summary>
    public partial class Operationa_lndicators_of_Common_Property_Page : Page
    {
        public Operationa_lndicators_of_Common_Property_Page()
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
            cmd.CommandText = "select * from [Operationa_lndicators_of_Common_Property]";
            cmd.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Operationa_lndicators_of_Common_Property");
            adapter.Fill(dt);

            operationa_lndicators_of_Common_PropertyDataGrid.ItemsSource = dt.DefaultView;
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (operationa_lndicators_of_Common_PropertyDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите действительную строку для удаления.");
                return;
            }
            if (operationa_lndicators_of_Common_PropertyDataGrid.SelectedItem is DataRowView selectedRow)
            {
                int selectedIndex = operationa_lndicators_of_Common_PropertyDataGrid.SelectedIndex;
                DataTable dt = ((DataView)operationa_lndicators_of_Common_PropertyDataGrid.ItemsSource).Table;
                dt.Rows[selectedIndex].Delete();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [Operationa_lndicators_of_Common_Property]", conn))
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
            DataTable changes = ((DataView)operationa_lndicators_of_Common_PropertyDataGrid.ItemsSource).Table.GetChanges();

            if (changes != null)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [Operationa_lndicators_of_Common_Property]", conn))
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
