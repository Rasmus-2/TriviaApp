using TriviaApp3.DataAccessLayer;

namespace TriviaApp3
{
    public partial class MainPage : ContentPage
    {
        static Models.SettingsSingleton settings = Models.SettingsSingleton.GetSettings();

        public MainPage()
        {
            InitializeComponent();

            Contracts.ILoginCheckerFacade _loginCheckerFacade = new Facades.LoginCheckerFacade();

            var task = Task.Run(() => _loginCheckerFacade.CheckLogin());
            task.Wait();

            if (task.Result == false)
            {
                Navigation.PushAsync(new Views.LoginPage());
            }
            else
            {
                UserNameLabel.Text = settings.GetCurrentUser();
            }
        }

        private void PlayButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.GamePage());
        }

        private void SettingsButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.SettingsView());
        }

        private void StatisticsButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.StatisticsPage());
        }

        private void QuitButtonClicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }

        private void LogOutButtonClicked(object sender, EventArgs e)
        {
            FileIO.DeleteLogin();
            Navigation.PushAsync(new Views.LoginPage());
        }
    }
}
