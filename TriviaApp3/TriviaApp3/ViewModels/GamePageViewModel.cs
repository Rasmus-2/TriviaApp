using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using TriviaApp3.Models;

namespace TriviaApp3.ViewModels
{
    internal class GamePageViewModel : Contracts.IGamePageViewModel
    {
        public async Task<Models.Trivia> GetTriviaAsync(string uri)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://opentdb.com/");

            Models.Trivia trivia = null;

            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                trivia = JsonSerializer.Deserialize<Models.Trivia>(responseString);
            }
            return trivia;
        }

        public async Task<Models.Trivia> DecodeStrings(Models.Trivia trivia)
        {
            foreach (var item in trivia.results)
            {
                //Format the data
                item.category = HttpUtility.HtmlDecode(item.category);
                item.question = HttpUtility.HtmlDecode(item.question);
                item.correct_answer = HttpUtility.HtmlDecode(item.correct_answer);
                for (int i = 0; i < item.incorrect_answers.Length; i++)
                {
                    item.incorrect_answers[i] = HttpUtility.HtmlDecode(item.incorrect_answers[i]);
                }
            }
            return trivia;
        }

        public async Task<FormattedTrivia> FormatData(Models.Trivia trivia)
        {
            Models.FormattedTrivia fTrivia = new Models.FormattedTrivia();

            fTrivia.NumberOfQuestions = trivia.results.Length;

            for (int i = 0; i < trivia.results.Length; i++)
            {
                Models.FormattedQuiz quiz = new Models.FormattedQuiz();
                quiz.Category = trivia.results[i].category;
                quiz.Question = trivia.results[i].question;
                quiz.CorrectAnswer = trivia.results[i].correct_answer;

                List<string> answers = new List<string>();
                answers.Add(trivia.results[i].correct_answer);
                answers.AddRange(trivia.results[i].incorrect_answers);
                answers.Sort();
                quiz.Answers = answers;
                fTrivia.Quizzes.Add(quiz);
            }
            return fTrivia;
        }
    }
}
