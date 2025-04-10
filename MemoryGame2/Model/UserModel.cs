using MemoryGame2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame2.Model
{
    public class UserModel: BaseClass
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string image_url;
        public string ImageUrl
        {
            get { return image_url; }
            set
            {
                image_url = value;
                OnPropertyChanged("ImageUrl");
            }
        }


        private int wins=0;
        public int Wins
        {
            get { return wins; }
            set
            {
                wins = value;
                OnPropertyChanged("Wins");
            }
        }

        private int tries=0;
        public int Tries
        {
            get { return tries; }
            set
            {
                tries = value;
                OnPropertyChanged("Tries");
            }
        }
        public UserModel() { }

        public UserModel(string name, string image_url)
        {
            Name = name;
            ImageUrl = image_url;
        }

        public double WinRate
        {
            get
            {
                if (Tries == 0)
                    return 0;
                return (double)Wins / Tries * 100;
            }
        }
    }
}
