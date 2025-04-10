using MemoryGame2.Commands;
using MemoryGame2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MemoryGame2.ViewModel
{
    public class SavedGamesVM:BaseClass
    {
        public ObservableCollection<GameSaveData> SavedGames { get; set; }

        private GameSaveData selectedGame;
        public GameSaveData SelectedGame
        {
            get { return selectedGame; }
            set
            {
                selectedGame = value;
                OnPropertyChanged(nameof(SelectedGame));
            }
        }
        private ICommand deleteGameCommand;
        public ICommand DeleteGameCommand
        {
            get
            {
                if (deleteGameCommand == null)
                {
                    deleteGameCommand = new RelayCommand(DeleteGame, _ => SelectedGame != null);
                }
                return deleteGameCommand;
            }
        }

        private ICommand openGameCommand;
        public ICommand OpenGameCommand
        {
            get
            {
                if (openGameCommand == null)
                {
                    openGameCommand = new RelayCommand(OpenGame, _ => SelectedGame != null);
                }
                return openGameCommand;
            }
        }


        public SavedGamesVM(UserModel user)
        {
            var savedGamesList = Services.FileHelpers.LoadGamesForUser(user);
            SavedGames = new ObservableCollection<GameSaveData>(savedGamesList);
        }

        private void OpenGame(object parameter)
        {
            if (SelectedGame == null)
                return;
            Services.FileHelpers.DeleteGameForUser(SelectedGame.User, SelectedGame);
            Services.GameSettings.OpenGame(SelectedGame);
        }

        private void DeleteGame (object parameter)
        {

            if (SelectedGame == null)
                return;
            Services.FileHelpers.DeleteGameForUser(SelectedGame.User, SelectedGame);
            SavedGames.Remove(SelectedGame);
            
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
            CloseAction?.Invoke();
        }
    }
}

