using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame2.Model
{
    public class GameModel
    {
        public List<CardModel> Cards { get; private set; } = new();
        public string BackImagePath { get; private set; } = string.Empty;

        public UserModel User { get; private set; }

        public GameModel(UserModel user) 
        {
            User = user;
        }

        public GameModel() { }

        public void GenerateCards(int rows, int cols, string cardTypeFolder)
        {
            Cards.Clear();

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images", cardTypeFolder);
            var imagePaths = Directory.GetFiles(folderPath, "*.png").Where(p => !p.Contains("back.png")).ToList();

            if (File.Exists(Path.Combine(folderPath, "back.png")))
                BackImagePath = Path.Combine(folderPath, "back.png");

            int totalCards = rows * cols;

           
            if (totalCards % 2 != 0)
                throw new InvalidOperationException("The total number of cards must be even.");

            int pairsNeeded = totalCards / 2;
            var selectedImages = imagePaths.OrderBy(x => Guid.NewGuid()).Take(pairsNeeded).ToList();

            foreach (var img in selectedImages)
            {
                var card1 = new CardModel { ImagePath = img, IsFlipped = false, IsMatched = false, BackImagePath = BackImagePath };
                var card2 = new CardModel { ImagePath = img, IsFlipped = false, IsMatched = false, BackImagePath = BackImagePath };
                Cards.Add(card1);
                Cards.Add(card2);
            }

            Shuffle();
        }

        private void Shuffle()
        {
            var rnd = new Random();
            Cards = Cards.OrderBy(_ => rnd.Next()).ToList();
        }
    }
}
