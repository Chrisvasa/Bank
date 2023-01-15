using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bank
{
    // A class that reads from the Funds.txt, parses it and then stores it as a 2d string array
    // And writes any changes back to Funds.txt
    internal class CustomerFunds
    {
        protected decimal[][] userFunds = new decimal[0][];
        // Reads the Funds.txt and turns it into a jagged decimal array
        public CustomerFunds()
        {
            LoadFunds();
        }
        // Reads in the data from Funds.txt and places it in an array
        public void LoadFunds()
        {
            string[] funds;
            string[] customerFunds;
            // Works for powershell and on windows, hopefully works on mac
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Funds.txt")))
            {
                customerFunds = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Funds.txt"));

            }
            else if (File.Exists("../../../Funds.txt")) // Works on windows if ran from visual studio
            {
                customerFunds = File.ReadAllLines("../../../Funds.txt");
            }
            else // Hopefully this works on mac if those above does not
            {
                customerFunds = File.ReadAllLines("..\\..\\..\\Funds.txt");
            }
            userFunds = new decimal[customerFunds.Length][];
            // A loop that goes through the array from the textfile
            // Counts the amount of ';' chars per row
            // Then loops through that amount of times, and adds the parsed values to userFunds[i][j] array
            for (int i = 0; i < customerFunds.Length; i++)
            {
                int count = customerFunds[i].Split(';').Length;
                userFunds[i] = new decimal[count];
                for (int j = 0; j < count; j++)
                {
                    funds = customerFunds[i].Split(";");
                    userFunds[i][j] = decimal.Parse(funds[j]);
                }
            }
        }
        //Getter and setter for the UserFunds Jagged Array
        public decimal[][] UserFunds
        {
            get { return userFunds; }
            set { userFunds = value; }
        }
        // Access funds at a specific index
        public decimal GetFundsAt(int user, int index)
        {
            return userFunds[user][index];
        }

        public decimal[] GetUserFunds(int user)
        {
            return userFunds[user];
        }
        // Updates the current funds to Funds.txt
        public void UpdateFunds()
        {
            StringBuilder fundList = new StringBuilder();

            for(int i = 0; i < userFunds.Length; i++) // Loops through the array of funds
            { 
                for(int j = 0; j < userFunds[i].Length; j++) // Loops through the columns in the array of funds
                {
                    fundList.Append(userFunds[i][j]); // Adds each item to the stringbuilder
                    if (j >= 0 && j < userFunds[i].Length - 1) // Adds a semicolon between funds
                    {
                        fundList.Append(";");
                    }
                }
                if(i < userFunds.Length - 1) // Adds a newline at the end of a column
                {
                    fundList.Append('\n');
                }
            }
            // Works for powershell and on windows, hopefully works on mac
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Funds.txt")))
            {
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "Funds.txt"), fundList.ToString());

            }
            else if (File.Exists("../../../Funds.txt")) // Works on windows if ran from visual studio
            {
                File.WriteAllText("../../../Funds.txt", fundList.ToString());
            }
            else // Hopefully this works on mac if those above does not
            {
                File.WriteAllText("..\\..\\..\\Funds.txt", fundList.ToString());
            }
        }

    }
}
