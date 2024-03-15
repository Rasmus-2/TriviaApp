using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp3.ViewModels
{
    internal class StatisticsPageViewModel
    {
        public List<Models.UserData> UserDataList { get; set; }

        public StatisticsPageViewModel()
        {
            UserDataList = new List<Models.UserData>();

            var task = Task.Run(() => GameOverPageViewModel.GetUserDataFromDb());
            task.Wait();
            UserDataList.AddRange(task.Result);

            var sortedList = (from d in UserDataList
                              orderby d.Percentage descending
                              select d).ToList();

            UserDataList.Clear();
            UserDataList.AddRange(sortedList);
        }
    }
}
