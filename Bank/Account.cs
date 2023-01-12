using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Account
    {
        protected static string[][] accounts = new string[][]
        {
            new string[] {"Private Account", "Savings"},
            new string[] {"Private Account", "Savings", "Payroll"},
            new string[] {"Private Account", "Savings", "Payroll", "Gambling"},
            new string[] {"Private Account", "Savings", "Payroll", "Gambling", "Shares & Stocks"},
            new string[] {"Private Account", "Savings", "Payroll", "Gambling", "Shares & Stocks", "TBD"}
        };

        public static string[][] Accounts
        {
            get { return accounts; }
        }

        public static string[] GetAccount(int user)
        {
            User userTest = new User();
            string test = userTest.Users[user, 2];
            int accountIndex = (int)Enum.Parse(typeof(AccountType), test) - 2;
            return accounts[accountIndex];
        }

        // Creates an account array and adds the "Go back" at the end of the array
        // Used to show different accounts as a menu
        public static string[] ShowAccount(int user)
        {
            User userTest = new User();
            string test = userTest.Users[user,2];
            int accountType = (int)Enum.Parse(typeof(AccountType), test);
            int accountIndex = accountType - 2;
            string[] userAcc = new string[accountType + 1];
            // Gets the account length at given account index, and loops through that many times
            // and every loop adds the given value to the array
            for (int i = 0; i < accounts[accountIndex].Length; i++) 
            {
                userAcc[i] = accounts[accountIndex][i];
            }
            // After the loop adds "Go back" to the end of array
            userAcc[accounts[accountIndex].Length] = "Go back";
            return userAcc;
        }
    }
}
