using System;
using System.Net.Http;
using ChauffeurApp.classes;
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
            var button = (Button) sender;
            button.IsEnabled = false;

            HttpClient client = new HttpClient();
            string result;

            try
            {
                result = await client.GetStringAsync("http://webdesignwolters.nl/snelle-wiel/admin/api/login/" + usernameEntry.Text.Trim() + "/" + passwordEntry.Text.Trim());
            }
            catch (Exception)
            {
                await DisplayAlert("Geen verbinding!", "Zet uw WIFI of mobiele data aan!", "Ok");
                button.IsEnabled = true;
                return;
            }

            try
            {
                var chauffeur = JsonConvert.DeserializeObject<Chauffeur>(result);
                await Navigation.PushModalAsync(new DashboardPage(chauffeur.Id, chauffeur.Voornaam));
            }
            catch (Exception)
            {
                await DisplayAlert("Onjuist!", "Gebruikersnaam of wachtwoord is onjuist!", "Ok");
                button.IsEnabled = true;
            }

            button.IsEnabled = true;
        }
    }
}
