using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using System.Text.Json;

namespace ToDoList;

public partial class MainPage : ContentPage
{

    public List<CardTodo> Cards { get; set; } = new List<CardTodo>();
    
    public MainPage()
	{
		InitializeComponent();
		LblTitle.Text = "Welcome <strong>User</strong>";
        GenerateCards();
    }

    private void SetBold(Label label, string Bold, string text)
	{
		label.Text = $"<strong>{Bold}:</strong> {text}";
    }

    private async void BtnCreateList_Clicked(object sender, EventArgs e)
    {
        await DisplayPopup();
    }
    public async Task DisplayPopup()
    {
        var popup = new CreateToDo();

        var result = await this.ShowPopupAsync(popup);

       GenerateCards();
    }

    private async void OnTodoDeleted()
    {
        GenerateCards();
        await DisplayAlert("Deleted", "Your file has been deleted!", "Ok");
    }
    private async void GenerateCards()
    {
        string cacheDir = FileSystem.Current.CacheDirectory;
        var Files = Directory.GetFiles(cacheDir);
        CardsLayout.Clear();

        if (Files.Count() > 0)
        {
            foreach (var item in Files)
            {
                var file = File.ReadAllText(item);
                var obj = JsonSerializer.Deserialize<TodoObject>(file);
                var card = new CardTodo();
                SetBold(card.LblTitleBorder, "Title", obj.Title);
                SetBold(card.LblDescription, "Description", obj.Description);
                SetBold(card.LblDone, "Done", "");
                card.CbCheckBox.IsChecked = obj.Completed;
                Cards.Add(card);
                Cards.LastOrDefault()!.TodoDeleted += OnTodoDeleted;
                CardsLayout.Add(card.BFiles);


            }

            LblNoItems.IsVisible = false;

        }
        else
        {
            LblNoItems.Text = "<strong>No Content!</strong>";
            LblNoItems.IsVisible = true;
        }
    }
}