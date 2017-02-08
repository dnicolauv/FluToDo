using FluToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FluToDo
{
    public partial class NewItem : ContentPage
    {
        public ICommand CreateCommand { get; set; }

        public NewItem()
        {
            InitializeComponent();
            this.Title = "Add New Item";

            this.CreateCommand = new Command(async (sender) =>
            {
                TodoItem item = new TodoItem();
                item.IsComplete = swComplete.IsToggled;
                item.Name = txtName.Text;
                await App.TodoMgr.SaveItemAsync(item, true);
                await App.Current.MainPage.Navigation.PopAsync(true);
            });

            this.BindingContext = this;
        }
    }
}
