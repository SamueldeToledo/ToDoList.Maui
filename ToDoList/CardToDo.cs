using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;

namespace ToDoList
{
    public class CardTodo
    {
        public event Action TodoDeleted;
        public event Action TodoUpdated;
        public Border BFiles { get; private set; }
        public Label LblTitleBorder { get; private set; }
        public Button BtnDelete { get; private set; }
        public Button BtnUpdate { get; private set; }
        public Label LblDescription { get; private set; }
        public Label LblDone { get; private set; }
        public CheckBox CbCheckBox { get; private set; }

        public CardTodo()
        {
            Build();
        }

        private void Build()
        {
            BFiles = new Border
            {
                IsVisible = true,
                MaximumHeightRequest = 600,
                MaximumWidthRequest = 500,
                Stroke = Colors.Black,
                StrokeThickness = 2,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = 10
                }
            };

            var grid = new Grid
            {
                   Margin = new Thickness(10),
                   RowSpacing = 10,
                   ColumnSpacing= 5,
                   RowDefinitions = {
                                        new RowDefinition { Height = GridLength.Auto },
                                        new RowDefinition { Height = GridLength.Auto },
                                        new RowDefinition { Height = GridLength.Auto }
                                    },
                   ColumnDefinitions ={
                                          new ColumnDefinition { Width = GridLength.Star },
                                          new ColumnDefinition { Width = GridLength.Auto },
                                          new ColumnDefinition { Width = GridLength.Star },
                                          new ColumnDefinition { Width = GridLength.Auto }
                                      }
            };

            LblTitleBorder = new Label
            {
                TextType = TextType.Html
            };

            LblDescription = new Label
            {
                TextType = TextType.Html
            };

            LblDone = new Label
            {
                TextType = TextType.Html
            };

            BtnDelete = new Button
            {
              Text = "Delete",
              BackgroundColor = Colors.Black,
              HorizontalOptions = LayoutOptions.End,
              FontSize = 15,
                
            };

            BtnUpdate = new Button
            {
                Text = "Update",
                BackgroundColor = Colors.Black,
                HorizontalOptions = LayoutOptions.End,
                FontSize = 15,

            };

            CbCheckBox = new CheckBox
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                Color = Colors.Black,

            };
            CbCheckBox.CheckedChanged += CbCheckBox_CheckedChanged;
            BtnDelete.Clicked += BtnDelete_Clicked;
            BtnUpdate.Clicked += BtnUpdate_Clicked;
            grid.Add(LblTitleBorder);
            grid.Add(LblDescription, 0, 1);
            grid.Add(LblDone, 0, 2);
            grid.Add(BtnUpdate, 2, 2);
            grid.Add(BtnDelete, 3, 2);
            grid.Add(CbCheckBox, 1, 2);

            BFiles.Content = grid;
        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            RemoveFile();
        }
        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            await DisplayPopup();
            TodoUpdated.Invoke();
        }
        private void CbCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                WriteFile(true);
            }
            else
            {
                WriteFile(false);
            }
        }
        public async Task DisplayPopup()
        {
            string title = RemoveHtml("Title:", LblTitleBorder);
            string description = RemoveHtml("Description:", LblDescription);
            var popup = new CreateToDo(title, description);
            var result = await Application.Current.MainPage.ShowPopupAsync(popup);
        }
        private void WriteFile(bool value)
        {
            string path = $"{FileSystem.Current.CacheDirectory}\\{RemoveHtml("Title:", LblTitleBorder)}.Json";
            var file = File.ReadAllText(path);
            var Json = JsonSerializer.Deserialize<TodoObject>(file);
            Json.Completed = value;
            var result = JsonSerializer.Serialize(Json);
            File.WriteAllText(path, result);
        }

        private string RemoveHtml(string word, Label label)
        {
            return label.Text.Replace($"<strong>{word}</strong> ", "");
        }

        private void RemoveFile()
        {
            string path = $"{FileSystem.Current.CacheDirectory}\\{LblTitleBorder.Text.Replace("<strong>Title:</strong> ", "")}.Json";
            File.Delete(path);
            TodoDeleted?.Invoke();
        }
    }
}
