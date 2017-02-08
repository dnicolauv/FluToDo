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
    public partial class TodoPage : ContentPage
    { 
        public ICommand NewItemCommand { get; set; }
        public TodoPage()
        {
            InitializeComponent();
            this.Title = "FluToDo";
            
            this.NewItemCommand = new Command(async (sender) =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new NewItem(), true);
            });

            this.BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listview.Refreshing += Listview_Refreshing; ;
            listview.ItemsSource = await App.TodoMgr.GetDataAsync();
        }

        private async void Listview_Refreshing(object sender, EventArgs e)
        {
            listview.ItemsSource = await App.TodoMgr.GetDataAsync();
            listview.EndRefresh();
        }

        private async void OnChangeStatus(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);

            TodoItem item = null;

            if (mi.CommandParameter is TodoItem)
            {
                item = (TodoItem)mi.CommandParameter;
                item.IsComplete = !item.IsComplete;
                await App.TodoMgr.SaveItemAsync(item, false);
                listview.BeginRefresh();
            }
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);

            TodoItem item = null;

            if (mi.CommandParameter is TodoItem)
            {
                item = (TodoItem)mi.CommandParameter;
                item.IsComplete = !item.IsComplete;

                await App.TodoMgr.DeleteItemAsync(item);
                listview.BeginRefresh();
            }
        }
    }
}
