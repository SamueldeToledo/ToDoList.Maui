using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Components;

namespace ToDoList.Model
{
    public class MainPageModel : INotifyPropertyChanged
    {
        private string? _Title;
        public string? Title { get { return _Title; } set { OnPropertyChange(nameof(_Title)); } }
        private string? _NoItems;
        public string? NoItems { get { return _NoItems; } set { OnPropertyChange(nameof(_NoItems)); } }
        private CardTodo? _Cards;
        public CardTodo? Cards { get { return _Cards; } set { OnPropertyChange(nameof(_Cards)); } }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChange(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));

        }
    }
}
