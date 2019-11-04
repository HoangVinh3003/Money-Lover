using MoneyLover.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MoneyLover.Application.Views
{
    /// <summary>
    /// Interaction logic for xaml
    /// </summary>
    public partial class PassBook : Window
    {
        private User current_user;
        private PassbookList passbookList;

        private PassBookService passBookService;

        private Dictionary<int, string> term = new Dictionary<int, string>
            {
                { 99, "Không thời hạn" },
                { 1, "1 tháng" },
                { 3, "3 tháng" },
                { 6, "6 tháng" },
                { 12, "12 tháng" },
            };

        private Dictionary<int, string> payInterest = new Dictionary<int, string>
            {
                { 1, "Cuối kỳ" },
                { 2, "Đầu kỳ" },
                { 3, "Định kỳ hằng tháng" }
            };

        private Dictionary<int, string> due = new Dictionary<int, string>
            {
                { 1, "Tái tục gốc và lãi" },
                { 2, "Tái tục gốc" },
                { 3, "Tất toán sổ" }
            };
        public PassBook()
        {
            InitializeComponent();
            using (var db = new DB.MoneyLoverDB())
            {
                passBookService = new PassBookService();
                current_user = db.Users.Find(System.Windows.Application.Current.Resources["current_user_id"]);

                cbbBank.ItemsSource = db.Banks.ToList();
                cbbBank.SelectedValuePath = "BankID";
                cbbBank.DisplayMemberPath = "BankName";
                cbbBank.SelectedIndex = 0;

                cbbTerm.ItemsSource = term;
                cbbTerm.SelectedValuePath = "Keys";
                cbbTerm.DisplayMemberPath = "Value";
                cbbTerm.SelectedIndex = 1;

                cbbPayInterest.ItemsSource = payInterest;
                cbbPayInterest.SelectedValuePath = "Keys";
                cbbPayInterest.DisplayMemberPath = "Value";
                cbbPayInterest.SelectedIndex = 0;

                cbbDue.ItemsSource = due;
                cbbDue.SelectedValuePath = "Keys";
                cbbDue.DisplayMemberPath = "Value";
                cbbDue.SelectedIndex = 0;
            }

        }
        public double ConvertStringToDouble(string value)
        {
            var allowedChars = "01234567890";
            string result = new string(value.Where(c => allowedChars.Contains(c)).ToArray());
            return Convert.ToDouble(result);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            double deposit = ConvertStringToDouble(txtDeposit.Text);
            int BankID = Convert.ToInt32(cbbBank.SelectedValue);
            int TermKey = Convert.ToInt32(((KeyValuePair<int, string>)cbbTerm.SelectedItem).Key);
            int payInterestKey = Convert.ToInt32(((KeyValuePair<int, string>)cbbPayInterest.SelectedItem).Key);
            int dueKey = Convert.ToInt32(((KeyValuePair<int, string>)cbbDue.SelectedItem).Key);
            Models.Bank Bank = Models.Bank.GetBank(BankID);

            if (IsDateBeforeOrToday(dpDate.Text) && ValidateDeposit(current_user.UserID, deposit))
            {
                Models.PassBook pb = passBookService.Create(BankID,
                                 Convert.ToDouble(txtDeposit.Text),
                                 dueKey,
                                 GetIndefiniteTerm(txtIndefiniteTerm.Text),
                                 TermKey,
                                 payInterestKey,
                                 DateTime.Parse(dpDate.Text),
                                 current_user.UserID,
                                 Convert.ToDouble(txtInterestRates.Text));
                Close();

            }

        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region validate
        public double GetIndefiniteTerm(string IndefiniteTerm)
        {
            try
            {
                return Convert.ToDouble(IndefiniteTerm);
            }
            catch
            {
                return 0.05;
            }
        }
        
        public bool IsDateBeforeOrToday(string input)
        {
            DateTime pDate = DateTime.Parse(input);
            //if (pDate == null)
            //{
            //    MessageBox.Show("Hãy chọn ngày gửi", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //else
            if (pDate <= DateTime.Now)
                return true;
            else
            {
                MessageBox.Show("Ngày gửi bé hơn hoặc bằng ngày hiện tại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool ValidateDeposit(int UserID, double deposit)
        {
            using (var db = new DB.MoneyLoverDB())
            {
                Models.User user = db.Users.Find(UserID);
                if (deposit < 1000000)
                {
                    MessageBox.Show("Số tiền tối thiểu là 1.000.000đ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (deposit > user.Wallet)
                {
                    MessageBox.Show("Tiền gửi phải nhỏ hơn hoặc bằng số tiền hiện có", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else return true;
            }
        }
        #endregion

        
    }




}
