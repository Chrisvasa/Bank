using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class CustomerFunds
    {
        protected decimal[][] userFunds = new decimal[0][];

        public CustomerFunds()
        {
            userFunds = new decimal[][]
            {
            new decimal[] {2500, 500},
            new decimal[] {6000, 500, 2700},
            new decimal[] {13000, 250, 444, 9370},
            new decimal[] {12750, 2440, 3, 1780,5, 23000},
            new decimal[] {125523, 99887, 78787, 45454, 3333, 25}
            };
        }

        public decimal[][] UserFunds
        {
            get { return userFunds; }
            set { userFunds = value; }
        }

        public decimal GetFundsAt(int user, int index)
        {
            return userFunds[user][index];
        }
    }
}
