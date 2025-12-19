using CommunityToolkit.Maui.Views;
using System.Text.Json;
using ToDoList.ViewModel;

namespace ToDoList;

public partial class CreateToDo : Popup
{
    public CreateToDo()
    {
        InitializeComponent();
        SetBold(lblTitle, "Title", "");
        SetBold(lblDescription, "Description", "");

        if (Vm != null)
            Vm.RequestClose += OnRequestClose;
    }

    public CreateToDo(string Title, string Description)
    {
        InitializeComponent();
        SetBold(lblTitle, "Title", "");
        SetBold(lblDescription, "Description", "");

        BtnCreate.Text = "Update";
        var Vm = new CreateToDoViewModel(Title, Description);
        BindingContext = Vm;
        if (Vm != null)
            Vm.RequestClose += OnRequestClose;
    }

    private async void OnRequestClose()
    {
        await this.CloseAsync();
    }


    private void SetBold(Label label, string Bold, string text)
    {
        label.Text = $"<strong>{Bold}:</strong> {text}";
    }

    private async void Close_Clicked(object sender, EventArgs e)
    {
        await this.CloseAsync();
    }
}