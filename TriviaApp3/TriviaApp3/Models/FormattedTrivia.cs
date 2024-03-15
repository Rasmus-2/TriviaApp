using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp3.Models
{
    internal class FormattedTrivia
    {
        public int NumberOfQuestions { get; set; }
        public List<FormattedQuiz> Quizzes { get; set; }

        public FormattedTrivia() 
        {
            Quizzes = new List<FormattedQuiz> { };
        }
    }

    internal class FormattedQuiz
    {
        public string Category { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> Answers { get; set; }
    }
}
