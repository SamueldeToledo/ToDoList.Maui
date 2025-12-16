using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ToDoList
{
    public class CardTodo
    {
        public Border BFiles { get; private set; }
        public Label LblTitleBorder { get; private set; }
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
                RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto }
            },
                ColumnDefinitions =
            {
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

            CbCheckBox = new CheckBox
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                Color = Colors.Black,

            };
            CbCheckBox.CheckedChanged += CbCheckBox_CheckedChanged;

            grid.Add(LblTitleBorder);
            grid.Add(LblDescription, 0, 1);
            grid.Add(LblDone, 0, 2);
            grid.Add(CbCheckBox, 1, 2);

            BFiles.Content = grid;
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

        private void WriteFile(bool value)
        {
            string path = $"{FileSystem.Current.CacheDirectory}\\{LblTitleBorder.Text.Replace("<strong>Title:</strong> ", "")}.Json";
            var file = File.ReadAllText(path);
            var Json = JsonSerializer.Deserialize<TodoObject>(file);
            Json.Completed = value;
            var result = JsonSerializer.Serialize(Json);
            File.WriteAllText(path, result);
        }
    }
}
