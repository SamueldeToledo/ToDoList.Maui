using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;

namespace ToDoList.Components
{
    public class CardTodo : Border
    {
        public event Action? TodoDeleted;
        public event Action? TodoUpdated;

        public Label LblTitle;
        public Label LblDescription;
        public Label LblDone;
        public Button BtnDelete;
        public Button BtnUpdate;
        public CheckBox CbDone;

        public CardTodo()
        {
            BuildLayout();
        }

        private void BuildLayout()
        {
            // === Border (this) ===
            IsVisible = true;
            MinimumHeightRequest = 120;
            MaximumHeightRequest = 600;
            MaximumWidthRequest = 500;
            Stroke = Colors.Black;
            StrokeThickness = 2;
            StrokeShape = new RoundRectangle { CornerRadius = 10 };
            Margin= 10;

            // === Grid ===
            var grid = new Grid
            {
                Margin = 10,
                RowSpacing = 10,
                ColumnSpacing = 5,
                RowDefinitions =
                {
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto)
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Auto),
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Auto)
                }
            };

            // === Labels ===
            LblTitle = CreateHtmlLabel();
            LblDescription = CreateHtmlLabel();
            LblDone = CreateHtmlLabel();

            // === Buttons ===
            BtnUpdate = CreateButton("Update", BtnUpdate_Clicked);
            BtnDelete = CreateButton("Delete", BtnDelete_Clicked);

            // === Checkbox ===
            CbDone = new CheckBox
            {
                Color = Colors.Black,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };
            CbDone.CheckedChanged += CbDone_CheckedChanged;

            // === Grid add ===
            grid.Add(LblTitle);
            grid.Add(LblDescription, 0, 1);
            grid.Add(LblDone, 0, 2);
            grid.Add(CbDone, 1, 2);
            grid.Add(BtnUpdate, 2, 2);
            grid.Add(BtnDelete, 3, 2);

            Content = grid;
        }

        // =========================
        // Events
        // =========================

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            RemoveFile();
            TodoDeleted?.Invoke();
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            await DisplayPopup();
            TodoUpdated?.Invoke();
        }

        private void CbDone_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            WriteFile(e.Value);
        }

        // =========================
        // Helpers
        // =========================

        private async Task DisplayPopup()
        {
            string title = RemoveHtml("Title:", LblTitle);
            string description = RemoveHtml("Description:", LblDescription);

            var popup = new CreateToDo(title, description);
            await Application.Current.MainPage.ShowPopupAsync(popup);
        }

        private void WriteFile(bool completed)
        {
            var path = System.IO.Path.Combine(
                FileSystem.Current.CacheDirectory,
                $"{RemoveHtml("Title:", LblTitle)}.json"
            );

            var json = File.ReadAllText(path);
            var todo = JsonSerializer.Deserialize<TodoObject>(json);

            if (todo == null) return;

            todo.Completed = completed;
            File.WriteAllText(path, JsonSerializer.Serialize(todo));
        }

        private void RemoveFile()
        {
            var path = System.IO.Path.Combine(
                FileSystem.Current.CacheDirectory,
                $"{RemoveHtml("Title:", LblTitle)}.json"
            );

            if (File.Exists(path))
                File.Delete(path);
        }

        private static Label CreateHtmlLabel()
        {
            return new Label
            {
                TextType = TextType.Html
            };
        }

        private static Button CreateButton(string text, EventHandler handler)
        {
            var btn = new Button
            {
                Text = text,
                BackgroundColor = Colors.Black,
                FontSize = 15,
                HorizontalOptions = LayoutOptions.End
            };

            btn.Clicked += handler;
            return btn;
        }

        private static string RemoveHtml(string word, Label label)
        {
            return label.Text?.Replace($"<strong>{word}</strong> ", "") ?? string.Empty;
        }
    }
}
