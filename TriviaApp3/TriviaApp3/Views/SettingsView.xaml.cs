namespace TriviaApp3.Views;

public partial class SettingsView : ContentPage
{
    static Models.SettingsSingleton Settings = Models.SettingsSingleton.GetSettings();
    public SettingsView()
    {
        InitializeComponent();
        NumberOfQuestionsLabel.Text = $"Number of questions: {Settings.GetAmount()}";
        Slider.Value = Settings.GetAmount();

        if (Settings.GetDifficulty() == "easy")
        {
            EasyButton.IsChecked = true;
        }
        else if (Settings.GetDifficulty() == "medium")
        {
            MediumButton.IsChecked = true;
        }
        else if (Settings.GetDifficulty() == "hard")
        {
            HardButton.IsChecked = true;
        }
    }

    private async void MainMenuButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private void RadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        RadioButton difficultyButton = sender as RadioButton;
        //testLabel.Text = $"I see you are {difficultyButton.Content}";
        Settings.SetDifficulty(difficultyButton.Value.ToString());
    }

    private void SliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        int value = Convert.ToInt32(Slider.Value);
        NumberOfQuestionsLabel.Text = $"Number of questions: {value}";
        Settings.SetAmount(value);
    }
}