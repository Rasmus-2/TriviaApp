using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp3.Facades
{
    internal class LoginCheckerFacade : Contracts.ILoginCheckerFacade
    {
        private readonly Contracts.IMainPageViewModel _mainPageViewModel;

        public LoginCheckerFacade()
        {
            _mainPageViewModel = new ViewModels.MainPageViewModel();
        }

        public async Task<bool> CheckLogin()
        {
            List<Models.User> users = await _mainPageViewModel.GetUserFromDb();

            string loggedInUser = _mainPageViewModel.CheckLoggedIn();

            bool loggedIn = false;
            if (loggedInUser != null)
            {
                loggedIn = _mainPageViewModel.CheckUserExists(users, loggedInUser);
            }
            return loggedIn;
        }
    }
}
