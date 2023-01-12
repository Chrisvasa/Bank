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

        private void UpdateUserList()
        {
            CustomerFunds fundList = new CustomerFunds();
            int size = userList.Users.Length / 3;
            string[,] tempUserList = new string[size + 1, 3];
            for(int i = 0; i < userList.Users.Length / 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    tempUserList[i, j] = userList.Users[i, j];
                }
            }
            tempUserList[size, 0] = userName.ToUpper();
            tempUserList[size, 1] = pincode;
            tempUserList[size, 2] = accType;
            userList.Users = tempUserList;
            userList.UpdateList();
            // Add different amounts of funds depending on acc type to Funds.txt
            int numOfAcc = (int)Enum.Parse(typeof(AccountType), accType);
            int sizeTest = fundList.UserFunds.Length + 1;
            decimal[][] listedFunds = fundList.UserFunds;
            decimal[][] tempFundList = new decimal[sizeTest][];
            for(int i = 0; i < sizeTest; i++)
            {
                if (i == sizeTest - 1)
                {
                    tempFundList[i] = new decimal[numOfAcc];
                    for (int j = 0; j < numOfAcc; j++)
                    {
                        tempFundList[i][j] = 0;
                    }
                }
                else
                {
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
