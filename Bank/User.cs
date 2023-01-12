using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bank
{
    // A class that reads from the Users.txt, parses it and then stores it as a 2d string array
    internal class User
    {
        protected string[,] users = new string[0, 0];
        // A constructor that parses and stores it in a 2d array upon initialization
        public User()
        {
            LoadUser();
        }

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
        // Getters and setters
        public string[,] GetUsers()
        {
            return users;
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

        // Updates the Users.txt file with changed values
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
