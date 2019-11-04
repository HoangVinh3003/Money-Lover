using MoneyLover.Application.DB;
using MoneyLover.Application.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoneyLover.Application.Views
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        private MoneyLoverDB db = new MoneyLoverDB();
        private SignIn signIn;
        public SignIn()
        {
            InitializeComponent();
        }

       
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text.Length == 0)
            {
                MessageBox.Show("Enter an email");
                txtEmail.Focus();
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Enter a valid email");
                txtEmail.Select(0, txtEmail.Text.Length);
                txtEmail.Focus();
            }
            else if (psdPassword.Password.Length == 0)
            {
                MessageBox.Show("Enter an Password!");
                psdPassword.Focus();
            }
            //else
            //{
            //    SqlDataAdapter adapter = new SqlDataAdapter();
            //    adapter.SelectCommand = cmd;
            //    DataSet dataSet = new DataSet();
            //    adapter.Fill(dataSet);
            //}
            if (Login(txtEmail.Text, psdPassword.Password))
            {
                this.Close();
                PassbookList passBookList = new PassbookList();
                passBookList.Show();
            }
            else
                MessageBox.Show("Tài khoản sai thông tin đăng nhập !!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public bool Login(string email, string password)
        {
            User checkUser = db.Users.Where(m => m.Email == email).FirstOrDefault();
            if (checkUser != null && checkUser.Password == password)
            {
                StoreUser(checkUser);
                return true;
            }
            else return false;
        }
        private void StoreUser(User user)
        {
            System.Windows.Application.Current.Resources["current_user_id"] = user.UserID;
            System.Windows.Application.Current.Resources["user_signed_in"] = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
