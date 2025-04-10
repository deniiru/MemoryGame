using MemoryGame2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame2.ViewModel
{
    public class UserPage: BaseClass
    {
        public UserModel UserModel { get; set; }

        public UserPage() { }
        public UserPage(UserModel userModel)
        {
            this.UserModel = userModel;
        }


    }
}
