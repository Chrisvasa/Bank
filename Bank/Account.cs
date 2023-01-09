using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Account
    {
        // REWRITE 
        /* Account class functions
         * These are something every account should have
         * > Check funds
         * >> Lets user check funds at given account
         * > Deposit funds
         * >> Lets user deposit funds into a given account
         * > Withdraw funds
         * >> Lets user withdraw funds from a given account
         * > Transfer money between accounts
         * >> Lets user transfer money between his own accounts
         * > Transfer money between users
         * >> Lets user transfer money to other users
         * -----------------------------------------------------
         * Things to think about
         * - Users/Customers have varying amounts of accounts
         * - From 2 to 6 
         * - 
         * 
         */
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
            set { accounts = value; }
        }

        public static string[] GetAccount(int user)
        {
            string[] accArray = new string[accounts[user].Length];
            for(int i = 0; i < accounts[user].Length; i++)
            {
                accArray[i] += accounts[user][i];
            }
            return accArray;
        }

        public static string[] ShowAccount(int user)
        {
            string[] userAcc = new string[accounts[user].Length + 1];
            for (int i = 0; i < accounts[user].Length; i++)
            {
                userAcc[i] = accounts[user][i];
            }
            userAcc[accounts[user].Length] = "Go back";
            return userAcc;
        }
    }
}

// Flytta över alla konton hit?
// Account Chrille = new account() etc
// Getter setter för username, password