using System;
using System.Collections.Generic;
using System.Data;
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

namespace Book
{
    /// <summary>
    /// Логика взаимодействия для NoteBook.xaml
    /// </summary>
    public partial class NoteBook : Page
    {
        public NoteBook()
        {
            InitializeComponent();
            DataGrid();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ConString.Open();

            using (SqlCommand cmd = new SqlCommand("Insert Into Tasks (AccountId, TaskName, Description, DateCreated, DateEnd) Values (@AccountId, @TaskName, @Description, @DateCreated, @DateEnd)", MainWindow.ConString))
            {
                cmd.Parameters.Add(new SqlParameter("@AccountId", MainWindow.Id));
                cmd.Parameters.Add(new SqlParameter("@TaskName", TaskName.Text));
                cmd.Parameters.Add(new SqlParameter("@Description", TaskTip.Text));
                cmd.Parameters.Add(new SqlParameter("@DateCreated", DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                cmd.Parameters.Add(new SqlParameter("@DateEnd", TaskDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                

                cmd.ExecuteNonQuery();
                MessageBox.Show("Добавлено");
                
            }
            MainWindow.ConString.Close();
            DataGrid();
        }

        private void DataGrid()
        {
            MainWindow.ConString.Open();


            string sqlString = "SELECT Id, TaskName, Description, DateCreated, DateEnd FROM Tasks WHERE AccountId= '" + MainWindow.Id + "'";
            SqlCommand cmd = new SqlCommand(sqlString, MainWindow.ConString);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGrid.ItemsSource = dt.DefaultView;
            MainWindow.ConString.Close();

        }

        private void RemoveButon_Click(object sender, RoutedEventArgs e)
        {
            

            MainWindow.ConString.Open();
            string query = "Delete from Tasks where ID ='" + TaskRemNum.Text + "'";
            SqlCommand sqlcmd = new SqlCommand(query, MainWindow.ConString);
            sqlcmd.ExecuteNonQuery();
            MessageBox.Show("Успешно удалено.");
            MainWindow.ConString.Close();
            DataGrid();

        }

        private void DateButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ConString.Open();
            string sqlString = "SELECT * FROM Tasks WHERE DateCreated >='" + TaskDateFrom.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + "And DateEnd <='" + TaskDateTo.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            SqlCommand cmd = new SqlCommand(sqlString, MainWindow.ConString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable datat = new DataTable();
            sqlDataAdapter.Fill(datat);
            dataGrid.ItemsSource = datat.DefaultView;
            MainWindow.ConString.Close();
        }
    }
}
