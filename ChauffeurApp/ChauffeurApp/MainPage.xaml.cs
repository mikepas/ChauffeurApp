using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ChauffeurApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Image.Source = ImageSource.FromResource("ChauffeurApp.eeoswqfxlmiozhyszotl.png");
        }

        protected override void OnAppearing()
        {
            usernameEntry.Text = "erik@snellewiel.nl";
            passwordEntry.Text = "test";
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            //var result = await CheckLogin();
            if (usernameEntry.Text == "erik@snellewiel.nl" && passwordEntry.Text == "test")
            {
                await Navigation.PushModalAsync(new DashboardPage());
            }
            else
            {
                await DisplayAlert("Onjuist!", "Gebruikersnaam of wachtwoord is onjuist!", "Ok");
            }
        }

        private async Task<string> CheckLogin(string password = "test", string username = "ivowo2@gmail.com")
        {
            string webadres = "http://webdesignwolters.nl/snelle-wiel/admin/api/login/"+username+"/"+password;
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(webadres);
            return JsonConvert.DeserializeObject<string>(json);
        }
    }
}
