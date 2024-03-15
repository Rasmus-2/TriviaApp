using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp3.Contracts
{
    internal interface IGamePageViewModel
    {
        Task<Models.Trivia> GetTriviaAsync(string uri);
        Task<Models.Trivia> DecodeStrings(Models.Trivia trivia);
        Task<Models.FormattedTrivia> FormatData(Models.Trivia trivia);
    }
}
