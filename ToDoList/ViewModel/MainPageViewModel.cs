using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]

        private CreateToDoModel toDo;
        [ObservableProperty]
        private ObservableCollection<CardTodo> cards = new ObservableCollection<CardTodo>();

        public MainPageViewModel()
        {
            toDo = new CreateToDoModel();
            GenerateCards();
        }
  

        private void SetBold(Label label, string Bold, string text)
        {
            label.Text = $"<strong>{Bold}:</strong> {text}";
        }

        [RelayCommand]
        private async void Deleted()
        {
            GenerateCards();
            await new Page().DisplayAlert("Deleted", "Your file has been deleted!", "Ok");
        }

        private async void Updated()
        {
            GenerateCards();
        }
        public async void GenerateCards()
        {
            string cacheDir = FileSystem.Current.CacheDirectory;
            var Files = Directory.GetFiles(cacheDir);
            cards.Clear();

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
                    cards.Add(card);
                    cards.LastOrDefault()!.TodoDeleted += Updated;
                    cards.LastOrDefault()!.TodoUpdated += Updated;
                }
        }
    }
}
