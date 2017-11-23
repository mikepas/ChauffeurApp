using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChauffeurApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DashboardPage : ContentPage
	{
		public DashboardPage ()
		{
			InitializeComponent ();
		}

	    protected override bool OnBackButtonPressed()
	    {
	        Device.BeginInvokeOnMainThread(async () =>
	        {
	            if (!await DisplayAlert("Uitloggen?", "Weet u zeker dat u uit wilt loggen?", "Ja", "Nee")) return;
	            base.OnBackButtonPressed();
	            await Navigation.PushModalAsync(new MainPage());
	        });
	        return true;
        }
    }
}