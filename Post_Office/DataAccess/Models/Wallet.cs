using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Wallet
    {
        private int Balance;
        public Wallet()
        {
            Balance = 500;
        }
        public void Add_Money(int x)
        {
            if (x <= 0)
                throw new ArgumentException("The amount of money must be Positive");
            Balance += x;
        }
        public int Get_Balance()
        {
            return Balance;
        }
        public void Withdraw(int x)
        {
            Balance -= x;
        }
        public void set(int x)
        {
            Balance = x;
        }
    }
}
