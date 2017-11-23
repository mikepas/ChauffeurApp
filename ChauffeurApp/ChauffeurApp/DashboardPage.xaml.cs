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

		    var forgetPassword_tap = new TapGestureRecognizer();
		    forgetPassword_tap.Tapped += (s, e) =>
		    {
		        Navigation.PushModalAsync(new OrderPage());
		    };
            Label.GestureRecognizers.Add(forgetPassword_tap);
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