using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class AccountCreator
    {
        private string userName = "";
        private string pincode = "";
        private string selectedAccountType = "";
        
        public void CreateCustomer()
        {
            Menu AccountCreatorMenu = new Menu(new string[] { "Set Username", "Set Pincode", "Set Account Type", "Create the account", "Go back" });
            bool IsRunning = true;

            AccountCreatorMenu.PrintSystem();
            do
            {
                int index = AccountCreatorMenu.UseMenu();
                switch(index)
                {
                    case 0:
                        SetUsername();
                        break;
                    case 1:
                        SetPincode();
                        break;
                    case 2:
                        SetAccountType();
                        break;
                    case 3:
                        CreateAccount();
                        break;
                    case 4:
                        IsRunning = false;
                        break;
                }
                AccountCreatorMenu.PrintSystem();
            } while (IsRunning);
        }

        private void SetUsername()
        {
            while (String.IsNullOrEmpty(userName))
            {
                Console.Clear();
                Console.Write("Enter Username:");
                userName = Console.ReadLine();
            };
        }

        private void SetPincode()
        {
            while (String.IsNullOrEmpty(pincode))
            {
                Console.Clear();
                Console.Write("Enter Pincode:");
                pincode = Console.ReadLine();
            };
        }

        private void SetAccountType()
        {
            Menu accountMenu = new Menu(new string[] { "Free", "Basic", "Business", "Business Premium", "Exclusive", "Go back" });
            accountMenu.PrintSystem();
            while (String.IsNullOrEmpty(selectedAccountType))
            {
                int index = accountMenu.UseMenu();
                switch(index)
                {
                    case 0:
                        selectedAccountType = " Free";
                        break;
                    case 1:
                        selectedAccountType = " Basic";
                        break;
                    case 2:
                        selectedAccountType = " Business";
                        break;
                    case 3:
                        selectedAccountType = " BusinessPremium";
                        break;
                    case 4:
                        selectedAccountType = " Exclusive";
                        break;
                    case 5:
                        Console.WriteLine("Please select an account type before exiting");
                        break;
                }
            };
        }

        private void CreateAccount()
        {
            if(!String.IsNullOrWhiteSpace(userName) || !String.IsNullOrWhiteSpace(pincode) || !String.IsNullOrWhiteSpace(selectedAccountType))
            {
                Console.Clear();
                Console.WriteLine("Are you sure you want to create your account?");
                Console.WriteLine("Double check your input:");
                Console.WriteLine("Username: {0}\nPincode: {1}\nAccount Type:{2}", userName, pincode, selectedAccountType);
                Console.WriteLine("Press [Y] if you want to confirm account creation. Otherwise press [N]");
                Char answer = Console.ReadKey(true).KeyChar;

                if (answer == 'Y' || answer == 'y')
                {
                    UpdateUserList();
                    Console.WriteLine("Your account was successfully created. Press any key to continue.");
                    Console.ReadKey(true);
                    userName = "";
                    pincode = "";
                    selectedAccountType = "";

                }
            }
            else
            {
                //Console.WriteLine("Please enter some data to create an account.");
                Console.WriteLine("Make sure you have not missed to fill in any data.");
                Console.ReadKey();
            }
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
            User UserList = new User();
            int size = UserList.Users.Length / 3;
            string[,] tempUserList = new string[size + 1, 3];
            for(int i = 0; i < UserList.Users.Length / 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    tempUserList[i, j] = UserList.Users[i, j];
                }
            }
            // Adds the newly created account to the end of the array
            tempUserList[size, 0] = userName.ToUpper();
            tempUserList[size, 1] = pincode;
            tempUserList[size, 2] = selectedAccountType; // The account type 
            UserList.Users = tempUserList;
            UserList.UpdateList();
            // The logic that handles Funds.txt
            CustomerFunds FundList = new CustomerFunds();
            int accountType = (int)Enum.Parse(typeof(AccountType), selectedAccountType); // Gets the amount of accounts that the specified account type has
            int listLength = FundList.UserFunds.Length + 1;
            decimal[][] listedFunds = FundList.UserFunds;
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
                        tempFundList[i][h] = FundList.UserFunds[i][h];
                    }
                }
            }
            FundList.UserFunds = tempFundList;
            FundList.UpdateFunds();
        }

            
    }
}
