using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class CreateToDoViewModel : ObservableObject
    {
        public event Action? RequestClose;

        [ObservableProperty]
        private CreateToDoModel toDo;
        
        public CreateToDoViewModel()
        {
            toDo = new CreateToDoModel();
        }

        public CreateToDoViewModel(string title, string description)
        {
            toDo = new CreateToDoModel() { Title = title, Description = description, OldTitle = title };
        }


        [RelayCommand]
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
