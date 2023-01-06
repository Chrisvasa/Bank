using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Account
    {
        protected static string[][] accounts = new string[][]
        {
            new string[] {"Privatkonto", "Sparkonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto", "Aktiekonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto", "Aktiekonto", "Matkonto"}
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
            userAcc[accounts[user].Length] = "Gå tillbaka";
            return userAcc;
        }
    }
}

// Flytta över alla konton hit?
// Account Chrille = new account() etc
// Getter setter för username, password