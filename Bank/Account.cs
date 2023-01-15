using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public static class Account
    {
        private static string[][] accounts = new string[][]
        {
            new string[] {"Private Account", "Savings"},
            new string[] {"Private Account", "Savings", "Payroll"},
            new string[] {"Private Account", "Savings", "Payroll", "Gambling"},
            new string[] {"Private Account", "Savings", "Payroll", "Gambling", "Shares & Stocks"},
            new string[] {"Private Account", "Savings", "Payroll", "Gambling", "Shares & Stocks", "Vacation"}
        };

        // Takes the user index and returns the corresponding string-array of accounts and returns it
        public static string[] GetAccount(int user)
        {
            string userAccountType = GetAccountType(user);
            int accountIndex = (int)Enum.Parse(typeof(AccountType), userAccountType) - 2; // Gets the row with users accounts from the accounts jagged array
            return accounts[accountIndex];
        }

        // Creates an account array and adds the "Go back" at the end of the array
        // Used to show different accounts as a menu
        public static string[] ShowAccount(int user)
        {
            string userAccountType = GetAccountType(user);
            int accountType = (int)Enum.Parse(typeof(AccountType), userAccountType);
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

        private static string GetAccountType(int user)
        {
            User userCheck = new User();
            string userAccount = userCheck.Users[user, 2];
            return userAccount;
        }
    }
}
