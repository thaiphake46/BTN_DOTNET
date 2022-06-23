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
    /// Interaction logic for lienHe.xaml
    /// </summary>
    public partial class lienHe : Page
    {
        public lienHe()
        {
            InitializeComponent();
        }

        SqlConnection con =
        new SqlConnection(@"Data Source=DESKTOP-I7I50LA;Initial Catalog=LuatLamNghiep_backup;Integrated Security=True");

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public bool isValid()
        {
            if(hoten_txt.Text == String.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (email_txt.Text == String.Empty)
            {
                MessageBox.Show("Email is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (phoneNumber_txt.Text == String.Empty)
            {
                MessageBox.Show("Number Phone is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false ;
            }
            if(noiDung_txt.Text == String.Empty)
            {
                MessageBox.Show("Nội dung is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void send_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    string str = "insert into lienHe values(@hoTen, @email, @soDienThoai, @noiDung)";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@hoTen", hoten_txt.Text);
                    cmd.Parameters.AddWithValue("@email", email_txt.Text);
                    cmd.Parameters.AddWithValue("@soDienThoai", phoneNumber_txt.Text);
                    cmd.Parameters.AddWithValue("@noiDung", noiDung_txt.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
