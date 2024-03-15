using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp3.Models
{
    public class UserData
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public int Answered { get; set; }
        public int Correct { get; set; }
        public int Percentage { get; set; }
    }
}
