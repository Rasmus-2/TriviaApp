using MongoDB.Driver;
using TriviaApp3.DataAccessLayer;

namespace TriviaApp3.Views;

public partial class LoginPage : ContentPage
{
    static Models.SettingsSingleton settings = Models.SettingsSingleton.GetSettings();

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        List<Models.User> users = await DataAccessLayer.DB.GetUserCollection().AsQueryable().ToListAsync();

        Models.User currentUser = null;

        foreach (Models.User user in users)
        {
            if (user.UserName == UserNameEntry.Text)
            {
                currentUser = user;
                break;
            }
        }

        if (currentUser != null)
        {
            if (currentUser.Password == PasswordEntry.Text)
            {
                settings.SetCurrentUser(currentUser.UserName);
                FileIO.WriteAndReplace(currentUser.UserName);
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                InfoLabel.Text = "Wrong password";
            }
        }
        else
        {
            InfoLabel.Text = "Can't find user";
        }
    }

    private async void OnSignUpButtonClicked(object sender, EventArgs e)
    {
        List<Models.User> users = await DataAccessLayer.DB.GetUserCollection().AsQueryable().ToListAsync();
        bool success = true;

        foreach (Models.User user in users)
        {
            if (user.UserName == UserNameEntry.Text)
            {
                InfoLabel.Text = "Username unavailable, please choose another one";
                success = false;
                break;
            }
        }

        if (success)
        {
            Models.User newUser = new Models.User()
            {
                Id = new Guid(),
                UserName = UserNameEntry.Text,
                Password = PasswordEntry.Text
            };

            await DataAccessLayer.DB.GetUserCollection().InsertOneAsync(newUser);
            settings.SetCurrentUser(newUser.UserName);
            FileIO.WriteAndReplace(newUser.UserName);
            Navigation.PushAsync(new MainPage());
        }
    }
}