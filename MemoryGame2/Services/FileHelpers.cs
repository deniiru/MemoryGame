using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using MemoryGame2.Model;

using System.IO;
using System.Security.RightsManagement;
using System.ComponentModel;



namespace MemoryGame2.Services
{
    public static class FileHelpers
    {
        private static readonly string filePath = "Users.json";
        private static readonly string GamesFolder = Path.Combine(Environment.CurrentDirectory, "Games");


        public static void SaveToFileCollection(ObservableCollection<UserModel> users)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string jsonString = JsonSerializer.Serialize(users, options);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving users: {ex.Message}");
            }
        }


        public static void UpdateUser(UserModel updatedUser)
        {
            try
            {
                var users = LoadFromFileCollection();

                var existingUser = users.FirstOrDefault(u => u.Name == updatedUser.Name);
                if (existingUser != null)
                {
                    int index = users.IndexOf(existingUser);
                    users[index] = updatedUser;

                    SaveToFileCollection(users);
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
            }
        }
        public static ObservableCollection<UserModel> LoadFromFileCollection()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonString = File.ReadAllText(filePath);
                    var users = JsonSerializer.Deserialize<ObservableCollection<UserModel>>(jsonString);
                    return users ?? new ObservableCollection<UserModel>();
                }
                else
                {
                    return new ObservableCollection<UserModel>();

                }

            }
            catch (Exception ex)
            {
                return new ObservableCollection<UserModel>();
            }
        }



        public static void SaveGameForUser(GameSaveData gameData)
        {
            try
            {
                string userFolder = Path.Combine(GamesFolder, gameData.User.Name);
                Directory.CreateDirectory(userFolder);

                string gameFilePath = Path.Combine(userFolder, "games.json");

                List<GameSaveData> savedGames = new List<GameSaveData>();

                if (File.Exists(gameFilePath))
                {
                    string jsonString = File.ReadAllText(gameFilePath);
                    savedGames = JsonSerializer.Deserialize<List<GameSaveData>>(jsonString) ?? new List<GameSaveData>();
                }

                for (int i = 0; i < savedGames.Count; i++)
                {
                    if (savedGames[i].StartDate == gameData.StartDate)
                    {
                        savedGames[i] = gameData;
                        break;
                    }
                }
                for (int i = 0; i < savedGames.Count; i++)
                {
                    if (savedGames[i].StartDate == gameData.StartDate)
                    {
                        savedGames[i] = gameData;
                        break;
                    }
                }
                if (!savedGames.Contains(gameData))
                {
                    savedGames.Add(gameData);
                }
                

                var options = new JsonSerializerOptions { WriteIndented = true };
                string outputJson = JsonSerializer.Serialize(savedGames, options);
                File.WriteAllText(gameFilePath, outputJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game for user {gameData.User.Name}: {ex.Message}");
            }
        }

        
        public static List<GameSaveData> LoadGamesForUser(UserModel user)
        {
            try
            {
                string userFolder = Path.Combine(GamesFolder, user.Name);
                string gameFilePath = Path.Combine(userFolder, "games.json");

                if (File.Exists(gameFilePath))
                {
                    string jsonString = File.ReadAllText(gameFilePath);
                    var savedGames = JsonSerializer.Deserialize<List<GameSaveData>>(jsonString);
                    return savedGames ?? new List<GameSaveData>();
                    MessageBox.Show(savedGames[0].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading games for user {user.Name}: {ex.Message}");
            }
            return new List<GameSaveData>();
        }


        public static void DeleteGameForUser(UserModel user, GameSaveData gameData)
        {
            try
            {
                string userFolder = Path.Combine(GamesFolder, user.Name);
                string gameFilePath = Path.Combine(userFolder, "games.json");

                if (!File.Exists(gameFilePath))
                    return;

                string jsonString = File.ReadAllText(gameFilePath);
                List<GameSaveData> savedGames = JsonSerializer.Deserialize<List<GameSaveData>>(jsonString) ?? new List<GameSaveData>();

                savedGames.RemoveAll(g => g.Date == gameData.Date);

                var options = new JsonSerializerOptions { WriteIndented = true };
                string outputJson = JsonSerializer.Serialize(savedGames, options);
                File.WriteAllText(gameFilePath, outputJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting game for user {user.Name}: {ex.Message}");
            }
        }


        private static readonly string UsersFilePath = "UserStats.json";

        public static ObservableCollection<UserModel> LoadUserStatistics()
        {
            try
            {
                if (File.Exists(UsersFilePath))
                {
                    string json = File.ReadAllText(UsersFilePath);
                    var users = JsonSerializer.Deserialize<ObservableCollection<UserModel>>(json);
                    return users ?? new ObservableCollection<UserModel>();
                }
                return new ObservableCollection<UserModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading user statistics: {ex.Message}");
                return new ObservableCollection<UserModel>();
            }
        }

        public static void AddOrUpdateUserStatistic(UserModel userStat)
        {
            try
            {
                ObservableCollection<UserModel> users = LoadUserStatistics();

                var existingUser = default(UserModel);
                foreach (var user in users)
                {
                    if (user.Name == userStat.Name)
                    {
                        existingUser = user;
                        break;
                    }
                }

                if (existingUser != null)
                {
                    existingUser.Tries = userStat.Tries;
                    existingUser.Wins = userStat.Wins;
                }
                else
                {
                    users.Add(userStat);
                }

                var options = new JsonSerializerOptions { WriteIndented = true };
                string output = JsonSerializer.Serialize(users, options);
                File.WriteAllText(UsersFilePath, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user statistic: {ex.Message}");
            }
        }

        public static void DeleteUser(UserModel user)
        {
            try
            {
                ObservableCollection<UserModel> users = LoadUserStatistics();

                var userToDelete = users.FirstOrDefault(u => u.Name == user.Name);
                if (userToDelete != null)
                {
                    users.Remove(userToDelete);

                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string output = JsonSerializer.Serialize(users, options);
                    File.WriteAllText(UsersFilePath, output);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user");
            }
        }
    }
}

