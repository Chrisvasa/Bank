using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bank
{
    // A class that reads from the Users.txt, parses it and then stores it as a 2d string array
    // And writes any changes back to Users.txt
    internal class User
    {
        protected string[,] users = new string[0, 0];
        // A constructor that parses and stores it in a 2d array upon initialization
        public User()
        {
            LoadUser();
        }
        // A method that loads in and parses from the Users.txt
        // Splits the text, trims it and stores it as a 2d string array
        // First column is the username, second is for the pincode and third is for the account type
        public void LoadUser()
        {
            string[] userArr;
            if (File.Exists(".\\Users.txt"))
            {
                userArr = File.ReadAllLines(".\\Users.txt");
            }
            else
            {
                userArr = File.ReadAllLines("../../../Users.txt");
            }
            users = new string[userArr.Length, 3];
            for (int i = 0; i < userArr.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var numbers = userArr[i].Split(',');
                    users[i, j] = numbers[j];
                    if (j == 1)
                    {
                        users[i, j] = users[i, j].TrimStart();
                    }
                }
            }
        }

        public string[,] Users 
        { 
            get { return users; } 
            set { users = value; }
        }

        public string GetUserInfo(int index)
        {
            return users[index, 2];
        }
        // A method to reset the pincodes of existing users
        // Checks if the username exists, and if it does
        // Prompts user for new pincode and then stores it into the 2d array that was created above
        public void ResetPin()
        {
            Console.Clear();
            Console.Write("Username: ");
            string anwser = Console.ReadLine().ToUpper();
            for (int i = 0; i < users.Length / 3; i++) // Loops through the array of users
            {
                if (users[i, 0] == anwser)
                {
                    Console.Write("Enter a new pincode: ");
                    bool success = int.TryParse(Console.ReadLine(), out int newPass);
                    if (success)
                    {
                        users[i, 1] = newPass.ToString();
                        Console.WriteLine("Pincode was changed. Press any key to continue.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Pincode must contain NUMBERS only! Try again.");
                        Console.ReadKey();
                    }
                }
            }
        }

        // Updates the Users.txt file with changed values from the 2d string array
        public void UpdateList()
        {
            string text = "";
            for (int i = 0; i < users.Length / 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    text += users[i, j];
                    if(j == 0)
                    {
                        text += ", ";
                    }
                    else if(j == 1)
                    {
                        text += ",";
                    }
                }
                if(i < users.Length / 3 - 1)
                {
                    text += "\n";
                }
            }
            if (File.Exists(".\\Users.txt"))
            {
                File.WriteAllText(".\\Users.txt", text);
            }
            else
            {
                File.WriteAllText("../../../Users.txt", text);
            }
        }

    }
}
