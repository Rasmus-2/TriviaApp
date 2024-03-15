using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaApp3.DataAccessLayer;

namespace TriviaApp3.ViewModels
{
    internal class GameOverPageViewModel
    {
        static Models.SettingsSingleton settings = Models.SettingsSingleton.GetSettings();

        public static string GetMessage(double score)
        {
            string message = "Error calculating score";
            if (score >= 1)
            {
                message = "Perfect score, impressive!";
            }
            else if (score > 0.8)
            {
                message = "That's a really great score, congratulations!";
            }
            else if (score > 0.6)
            {
                message = "Well done, keep it up!";
            }
            else if (score > 0.4)
            {
                message = "Not bad, but you can do better!";
            }
            else if (score > 0.2)
            {
                message = "Better luck next time";
            }
            else if (score > 0)
            {
                message = "Well, it could have gone worse.";
            }
            else
            {
                message = "On the bright side, it can only get better!";
            }
            return message;
        }

        public static async Task<List<Models.UserData>> GetUserDataFromDb()
        {
            string currentUser = settings.GetCurrentUser();
            List<Models.UserData> userDatas = await DataAccessLayer.DB.GetUserDataCollection(currentUser).AsQueryable().ToListAsync();
            return userDatas;
        }

        //Can be improved, replaces all data.
        public async static void UpdateScoreToDatabase(List<Models.UserData> newUserData)
        {
            string currentUser = settings.GetCurrentUser();

            List<Models.UserData> dbUserData = await GetUserDataFromDb();
            bool found = false;

            foreach (var newData in newUserData)
            {
                foreach (var dbData in dbUserData)
                {
                    if (dbData.Category == newData.Category)
                    {
                        dbData.Answered += newData.Answered;
                        dbData.Correct += newData.Correct;
                        dbData.Percentage = (int)(Math.Floor(((double)dbData.Correct / (double)dbData.Answered) * 100));
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    dbUserData.Add(newData);
                }
                found = false;
            }

            DataAccessLayer.DB.GetUserDataCollection(currentUser).Database.DropCollection(currentUser);

            foreach (var dbData in dbUserData)
            {
                await DataAccessLayer.DB.GetUserDataCollection(currentUser).InsertOneAsync(dbData);
                //var filter = Builders<Models.UserData>.Filter.Eq(x => x.Category, dbData.Category);
                //await DataAccessLayer.DB.GetUserDataCollection().ReplaceOneAsync(filter, dbData);
            }
        }
    }
}
