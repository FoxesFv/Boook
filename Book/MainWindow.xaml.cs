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


namespace Book
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static string username;
        internal static string password;
        internal static int Id;
        internal static SqlConnection ConString = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Desktop\4 курс\Производственная практика\Работа\Book\Database1.mdf;Integrated Security=True");
        public static MainWindow Instance;




        public MainWindow()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeComponent();
                PageInital(new Login());
            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }



        }




        public static void PageInital(Page page)
        {
            Instance.LoadFrame.Content = page;
        }
    }
}
