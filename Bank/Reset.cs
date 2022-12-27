using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    // A class that allows the user to reset passwords of different users
    internal class Reset
    {
        string[,] users = new string[0,0];
        public Reset(User userList)
        {
            users = userList.Users;
        }


        public void ResetPass(int index)
        {
            users[index, 1] = "321";
        }
    }
}
