using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp3.Models
{
    internal class Trivia
    {
        public int response_code { get; set; }
        public Quiz[] results { get; set; }
    }

    internal class Quiz
    {
        public string type { get; set; }
        public string difficulty { get; set; }
        public string category { get; set; }
        public string question { get; set; }
        public string correct_answer { get; set; }
        public string[] incorrect_answers { get; set; }
    }
}
