using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Reset
    {
        string[,] users = new string[0,0];
        public Reset(User userList)
        {
            users = userList.GetUsers();
        }
        public void ResetPass(int index)
        {
            users[index, 1] = "321";
        }
    }
}
