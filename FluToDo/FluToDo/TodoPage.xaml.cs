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

            //Removes toolbar item for Android, and uses fab button instead. Silently falls to toolbar item on iOS
            if (Device.OS == TargetPlatform.Android)
                ToolbarItems.Clear();

            listview.Refreshing += Listview_Refreshing;
            listview.ItemsSource = await App.TodoMgr.GetDataAsync();
        }

        private async void Listview_Refreshing(object sender, EventArgs e)
        {
            listview.ItemsSource = await App.TodoMgr.GetDataAsync();
            listview.EndRefresh();
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            TodoItem item = null;

            if (e.Item is TodoItem)
            {
                item = (TodoItem)e.Item;

                //Toggle the status
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
                string msg = String.Format("ToDo item {0} has been deleted", item.Name);
                await DisplayAlert("Item deleted", msg, "OK");
            }
        }
    }
}
