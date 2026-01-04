using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public partial class CreateToDoModel : ObservableObject
    {
        [ObservableProperty]

        private  string? title;
        [ObservableProperty]

        private string? oldTitle;
        [ObservableProperty]

        private string? description;
 
    }
}
