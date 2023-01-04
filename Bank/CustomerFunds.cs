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
        // Reads the Funds.txt and turns it into a jagged decimal array
        public CustomerFunds()
        {
            string[] test;
            string[] userArr = File.ReadAllLines(@"C:\Users\Chris\Desktop\Bank\Bank\Funds.txt");
            userFunds = new decimal[userArr.Count()][];

            for (int i = 0; i < userArr.Count(); i++)
            {
                int count = userArr[i].Split(';').Length;
                userFunds[i] = new decimal[count];
                for (int j = 0; j < count; j++)
                {
                    test = userArr[i].Split(";");
                    userFunds[i][j] = decimal.Parse(test[j]);
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
        // Updates the current funds to Funds.txt
        public void UpdateFunds()
        {
            StringBuilder fundList = new StringBuilder();

            for(int i = 0; i < userFunds.Length; i++) 
            { 
                for(int j = 0; j < userFunds[i].Length; j++)
                {
                    fundList.Append(userFunds[i][j]);
                    if(j >= 0 && j < userFunds[i].Length - 1)
                    {
                        fundList.Append(";");
                    }
                }
                if(i< userFunds.Length - 1)
                {
                    fundList.Append('\n');
                }
            }
            File.WriteAllText(@"C:\Users\Chris\Desktop\Bank\Bank\Funds.txt", fundList.ToString());
        }

    }
}
