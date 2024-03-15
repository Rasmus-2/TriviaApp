using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp3.Contracts
{
    internal interface IPrepareNewGameFacade
    {
        Task<Models.FormattedTrivia> PrepareNewGame(); 
    }
}
