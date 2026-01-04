using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Components;

namespace ToDoList.Model
{
    public partial class MainPageModel : ObservableObject
    {
        [ObservableProperty]
        private string? title;

        [ObservableProperty]
        private string? noItems;

        [ObservableProperty]
        private CardTodo? cards;

    }
}
