using CommunityToolkit.Maui.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Components;
using ToDoList.Model;

namespace ToDoList.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged 
    {
        private CreateToDoModel _ToDo;
        public CreateToDoModel ToDo { get { return _ToDo; } set { OnPropertyChanged(nameof(_ToDo)); } }
        
        public ObservableCollection<CardTodo> Cards { get; set; } = new ObservableCollection<CardTodo>();
        public ICommand GenerateToDo { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainPageViewModel()
        {
            ToDo =  new CreateToDoModel();
            GenerateToDo = new Command(OpenGeneratePopUp);
            GenerateCards();
        }

        private async void OpenGeneratePopUp()
        {
            var popup = new CreateToDo();

            var result = await new Page().ShowPopupAsync(popup);
        }
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void SetBold(Label label, string Bold, string text)
        {
            label.Text = $"<strong>{Bold}:</strong> {text}";
        }
        private async void OnTodoDeleted()
        {
            GenerateCards();
            await new Page().DisplayAlert("Deleted", "Your file has been deleted!", "Ok");
        }

        private async void OnTodoUpdated()
        {
            GenerateCards();
        }
        public async void GenerateCards()
        {
            string cacheDir = FileSystem.Current.CacheDirectory;
            var Files = Directory.GetFiles(cacheDir);
            Cards.Clear();

            if (Files.Count() > 0)
                foreach (var item in Files)
                {
                    var file = File.ReadAllText(item);
                    var obj = JsonSerializer.Deserialize<TodoObject>(file);
                    var card = new CardTodo();
                    SetBold(card.LblTitle, "Title", obj.Title);
                    SetBold(card.LblDescription, "Description", obj.Description);
                    SetBold(card.LblDone, "Done", "");
                    card.CbDone.IsChecked = obj.Completed;
                    Cards.Add(card);
                    Cards.LastOrDefault()!.TodoDeleted += OnTodoDeleted;
                    Cards.LastOrDefault()!.TodoUpdated += OnTodoUpdated;
                }
        }
    }
}
