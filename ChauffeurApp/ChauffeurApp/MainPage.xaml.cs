using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void Button_OnClicked(object sender, EventArgs e)
        {
            if (usernameEntry.Text == "erik@snellewiel.nl" && passwordEntry.Text == "test")
            {
                Navigation.PushModalAsync(new DashboardPage());
            }
            else
            {
                DisplayAlert("Onjuist!", "Gebruikersnaam of wachtwoord is onjuist!", "Ok");
            }
        }
    }
}
