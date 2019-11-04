using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace MoneyLover.Application.Views
{
    /// <summary>
    /// Interaction logic for Withdrawal.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private Views.Menu menu;
        public Menu(Models.PassBook pb)
        {
            InitializeComponent();
            menu.txtBankName.Text = "Ngân hàng: " + Models.Bank.GetBank(pb.BankID).BankName;
            menu.txtDeposit.Text = "Số tiền gửi: " + pb.Deposit.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN").NumberFormat) + " đ";
            menu.txtEndDate.Text = "Ngày đến hạn: " + pb.EndDate.ToString("dd/MM/yyyy");
            menu.txtPassBookID.Text = "Mã số: #" + pb.GetID;
        }

       

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
