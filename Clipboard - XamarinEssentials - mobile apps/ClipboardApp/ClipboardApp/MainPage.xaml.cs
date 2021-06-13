using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace ClipboardApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(EntryText.Text);
            await DisplayAlert("You did it!", "You've copied text to clipboard!", "Ok");
        }

        public async void Button2_Clicked(object sender, EventArgs e)
        {
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                if (text.Length != 0)
                {
                    PasteText.Text = text;
                } else
                    await DisplayAlert("Error!", "Clipboard is empty!", "Ok");
            }
        }

       /* protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (Clipboard.HasText)
            {
                await Clipboard.SetTextAsync("1234");
                var text = await Clipboard.GetTextAsync();
                if (text.Length == 4)
                    EntryText.Text = text;
            }
        }*/
    }
}
