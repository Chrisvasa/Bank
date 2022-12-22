using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class Login
    {
        protected string[,] users = new string[0, 0];
        public Login()
        {
            string[] userArr = File.ReadAllLines(@"C:\Users\Chris\Desktop\Bank\Users.txt");
            users = new string[userArr.Count(), 2];

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

        public void UserLogin()
        {
            Console.Clear();
            Console.WriteLine("Enter your username: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();
        }
    }
}