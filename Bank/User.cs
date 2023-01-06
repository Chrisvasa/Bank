using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    // A class that reads from the Users.txt, parses it and then stores it as a 2d string array
    internal class User
    {
        protected string[,] users = new string[0, 0];
        protected string password;
        // A constructor that parses and stores it in a 2d array upon initialization
        public User()
        {
            string[] userArr = File.ReadAllLines(@"C:\Users\Chris\Desktop\Bank\Bank\Users.txt");
            users = new string[userArr.Count(), 2];
            password = "12345";
            for (int i = 0; i < userArr.Count(); i++)
            {
                for (int j = 0; j < 2; j++)
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

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        // Changes the password at given index
        public void ChangePassword(int index)
        {
            users[index, 1] = password;
        }
        // Updates the Users.txt file with changed values
        public void UpdateList()
        {
            string text = "";
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    text += users[i, j];
                    if(j == 0)
                    {
                        text += ", ";
                    }
                }
                if(i < 4)
                {
                    text += "\n";
                }
            }
            File.WriteAllText(@"C:\Users\Chris\Desktop\Bank\Bank\Users.txt", text);
        }

    }
}
