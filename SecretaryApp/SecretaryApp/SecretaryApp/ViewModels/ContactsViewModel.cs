using Android.Content.Res;
using MvvmHelpers.Commands;
using SecretaryApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;


namespace SecretaryApp.ViewModels
{
    public class ContactsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Contact> contactsCollect = new ObservableCollection<Contact>();


        public ObservableCollection<Contact> FilteredContacts = new ObservableCollection<Contact>();

        public List<Contact> sortedContacts = new List<Contact>();


        public List<Contact> SortedContacts
        {
            get { return sortedContacts; }
            set { sortedContacts = value; OnPropertyChanged("SortedContacts"); }
        }

    


        private Contact selectedItem;

        public Contact Selecteditem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged("Selecteditem"); }
        }

        public ObservableCollection<Contact> filteredContacts
        {
            get { return FilteredContacts; }
            set {   FilteredContacts = value; OnPropertyChanged("filteredContacts"); }
        }



        private string searchkey;
        public ICommand searchcommand { get; }

        public ICommand MessageCommand { get; }

        public ICommand CallCommand { get; }


        public string SearchKey
        {
            get { return searchkey; }
            set { searchkey = value; SearchPropertyChanged("Searchkey"); }
        }
        public ObservableCollection<Contact> cntcollect
        {
            get { return contactsCollect; }
            set
            {
                contactsCollect = value; OnPropertyChanged("cntCollect");
            }
        }

        public ContactsViewModel()
        {
            GetContact();
           
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
            
            try
            {
                // cancellationToken parameter is optional
                var cancellationToken = default(CancellationToken);
                var contacts = await Contacts.GetAllAsync(cancellationToken);

                if (contacts == null)
                    return;




                foreach (var contact in contacts) { contactsCollect.Add(contact); }
            
            }
            catch (Exception ex)
            {
                // Handle exception here.
            }

            foreach (var item in contactsCollect)
            {
                FilteredContacts.Add(item);
            }

            SortedContacts = FilteredContacts.OrderBy(x => x.DisplayName).ToList();





        }

       



        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SearchPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            FilteredContacts.Clear();
            if (SearchKey != null)
            {
                foreach (var item in contactsCollect)
                {
                    if (item.DisplayName.ToLower().Contains(SearchKey.ToLower())) { FilteredContacts.Add(item); }
                }

                SortedContacts = FilteredContacts.OrderBy(x => x.DisplayName).ToList();


            }
            else if (SearchKey == "")
            {

                foreach (var item in contactsCollect)
                {
                    FilteredContacts.Add(item);
                }
                SortedContacts = FilteredContacts.OrderBy(x => x.DisplayName).ToList();



            }
            else {
                foreach (var item in contactsCollect)
                {
                    FilteredContacts.Add(item);
                }


                SortedContacts = FilteredContacts.OrderBy(x => x.DisplayName).ToList();
                

            }
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
