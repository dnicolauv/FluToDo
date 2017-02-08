using FluToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluToDo.Data
{
    public interface IRestService
    {
        Task<List<TodoItem>> GetDataAsync();

        Task SaveItemAsync(TodoItem item, bool isNewItem);

        Task DeleteItemAsync(string id);
    }
}
