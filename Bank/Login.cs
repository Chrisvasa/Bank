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
        protected int userIndex = 0;
        //protected string[,] users = new string[0, 0];
        //public Login()
        //{
        //    string[] userArr = File.ReadAllLines(@"C:\Users\Chris\Desktop\Bank\Bank\Users.txt");
        //    users = new string[userArr.Count(), 2];

        //    for (int i = 0; i < userArr.Count(); i++)
        //    {
        //        for (int j = 0; j < 2; j++)
        //        {
        //            var numbers = userArr[i].Split(',');
        //            users[i, j] = numbers[j];
        //            if (j == 1)
        //            {
        //                users[i, j] = users[i, j].TrimStart();
        //            }
        //        }
        //    }
        //}

        public int UserIndex 
        { 
            get { return userIndex; } 
            set { userIndex = value; }
        }
        public bool UserLogin(string[,] users)
        {
            bool isTrue = true;
            int count = 0;
            do
            {
                Console.Clear();
                Console.Write("Enter your username: ");
                string userName = Console.ReadLine();
                for (int i = 0; i < users.Length / 2; i++)
                {
                    if (users[i, 0] == userName)
                    {
                        Console.Write("Password: ");
                        string password = Console.ReadLine();
                        if (users[i, 1] == password)
                        {
                            Console.WriteLine("Login success!");
                            Console.ReadLine();
                            UserIndex = i;
                            isTrue = false;
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Wrong password. Try again!");
                            count++;
                            Console.ReadKey();
                        }
                        if (count >= 3)
                        {
                            Console.WriteLine("Too many attempts have been made..");
                            Console.ReadKey();
                            isTrue = false;
                        }
                    }
                    //else
                    //{
                    //    Console.WriteLine("Username not found");
                    //    Console.ReadKey();
                    //}
                }
            } while (isTrue);
            return false;

        }
    }
}