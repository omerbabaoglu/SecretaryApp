using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms.OpenWhatsApp;

namespace SecretaryApp.ViewModels
{
    public class SelectionPageViewModel : INotifyPropertyChanged
    {
        private string labelname;
        private string labelnumber;
        private string labelmail;
        private string messageEntry;
        private string mailEntry;
        public List<string> MailList = new List<string>();
      
        
        public ICommand smsCommand { get; }
        public ICommand WpCommand { get;  }
        public ICommand MailCommand { get; }


        public string MailEntry
        {
            get { return mailEntry; }
            set { mailEntry = value; OnPropertyChanged("MailEntry"); }
        }
        


        public string MessageEntry
        {
            get { return messageEntry; }
            set { messageEntry = value; OnPropertyChanged("MessageEntry"); }
        }

        public string labelMail
        {
            get { return labelmail; }
            set { labelmail = value; OnPropertyChanged("labelMail"); }
        }

        public string labelName
        {
            get { return labelname; }
            set { labelname = value;  OnPropertyChanged("labelName"); }
        }

        public string labelNumber
        {
            get{ return labelnumber; }
            set { labelnumber = value; OnPropertyChanged("labelNumber"); }
        }
        public SelectionPageViewModel()
        {
            smsCommand = new AsyncCommand(SendSms);
            WpCommand = new AsyncCommand(SendWpMessage);
            MailCommand = new AsyncCommand(SendEmail);
            MailList.Add(labelMail);
            
        }
        
     
        
        
        public event PropertyChangedEventHandler PropertyChanged;



        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public async Task SendSms()
        {
            
            try
            {
                var message = new SmsMessage(MessageEntry, labelNumber );
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Sms is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }

        public async Task SendWpMessage()
        {
            Chat.Open(labelNumber, MessageEntry);
        }

        public async Task SendEmail()
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = MailEntry,
                    Body = MessageEntry,
                    To = MailList,
                   // Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
        }

    }
}
