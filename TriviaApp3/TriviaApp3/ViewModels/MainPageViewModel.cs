using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp3.ViewModels
{
    internal class MainPageViewModel : Contracts.IMainPageViewModel
    {
        public async Task<List<Models.User>> GetUserFromDb()
        {
            List<Models.User> users = await DataAccessLayer.DB.GetUserCollection().AsQueryable().ToListAsync();
            return users;
        }

        public string CheckLoggedIn()
        {
            List<string> text = DataAccessLayer.FileIO.ReadAll();
            if (text.Count > 0)
            {
                string loggedInUser = text[0];

                if (loggedInUser == string.Empty)
                {
                    return null;
                }
                else
                {
                    return loggedInUser;
                }
            }
            else
            {
                return null;
            }
        }

        public bool CheckUserExists(List<Models.User> users, string loggedInUser)
        {
            foreach (var user in users)
            {
                if (user.UserName == loggedInUser)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
