using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame2.Model
{
    public class GameSaveData
    {
        public List<CardModel> Cards { get; set; }
        public UserModel User { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int TimeLeft { get; set; }
        public bool GameOver { get; set; }
        public bool GameWon { get; set; }

        public string Category { get; set; }

        public DateTime Date { get; set; }
        public DateTime StartDate { get; set; }
    }
}
