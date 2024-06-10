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
    /// Логика взаимодействия для Average_daily_expenses_Page.xaml
    /// </summary>
    public partial class Average_daily_expenses_Page : Page
    {
        public Average_daily_expenses_Page()
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
            cmd.CommandText = "select * from [Average_daily_expenses]";
            cmd.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Average_daily_expenses");
            adapter.Fill(dt);

            average_daily_expensesDataGrid.ItemsSource = dt.DefaultView;
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (average_daily_expensesDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите действительную строку для удаления.");
                return;
            }
            if (average_daily_expensesDataGrid.SelectedItem is DataRowView selectedRow)
            {
                int selectedIndex = average_daily_expensesDataGrid.SelectedIndex;
                DataTable dt = ((DataView)average_daily_expensesDataGrid.ItemsSource).Table;
                dt.Rows[selectedIndex].Delete();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [Average_daily_expenses]", conn))
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
            DataTable changes = ((DataView)average_daily_expensesDataGrid.ItemsSource).Table.GetChanges();

            if (changes != null)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MTS_MKD.Properties.Settings.MTS_MKD_DBConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [Average_daily_expenses]", conn))
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
