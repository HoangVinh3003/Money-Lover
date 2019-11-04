using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.Application.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Wallet { get; set; }
        public double SavingsWallet { get; set; }

        public static User GetUser(int UserID)
        {
            using (var db = new DB.MoneyLoverDB())
            {
                return db.Users.Find(UserID);
            }
        }
    }
}
