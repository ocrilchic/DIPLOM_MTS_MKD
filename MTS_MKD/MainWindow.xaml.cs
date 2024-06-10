using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace MTS_MKD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-V0CD02P\SQLEXPRESS;Initial Catalog=MTS_MKD_DB;Integrated Security=True;TrustServerCertificate=True");
            connection.Open();
            string query = $"select COUNT(*) from users where login = @login and password = @password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@login", txtBlLogin.Text);
            command.Parameters.AddWithValue("@password", txtPassword.Password);
            object result = command.ExecuteScalar();
            connection.Close();
            if (result != null)
            {
                int count = Convert.ToInt32(result);
                if (count > 0)
                {
                    UserWindows f1 = new UserWindows();
                    f1.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверные данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Неверные данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
