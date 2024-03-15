namespace TriviaApp3.Views;

public partial class StatisticsPage : ContentPage
{
    public StatisticsPage()
    {
        InitializeComponent();
        BindingContext = new ViewModels.StatisticsPageViewModel();
    }

    private async void MainMenuButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}