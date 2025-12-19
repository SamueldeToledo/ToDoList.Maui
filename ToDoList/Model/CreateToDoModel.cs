using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class CreateToDoModel : INotifyPropertyChanged
    {

        private string? _Title;
        public string? Title
        {
            get { return _Title; }
            set { _Title = value; OnPropertyChanged(nameof(_Title)); }
        }

        private string? _OldTitle;
        public string? OldTitle
        {
            get { return _OldTitle ; }
            set { _OldTitle = value; OnPropertyChanged(nameof(_OldTitle)); }
        }

        private string? _Description;
        public string? Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged(nameof(_Description)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
