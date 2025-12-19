using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using System.Text.Json;
using ToDoList.Components;

namespace ToDoList;

public partial class MainPage : ContentPage
{
    
    public MainPage()
	{
		InitializeComponent();
		LblTitle.Text = "Welcome <strong>User</strong>";
    }

    private void SetBold(Label label, string Bold, string text)
	{
		label.Text = $"<strong>{Bold}:</strong> {text}";
    }

    private async void BtnCreateList_Clicked(object sender, EventArgs e)
    {
        await DisplayPopup();
        Vm.GenerateCards();
    }
    public async Task DisplayPopup()
    {
        var popup = new CreateToDo();

        var result = await this.ShowPopupAsync(popup);

    }
   
}