using Android.Content.Res;
using MvvmHelpers.Commands;
using SecretaryApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;


namespace SecretaryApp.ViewModels
{
  public class ContactsViewModel : INotifyPropertyChanged
    {
       private ObservableCollection<Contact> contactsCollect = new ObservableCollection<Contact>();
      
        
        private Contact selectedItem;

        public Contact Selecteditem
        {
          get  { return selectedItem; }
          set  { selectedItem = value; OnPropertyChanged("Selecteditem"); }
        }



        private string searchkey;
        public ICommand searchcommand { get; }

        public ICommand MessageCommand { get; }

        public ICommand CallCommand { get; }
        

        public string SearchKey
        {
            get { return searchkey; }
            set { searchkey = value; OnPropertyChanged("Searchkey"); }
        }
        public ObservableCollection<Contact> cntcollect
        {
            get { return contactsCollect; }
            set
            {
                contactsCollect = value; OnPropertyChanged("cntCollect");
            }
        }

        public  ContactsViewModel()
        {
            GetContact();
            searchcommand = new AsyncCommand(searchtexthcanged);
            MessageCommand = new AsyncCommand(messagecommand);
            CallCommand = new AsyncCommand(MakeCall);
        }

        private async Task messagecommand()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SelectionPage(Selecteditem));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task GetContact()
        {
            contactsCollect.Clear();
            try
            {
                // cancellationToken parameter is optional
                var cancellationToken = default(CancellationToken);
                var contacts = await Contacts.GetAllAsync(cancellationToken);

                if (contacts == null)
                    return;

                foreach (var contact in contacts)
                    if (SearchKey != null){ if (contact.DisplayName.ToLower().Contains(SearchKey.ToLower())) { contactsCollect.Add(contact); } }
                    else { contactsCollect.Add(contact); }
                    
                    
            }
            catch (Exception ex)
            {
                // Handle exception here.
            }
        }

       async Task searchtexthcanged()
        {
            GetContact();
        }



        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public async Task MakeCall()
        {
            PlacePhoneCall(selectedItem.Phones[0].PhoneNumber);
        }


        
            public void PlacePhoneCall(string number)
            {
                try
                {
                    PhoneDialer.Open(number);
                }
                catch (ArgumentNullException anEx)
                {
                    // Number was null or white space
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Phone Dialer is not supported on this device.
                }
                catch (Exception ex)
                {
                    // Other error has occurred.
                }
            }
        




    }
}
