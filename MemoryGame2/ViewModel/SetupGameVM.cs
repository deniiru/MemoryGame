using MemoryGame2.Commands;
using MemoryGame2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MemoryGame2.Services;

namespace MemoryGame2.ViewModel
{
    class SetupGameVM
    {
        public UserModel User {  get; set; }
     
        private string cardType = "Sea";
        public string CardType
        {
            get => cardType;
            set
            {
                cardType = value;

            }
        }

        private bool isCustomDimension;
        public bool IsCustomDimension
        {
            get => isCustomDimension;
            set
            {
                isCustomDimension = value;
               
            }
        }

        private int rows = 4;
        public int Rows
        {
            get => rows;
            set
            {
                rows = value;
            
            }
        }

        private int cols = 4;
        public int Cols
        {
            get => cols;
            set
            {
                cols = value;
               
            }
        }

        private int time = 60;
        public int Time
        {
            get => time;
            set
            {
                time = value;
            }
        }


        public SetupGameVM(UserModel user) {
            
            User = user;
        }


        private ICommand newGameCommand;
        public ICommand NewGameCommand
        {
            get
            {
                if (newGameCommand == null)
                {
                    newGameCommand = new RelayCommand(_ => NewGame());
                }
                return newGameCommand;
            }
        }

        public void NewGame()
        {
            int finalRows = IsCustomDimension ? Rows : 4;
            int finalCols = IsCustomDimension ? Cols : 4;
            User.Tries++;
            GameSettings.NewGame(User, finalRows, finalCols, CardType, Time);
        }

        private ICommand openGameCommand;
        public ICommand OpenGameCommand
        {
            get
            {
                if (openGameCommand == null)
                {
                    openGameCommand = new RelayCommand(_ => Services.GameSettings.OpenGameDialog(User));   
                }
                return openGameCommand;
            }
        }

        private ICommand statistics;
        public ICommand StatisticsCommand
        {
            get
            {
                if (statistics == null)
                {
                    statistics = new RelayCommand(_ => Services.GameSettings.OpenStatistics());
                }
                return statistics;
            }
        }

        public Action? CloseAction { get; set; }

        private ICommand closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(_ => Cancel());
                }
                return closeCommand;
            }
        }
        private void Cancel()
        {
            Services.FileHelpers.UpdateUser(User);
            CloseAction?.Invoke();
        }
    }
}
