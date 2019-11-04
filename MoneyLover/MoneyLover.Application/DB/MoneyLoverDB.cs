using MoneyLover.Application.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.Application.DB
{
    public class MoneyLoverDB : DbContext
    {
        public MoneyLoverDB() : base("MoneyLoverDB")
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<PassBook> PassBooks { get; set; }
    }
}
