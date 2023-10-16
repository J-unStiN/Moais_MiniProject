using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp_Project.Etc;

namespace WpfApp_Project.Models
{
    public class AccountModel : Notifier
    {
        public string Name 
        { 
            get { return _Name; } 
            set { _Name = value; OnPropertyChanged(nameof(Name)); }
        }
        private string _Name;

        public int Age 
        { 
            get { return _Age; } 
            set { _Age = value; OnPropertyChanged(nameof(Age));}
        }
        private int _Age;

        public string PhoneNumber 
        { 
            get { return _PhoneNumber; } 
            set { _PhoneNumber = value; OnPropertyChanged(nameof(PhoneNumber));}
        }
        private string _PhoneNumber;

    }
}
