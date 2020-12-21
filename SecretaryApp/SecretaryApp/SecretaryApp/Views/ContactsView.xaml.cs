using SecretaryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecretaryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsView : ContentPage
    {
        
        public ContactsView()
        {
            InitializeComponent();

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectionPage(cntlist.SelectedItem as Contact));
        }
    }
}