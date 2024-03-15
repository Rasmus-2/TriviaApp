using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp3.Models
{
    internal class SettingsSingleton
    {
        private static readonly SettingsSingleton settings = new SettingsSingleton();

        public static SettingsSingleton GetSettings()
        {
            return settings;
        }

        private int Amount;
        private string Difficulty;
        private string CurrentUser;

        private SettingsSingleton()
        {
            Amount = 5;
            Difficulty = "easy";
            InitCurrentUser();
        }

        private void InitCurrentUser()
        {
            List<string> text = new List<string>();
            text = DataAccessLayer.FileIO.ReadAll();
            if (text.Count > 0)
            {
                CurrentUser = text[0];
            }
            else
            {
                CurrentUser = "";
            }
        }

        public void SetAmount(int amount)
        {
            Amount = amount;
        }

        public void SetDifficulty(string difficulty)
        {
            Difficulty = difficulty;
        }

        public void SetCurrentUser(string currentUser)
        {
            CurrentUser = currentUser;
        }

        public int GetAmount()
        {
            return Amount;
        }

        public string GetDifficulty()
        {
            return Difficulty;
        }

        public string GetCurrentUser()
        {
            return CurrentUser;
        }


    }
}
