using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp3.Facades
{
    internal class PrepareNewGameFacade : Contracts.IPrepareNewGameFacade
    {
        private readonly Contracts.IGamePageViewModel _gamePageViewModel;

        public PrepareNewGameFacade()
        {
            _gamePageViewModel = new ViewModels.GamePageViewModel();
        }

        public async Task<Models.FormattedTrivia> PrepareNewGame()
        {
            Models.SettingsSingleton settings = Models.SettingsSingleton.GetSettings();
            string uri = "api.php?amount=" + settings.GetAmount() + "&difficulty=" + settings.GetDifficulty() + "&type=multiple";

            Models.Trivia trivia = await _gamePageViewModel.GetTriviaAsync(uri);
            Models.Trivia newTrivia = await _gamePageViewModel.DecodeStrings(trivia);
            Models.FormattedTrivia formattedTrivia = await _gamePageViewModel.FormatData(newTrivia);
            return formattedTrivia;
        }
    }
}