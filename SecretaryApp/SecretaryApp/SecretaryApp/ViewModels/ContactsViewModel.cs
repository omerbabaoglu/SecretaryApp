using MvvmHelpers.Commands;
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
        private Contact SelectedItem;

        public Contact selecteditem
        {
          get  { return SelectedItem; }
          set  { SelectedItem = value; OnPropertyChanged("selecteditem"); }
        }



        private string searchkey;
        public ICommand searchcommand { get; }
        

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


    }
}
