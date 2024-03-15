using TriviaApp3.Models;

namespace TriviaApp3.Views;

public partial class GamePage : ContentPage
{
    public int QuestionCounter { get; set; }
    public bool Answered { get; set; }
    public int Points { get; set; }
    Models.FormattedTrivia Trivia { get; set; }
    List<Models.UserData> UserDataList { get; set; }

    public GamePage()
    {
        InitializeComponent();
        QuestionCounter = 0;
        Points = 0;
        Answered = false;
        UserDataList = new List<Models.UserData>();
        StartGame();
    }

    private async void StartGame()
    {
        Contracts.IPrepareNewGameFacade _prepareNewGameFacade = new Facades.PrepareNewGameFacade();
        Trivia = await _prepareNewGameFacade.PrepareNewGame();

        if (Trivia != null)
        {
            CategoryLabel.Text = Trivia.Quizzes[QuestionCounter].Category;
            QuestionField.Text = Trivia.Quizzes[QuestionCounter].Question;
            FirstAnswerField.Text = Trivia.Quizzes[QuestionCounter].Answers[0];
            SecondAnswerField.Text = Trivia.Quizzes[QuestionCounter].Answers[1];
            ThirdAnswerField.Text = Trivia.Quizzes[QuestionCounter].Answers[2];
            FourthAnswerField.Text = Trivia.Quizzes[QuestionCounter].Answers[3];
        }
    }

    private async void QuestionButtonCLicked(object sender, EventArgs e)
    {
        if (Answered)
        {
            if (QuestionCounter < (Trivia.NumberOfQuestions - 1))
            {
                QuestionCounter++;

                CategoryLabel.Text = Trivia.Quizzes[QuestionCounter].Category;
                QuestionField.Text = Trivia.Quizzes[QuestionCounter].Question;
                FirstAnswerField.Text = Trivia.Quizzes[QuestionCounter].Answers[0];
                SecondAnswerField.Text = Trivia.Quizzes[QuestionCounter].Answers[1];
                ThirdAnswerField.Text = Trivia.Quizzes[QuestionCounter].Answers[2];
                FourthAnswerField.Text = Trivia.Quizzes[QuestionCounter].Answers[3];

                FirstAnswerField.BackgroundColor = Microsoft.Maui.Graphics.Color.Parse("Bisque");
                SecondAnswerField.BackgroundColor = Microsoft.Maui.Graphics.Color.Parse("Bisque");
                ThirdAnswerField.BackgroundColor = Microsoft.Maui.Graphics.Color.Parse("Bisque");
                FourthAnswerField.BackgroundColor = Microsoft.Maui.Graphics.Color.Parse("Bisque");

                Answered = false;
            }
            else
            {
                await Navigation.PushAsync(new Views.GameOverPage(Trivia.NumberOfQuestions, Points, UserDataList));
            }
        }
    }

    private void AnswerButtonClicked(object sender, EventArgs e)
    {
        if (!Answered)
        {
            string userAnswer = ((Button)sender).Text;
            string correctAnswer = Trivia.Quizzes[QuestionCounter].CorrectAnswer;
            int getPoint = 0;
            bool updatedScore = false;

            if (userAnswer == correctAnswer)
            {
                ((Button)sender).BackgroundColor = Microsoft.Maui.Graphics.Color.Parse("Green");
                getPoint = 1;
                Points++;
            }
            else
            {
                ((Button)sender).BackgroundColor = Microsoft.Maui.Graphics.Color.Parse("Red");
                if (FirstAnswerField.Text == correctAnswer)
                {
                    FirstAnswerField.BackgroundColor = Microsoft.Maui.Graphics.Color.Parse("Green");
                }
                else if (SecondAnswerField.Text == correctAnswer)
                {
                    SecondAnswerField.BackgroundColor = Microsoft.Maui.Graphics.Color.Parse("Green");
                }
                else if (ThirdAnswerField.Text == correctAnswer)
                {
                    ThirdAnswerField.BackgroundColor = Microsoft.Maui.Graphics.Color.Parse("Green");
                }
                else if (FourthAnswerField.Text == correctAnswer)
                {
                    FourthAnswerField.BackgroundColor = Microsoft.Maui.Graphics.Color.Parse("Green");
                }
            }

            foreach (Models.UserData userData in UserDataList)
            {
                if (userData.Category == Trivia.Quizzes[QuestionCounter].Category)
                {
                    userData.Answered++;
                    userData.Correct += getPoint;
                    userData.Percentage = (int)(Math.Floor(((double)userData.Correct / (double)userData.Answered) * 100));
                    updatedScore = true;
                    break;
                }
            }
            if (!updatedScore)
            {
                Models.UserData userData = new Models.UserData()
                {
                    Id = Guid.NewGuid(),
                    Category = Trivia.Quizzes[QuestionCounter].Category,
                    Answered = 1,
                    Correct = getPoint,
                    Percentage = getPoint * 100
                };
                UserDataList.Add(userData);
            }
            Answered = true;
        }
    }
}