using MemoryGame2.Commands;
using MemoryGame2.Model;
using MemoryGame2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MemoryGame2.ViewModel
{
    public class GameVM : BaseClass
    {
        private readonly GameModel model;
        public ObservableCollection<CardModel> Cards { get; private set; }
        public UserModel User { get; set; }

        private CardModel? firstSelected;
        private CardModel? secondSelected;
        private bool canFlip = true;


        private int rows;
        public int Rows
        {
            get => rows;
            set
            {
                rows = value;
                OnPropertyChanged("Rows");
            }
        }

        private int cols;
        public int Cols
        {
            get => cols;
            set
            {
                cols = value;
                OnPropertyChanged("Cols");
            }
        }

        public string Category;

        private bool gameOver;
        public bool GameOver
        {
            get => gameOver;
            set
            {
                gameOver = value;
                OnPropertyChanged(nameof(GameOver));
            }
        }

        private bool gameWon;
        public bool GameWon
        {
            get => gameWon;
            set
            {
                gameWon = value;
                OnPropertyChanged(nameof(GameWon));
            }
        }

        private DateTime startDate;



        private ICommand cardClickCommand;
        public ICommand CardClickCommand { 
            get
            {
                if (cardClickCommand == null)
                    cardClickCommand = new RelayCommand(CardClicked, _ => canFlip);
                return cardClickCommand;
            }
        }


        private ICommand saveGameCommand;
        public ICommand SaveGameCommand
        {
            get
            {
                if (saveGameCommand == null)
                    saveGameCommand = new RelayCommand(_ => SaveGame());
                return saveGameCommand;
            }
        }

        public TimeManager Timer { get; private set; }


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
            CloseAction?.Invoke();
        }


        private void Timer_TimeExpired(object sender, EventArgs e)
        {
            canFlip = false;
            GameOver = true;
           
            if (!GameWon)
            {
                // Optionally, notify the user that time has expired.
                MessageBox.Show("Time is up! Game over!");
            }
        }

        public GameVM(UserModel user, string cardType, int row, int col, int time)
        {
            model = new GameModel();
            model.GenerateCards(row, col, cardType);

            Cards = new ObservableCollection<CardModel>(model.Cards);
            User = user;

            Rows = row; Cols = col; Category = cardType;

            Timer = new TimeManager(time);
            Timer.TimeExpired += Timer_TimeExpired;

            Timer.Start();

            GameOver = false;
            GameWon = false;

            startDate = DateTime.Now;
        }

        public GameVM(GameSaveData gameData)
        {
            Cards = new ObservableCollection<CardModel>(gameData.Cards);
            User = gameData.User;
            Rows = gameData.Rows; Cols = gameData.Cols;
            Timer = new TimeManager(gameData.TimeLeft);
            Timer.TimeExpired += Timer_TimeExpired;
            Timer.Start();
            GameOver = gameData.GameOver;
            GameWon = gameData.GameWon;
        }

        private async void CardClicked(object obj)
        {
            if (!canFlip) return;

            var clicked = obj as CardModel;
            if (clicked == null || clicked.IsFlipped || clicked.IsMatched) return;

            clicked.IsFlipped = true;

            if (firstSelected == null)
            {
                firstSelected = clicked;
            }
            else if (secondSelected == null)
            {
                secondSelected = clicked;
                canFlip = false;

                await Task.Delay(1000);

                if (firstSelected.ImagePath == secondSelected.ImagePath)
                {
                    firstSelected.IsMatched = true;
                    secondSelected.IsMatched = true;

                    if (GameOverCheck())
                    {
                        GameOver = true;
                        GameWon = true;
                        Timer.Stop();
                        User.Wins++;
                       FileHelpers.AddOrUpdateUserStatistic(User);
                        MessageBox.Show("You Won!!!");

                    }
                }
                else
                {
                    firstSelected.IsFlipped = false;
                    secondSelected.IsFlipped = false;
                }

                firstSelected = null;
                secondSelected = null;
                canFlip = true;
                Keyboard.ClearFocus();
            }
        }

        private bool GameOverCheck()
        {
            return Cards.All(c => c.IsMatched);
        }

        private void SaveGame()
        {
            var gameData = new GameSaveData
            {
                Cards = Cards.ToList(),
                User = User,
                Rows = Rows,
                Cols = Cols,
                TimeLeft = Timer.TimeLeft,
                Category = Category,
                Date = DateTime.Now,
                StartDate = startDate
            };
            Services.FileHelpers.SaveGameForUser(gameData);
            
            MessageBox.Show("The game has been saved");
        }
    }
}
