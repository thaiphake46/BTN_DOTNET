using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;

namespace BTN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-L0KSONR;Initial Catalog=LuatLamNghiep;Integrated Security=True");

        public void loadGrid()
        {
            try
            {
                string str_query = 
                    @"select
                        dieu as 'Điều',
	                    noiDungDieu as 'Nội dung điều',
	                    khoan as 'Khoản',
	                    noiDungKhoan as 'Nội dung khoản',
	                    mucPhatDuoi as 'Mức phạt dưới',
	                    mucPhatTren as 'Mức phạt trên'
                    from
                        Nhom3";
                SqlCommand cmd = new SqlCommand(str_query, con);
                DataTable dt = new DataTable();
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
                datagrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void refreshData()
        {
            dieu_txt.Clear();
            noiDungDieu_txt.Clear();
            khoan_txt.Clear();
            noiDungKhoan_txt.Clear();
            mucPhatDuoi_txt.Clear();
            mucPhatTren_txt.Clear();
        }

        private void loadData_btn_Click(object sender, RoutedEventArgs e)
        {
            loadGrid();
        }

        private void refresh_btn_Click(object sender, RoutedEventArgs e)
        {
            refreshData();
        }

        public bool isValid()
        {
            if (dieu_txt.Text == string.Empty)
            {
                MessageBox.Show("Điều is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (noiDungDieu_txt.Text == string.Empty)
            {
                MessageBox.Show("Nội dung điều is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (khoan_txt.Text == string.Empty)
            {
                MessageBox.Show("Khoản is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (noiDungKhoan_txt.Text == string.Empty)
            {
                MessageBox.Show("Nội dung khoản is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (int.Parse(mucPhatDuoi_txt.Text) < 0)
            {
                MessageBox.Show("Mức phạt dưới is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (int.Parse(mucPhatTren_txt.Text) < 0)
            {
                MessageBox.Show("Mức phạt trên is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void timKiem_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
