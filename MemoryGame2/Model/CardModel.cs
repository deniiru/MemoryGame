using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemoryGame2.ViewModel;

namespace MemoryGame2.Model
{
    public class CardModel:BaseClass
    {
        public string ImagePath { get; set; }

        private bool isFlipped;
        public bool IsFlipped
        {
            get => isFlipped;
            set
            {
                isFlipped = value;
                OnPropertyChanged(nameof(DisplayImagePath));
            }
        }

        private bool isMatched;
        public bool IsMatched
        {
            get => isMatched;
            set
            {
                isMatched = value;
                OnPropertyChanged(nameof(DisplayImagePath));
                OnPropertyChanged(nameof(CardVisible));
            }
        }

        private string backImagePath;
        public string BackImagePath
        {
            get => backImagePath;
            set
            {
                backImagePath = value;
                OnPropertyChanged(nameof(DisplayImagePath));
            }
        }

        public string DisplayImagePath => IsFlipped || IsMatched ? ImagePath : BackImagePath;
        public System.Windows.Visibility CardVisible => IsMatched ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;

    }

}
