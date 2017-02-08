using FluToDo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FluToDo
{
    public partial class App : Application
    {
        public static TodoManager TodoMgr { get; private set; }

        public App()
        {
            InitializeComponent();
            TodoMgr = new TodoManager(new RestService());
            MainPage = new NavigationPage(new TodoPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
