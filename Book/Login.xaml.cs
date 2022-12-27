using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace Book
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.username = NameInput.Text;
            MainWindow.password = PasswordInput.Text;


            SqlDataAdapter SDA = new SqlDataAdapter("SELECT * FROM Users WHERE UserName= '" + MainWindow.username + "' AND Password ='" + MainWindow.password + "'", MainWindow.ConString);

            DataTable DT = new DataTable();

            SDA.Fill(DT);
            if (DT.Rows.Count > 0)
            {

                foreach (DataRow row in DT.Rows)
                {
                    MainWindow.Id = int.Parse(row["Id"].ToString());
                }
                MessageBox.Show("Успешный вход!");
                MainWindow.PageInital(new NoteBook());
            }
            else
            {
                MessageBox.Show("Ошибка");
            }

        }
    }
}
