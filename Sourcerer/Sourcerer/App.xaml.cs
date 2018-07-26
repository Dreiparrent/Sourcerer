using System;
using Xamarin.Forms;
using Sourcerer.Views;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Sourcerer
{
	public partial class App : Application
	{
		
		public App ()
		{
			InitializeComponent();

            // Is Tablet
            if (Device.Idiom == TargetIdiom.Tablet || Device.Idiom == TargetIdiom.Desktop)
                MainPage = new MainPage();
            else
                MainPage = new MainPage();

        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
