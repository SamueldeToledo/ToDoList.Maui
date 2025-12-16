using CommunityToolkit.Maui.Views;
using System.Text.Json;

namespace ToDoList;

public partial class CreateToDo : Popup
{
	public CreateToDo()
	{
		InitializeComponent();
        SetBold(lblTitle, "Title","");
        SetBold(lblDescription, "Description","");
	}


    private void SetBold(Label label, string Bold, string text)
    {
        label.Text = $"<strong>{Bold}:</strong> {text}";
    }

    private async void Close_Clicked(object sender, EventArgs e)
    {
        await this.CloseAsync();
    }

    private void Create_Clicked(object sender, EventArgs e)
    {
        string cacheDir = FileSystem.Current.CacheDirectory;
        GenerateTodoFile(cacheDir);
    }

    async void GenerateTodoFile(string CachePath)
    {
        string path = $"{CachePath}\\{EtTitle.Text}.Json";
        var obj = new TodoObject() { Title = EtTitle.Text, Description = EtDescription.Text, Completed = false };
        var Json = JsonSerializer.Serialize(obj);
        File.WriteAllText(path, Json);
        Application.Current.MainPage.DisplayAlert("Created!" , "Your To Do File has been saved", "Ok");
        await this.CloseAsync();
    }
}