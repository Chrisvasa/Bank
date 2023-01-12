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
        protected string password;
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
            users = new string[userArr.Count(), 3];
            password = "12345";
            for (int i = 0; i < userArr.Count(); i++)
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

        //public string Password
        //{
        //    get { return password; }
        //    set { password = value; }
        //}
        // Changes the password at given index
        //public void ChangePassword(int index)
        //{
        //    users[index, 1] = password;
        //}

        // Skapa load()
        // Som läser in users när man kallar på den

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
