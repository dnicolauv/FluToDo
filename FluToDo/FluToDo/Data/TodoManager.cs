using FluToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluToDo.Data
{
    public class TodoManager
    {
        IRestService restService;

        public TodoManager(IRestService service)
        {
            restService = service;
        }

        public Task<List<TodoItem>> GetDataAsync()
        {
            return restService.GetDataAsync();
        }

        public Task SaveItemAsync(TodoItem item, bool isNewItem = false)
        {
            return restService.SaveItemAsync(item, isNewItem);
        }

        public Task DeleteItemAsync(TodoItem item)
        {
            return restService.DeleteItemAsync(item.Key);
        }
    }
}
