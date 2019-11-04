using MoneyLover.Application.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace MoneyLover.Application.Views
{
    /// <summary>
    /// Interaction logic for PassbookList.xaml
    /// </summary>
    public partial class PassbookList : Window
    {
        private PassbookList passBookList;
        private Models.PassBook lastSelectedItem;
        private int UserID = Convert.ToInt32(System.Windows.Application.Current.Resources["current_user_id"]);
        public PassbookList()
        {
            InitializeComponent();
            using (var db = new MoneyLoverDB())
            {
                AllPassbook.Items.Refresh();
                AllPassbook.ItemsSource = db.PassBooks.ToList().Where(x => x.Settlement == false).ToList();
                dtgridWithdrawal.ItemsSource = db.PassBooks.Where(x => x.Settlement == true).ToList();
                //Combobox User
            }
            LoadTotalMoneyPassBook();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnAddPassBook_Click(object sender, RoutedEventArgs e)
        {
            PassBook passBook = new PassBook();
            passBook.ShowDialog();
        }

        private void AllPassbook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           
        }

        public void LoadTotalMoneyPassBook()
        {
            txtTotalMoneyPassBook.Text = "Tổng tiền: " + Models.PassBook.TotalMoneyPassBook(UserID, false).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN").NumberFormat) + " đ (" + Models.PassBook.CountPassBook(UserID, false).ToString() + " sổ)";
        }
        private void Dtgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dtgrid = (DataGrid)sender;
            lastSelectedItem = (Models.PassBook)dtgrid.SelectedItem;
        }

        private void btnEditPassBook_Click(object sender, RoutedEventArgs e)
        {
            EditPassBook editPassBook = new EditPassBook();
            editPassBook.ShowDialog();
        }

        private void btnAddMore_Click(object sender, RoutedEventArgs e)
        {
            AddToPassBook addToPassBook = new AddToPassBook();
            addToPassBook.ShowDialog();
        }

        private void btnSettlement_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnWithDrawal_Click(object sender, RoutedEventArgs e)
        {
            //Menu menu = new Menu();
            //menu.ShowDialog();
            Withdrawal withdrawal = new Withdrawal();
            withdrawal.ShowDialog();
        }

        private void btnViewPassBook_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MoneyLoverDB())
            {
                AllPassbook.Items.Refresh();
                AllPassbook.ItemsSource = db.PassBooks.ToList().Where(x => x.Settlement == false).ToList();
                dtgridWithdrawal.ItemsSource = db.PassBooks.Where(x => x.Settlement == true).ToList();

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
