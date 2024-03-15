namespace TriviaApp3.Views;

public partial class GameOverPage : ContentPage
{
    public GameOverPage(int numberOfQuestions, int points, List<Models.UserData> newUserData)
    {
        InitializeComponent();
        string message = ViewModels.GameOverPageViewModel.GetMessage(((double)points / (double)numberOfQuestions));
        ScoreLabel.Text = "You scored: " + points + " out of " + numberOfQuestions + ". " + message;
        ViewModels.GameOverPageViewModel.UpdateScoreToDatabase(newUserData);
    }

    private async void MainMenuButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}