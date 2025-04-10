using MemoryGame2.Model;
using MemoryGame2.ViewModel;
using MemoryGame2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MemoryGame2.Services
{
    public class GameSettings
    {

        public static void NewGame(UserModel user, int row, int col, string cardType, int time)
        {
            if (row * col % 2 == 1)
            { MessageBox.Show("You must insert a even number of cards, change the Grid Dimenision");
            return; }
            if (row < 2 || row > 6)
            {
                MessageBox.Show("The number of rows must be between 2 and 6");
                return;
            }

            if (col < 2 || col > 6)
            {
                MessageBox.Show("The number of columns must be between 2 and 6");
                return;
            }

            Services.FileHelpers.AddOrUpdateUserStatistic(user);
            GameVM game = new GameVM(user, cardType, row, col, time);
            new GameView(game).Show();
        }

        public static void OpenStatistics()
        {
            StatisticsView statisticsView = new StatisticsView();
            statisticsView.Show();
        }

        public static void OpenGame(GameSaveData gameData)
        {
            GameVM gameVM = new GameVM(gameData);
            new GameView(gameVM).Show();
        }

        public static void OpenGameDialog(UserModel user)
        {
            new OpenGameDialog(user).Show();
        }

        public static void PlayGame(UserModel user)
        {
            MessageBox.Show(user.Name);
            if (user == null)
            {
                MessageBox.Show("User is empthy");
            }
            else
            new SetupGame(user).Show();
        }
    }
}
