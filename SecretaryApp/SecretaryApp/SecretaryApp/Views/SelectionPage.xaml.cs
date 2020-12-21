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
    public partial class SelectionPage : ContentPage
    {
        public SelectionPage(Contact contact)
        {
            InitializeComponent();
           
            lblName.Text = contact.DisplayName;
            lblNumber.Text = contact.Phones[0].PhoneNumber;
            if(contact.Emails.Count != 0)
            {
                lblMail.Text = contact.Emails[0].EmailAddress;
            }
           
        }

        

    }
}