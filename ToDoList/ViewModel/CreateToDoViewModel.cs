using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Model;

namespace ToDoList.ViewModel
{
    public class CreateToDoViewModel : INotifyPropertyChanged
    {
        public event Action? RequestClose;
        private CreateToDoModel _ToDo;
        public CreateToDoModel ToDo {
            get { return _ToDo; }
            set { _ToDo = value; OnPropertyChanged(nameof(_ToDo)); }
        }
        public ICommand CreateFile { get; set; }

        public CreateToDoViewModel()
        {
            CreateFile = new Command(Create);
            ToDo = new CreateToDoModel();
        }

        public CreateToDoViewModel(string title, string description)
        {
            CreateFile = new Command(Create);
            ToDo = new CreateToDoModel() { Title = title, Description = description, OldTitle = title };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private async void Create()
        {
            string CachePath = FileSystem.Current.CacheDirectory;
            string path = $"{CachePath}\\{ToDo.Title}.Json";
            var obj = new TodoObject() { Title = ToDo.Title!, Description = ToDo.Description!, Completed = false };
            var Json = JsonSerializer.Serialize(obj);

            if (ToDo.OldTitle != ToDo.Title)
                File.Delete($"{CachePath}\\{ToDo.OldTitle}.Json");

            File.WriteAllText(path, Json);
            RequestClose?.Invoke();
        }
    }
}
