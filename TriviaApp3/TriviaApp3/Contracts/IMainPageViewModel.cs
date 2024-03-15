using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp3.Contracts
{
    internal interface IMainPageViewModel
    {
        Task<List<Models.User>> GetUserFromDb();
        string CheckLoggedIn();
        bool CheckUserExists(List<Models.User> users, string loggedInUser);
    }
}
