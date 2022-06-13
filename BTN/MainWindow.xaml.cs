using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

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

        SqlConnection con =
        new SqlConnection(@"Data Source=DESKTOP-I7I50LA;Initial Catalog=LuatLamNghiep_backup;Integrated Security=True");

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

        public void loadGrid_query()
        {
            try
            {
                //string str_query =
                //    @"select
                //        dieu as 'Điều',
                //     noiDungDieu as 'Nội dung điều',
                //     khoan as 'Khoản',
                //     noiDungKhoan as 'Nội dung khoản',
                //     mucPhatDuoi as 'Mức phạt dưới',
                //     mucPhatTren as 'Mức phạt trên'
                //    from
                //        Nhom3";
                string getQuery = null;
                int check = 0;

                string[] listTenTruong = {
                    "dieu",
                    "noiDungDieu",
                    "khoan",
                    "noiDungKhoan",
                    "mucPhatDuoi",
                    "mucPhatTren"
                };

                string[] listInput = {
                    dieu_txt.Text,
                    noiDungDieu_txt.Text,
                    khoan_txt.Text,
                    noiDungKhoan_txt.Text,
                    mucPhatDuoi_txt.Text,
                    mucPhatTren_txt.Text
                };

                for(int i = 0; i < 6; i++)
                {
                    if (listInput[i] != string.Empty)
                    {
                        check++;
                    }
                }

                if (check == 1)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (listInput[i] != string.Empty)
                        {
                            if(i <= 3)
                                getQuery += $"{listTenTruong[i]} like N'%{listInput[i]}%'";
                            else
                            {
                                if(i == 4)
                                {
                                    getQuery += $"{listTenTruong[i]} > {listInput[i]}";
                                }
                                if(i == 5)
                                {
                                    getQuery += $"{listTenTruong[i]} < {listInput[i]}";
                                }
                            }
                        }
                    }
                }

                if(check > 1)
                {
                    int checkFirst = 0;
                    for(int i = 0; i < 6; i++)
                    {
                        if (listInput[i] != string.Empty) 
                        {
                            checkFirst++;
                            if(checkFirst == 1)
                            {
                                getQuery += $"{listTenTruong[i]} like N'%{listInput[i]}%' ";
                            }
                            else if(checkFirst > 1)
                            {
                                if(i >=0 || i <= 3)
                                {
                                    getQuery += $"and {listTenTruong[i]} like N'%{listInput[i]}%' ";
                                }
                                if (i >=4 && i <= 5)
                                {
                                    if(i == 4)
                                    {
                                        int mucPhatDuoi = int.Parse(listInput[i]);
                                        getQuery += $"and {listTenTruong[i]} >= {listInput[i]}";
                                    }
                                    if(i == 5)
                                    {
                                        int mucPhatTren = int.Parse(listInput[i]);
                                        getQuery += $"and {listTenTruong[i]} <= {listInput[i]}";
                                    }
                                }
                            }
                        }
                    }
                }

                string str_query =
                    $@"select
                        dieu as 'Điều',
	                    noiDungDieu as 'Nội dung điều',
	                    khoan as 'Khoản',
	                    noiDungKhoan as 'Nội dung khoản',
	                    mucPhatDuoi as 'Mức phạt dưới',
	                    mucPhatTren as 'Mức phạt trên'
                    from
                        Nhom3
                    where
                        {getQuery}";
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

        private void timKiem_btn_Click(object sender, RoutedEventArgs e)
        {
            loadGrid_query();
        }
    }
}
