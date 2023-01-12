using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class AccountCreator
    {
        User userList = new User();
        private string userName = "";
        private string pincode = "";
        private string accType = "";
        
        public void CreateCustomer()
        {
            Menu creatorMenu = new Menu("Set Username", "Set Pincode", "Set Account Type", "Go back");
            bool isTrue = true;

            creatorMenu.PrintSystem();
            do
            {
                int index = creatorMenu.UseMenu();
                switch(index)
                {
                    case 0:
                        SetName();
                        break;
                    case 1:
                        SetPin();
                        break;
                    case 2:
                        SetAccountType();
                        break;
                    case 3:
                        UpdateUserList();
                        isTrue = false;
                        break;
                }
                creatorMenu.PrintSystem();
            } while (isTrue);
        }

        private void SetName()
        {
            Console.Clear();
            Console.Write("Enter username:");
            this.userName = Console.ReadLine();
        }

        private void SetPin()
        {
            Console.Clear();
            Console.Write("Enter username:");
            this.pincode = Console.ReadLine();
        }

        private void SetAccountType()
        {
            Menu accountMenu = new Menu("Free", "Basic", "Business", "Business Premium", "Exclusive", "Go back");
            bool isTrue = true;
            accountMenu.PrintSystem();
            do
            {
                int index = accountMenu.UseMenu();
                switch(index)
                {
                    case 0:
                        this.accType = " Free";
                        break;
                    case 1:
                        this.accType = " Basic";
                        break;
                    case 2:
                        this.accType = " Business";
                        break;
                    case 3:
                        this.accType = " BusinessPremium";
                        break;
                    case 4:
                        this.accType = " Exclusive";
                        break;
                    case 5:
                        break;
                }
                isTrue = false;
            } while (isTrue);
        }
        /* Takes the User and Funds arrays and 
         * creates temporary new ones with the old array size + 1. 
         * ------------------------------------------------------
         * Fills the temporary array with the old values 
         * and adds the newly created account at the end
         * -----------------------------------------------------
         * Then replaces the old array with the newly created one
         * And then updates the text files with the new values
         */
        private void UpdateUserList()
        {
            //The logic that handles the Users.txt
            int size = userList.Users.Length / 3;
            string[,] tempUserList = new string[size + 1, 3];
            for(int i = 0; i < userList.Users.Length / 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    tempUserList[i, j] = userList.Users[i, j];
                }
            }
            // Adds the newly created account to the end of the array
            tempUserList[size, 0] = userName.ToUpper();
            tempUserList[size, 1] = pincode;
            tempUserList[size, 2] = accType; // The account type 
            userList.Users = tempUserList;
            userList.UpdateList();
            // The logic that handles Funds.txt
            CustomerFunds fundList = new CustomerFunds();
            int accountType = (int)Enum.Parse(typeof(AccountType), accType); // Gets the amount of accounts that the specified account type has
            int listLength = fundList.UserFunds.Length + 1;
            decimal[][] listedFunds = fundList.UserFunds;
            decimal[][] tempFundList = new decimal[listLength][];
            // Loops through the listLength
            for(int i = 0; i < listLength; i++)
            {
                // If at the end of array adds the newly created accounts funds to the array
                // Which are set to 0
                if (i == listLength - 1) 
                {
                    tempFundList[i] = new decimal[accountType];
                    for (int j = 0; j < accountType; j++)
                    {
                        tempFundList[i][j] = 0;
                    }
                }
                else
                {
                    //Fills the temporary array with the current arrays values
                    tempFundList[i] = new decimal[listedFunds[i].Length];
                    for (int h = 0; h < listedFunds[i].Length; h++)
                    {
                        tempFundList[i][h] = fundList.UserFunds[i][h];
                    }
                }
            }
            fundList.UserFunds = tempFundList;
            fundList.UpdateFunds();
        }

            
    }
}
