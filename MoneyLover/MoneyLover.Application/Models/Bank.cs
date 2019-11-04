using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.Application.Models
{
    public class Bank
    {
        [Key]
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string ShortName { get; set; }
        public ICollection<PassBook> PassBooks { get; set; }

        public static Bank GetBank(int id)
        {
            using (var db = new DB.MoneyLoverDB())
            {
                return db.Banks.Find(id);
            }
        }
    }
}
