﻿using FluToDo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FluToDo.Data
{
    public class RestService : IRestService
    {
        HttpClient client;

        public List<TodoItem> Items { get; private set; }

        public RestService()
        {
            //var authData = string.Format("{0}:{1}", Settings.Username, Settings.Password);
            //var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public async Task<List<TodoItem>> GetDataAsync()
        {
            Items = new List<TodoItem>();

            var uri = new Uri(string.Format(Settings.RestUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task SaveItemAsync(TodoItem item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Settings.RestUrl, item.Key));

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    uri = new Uri(string.Format(Settings.RestUrl + "/{0}", item.Key));
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"TodoItem successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }

        public async Task DeleteItemAsync(string id)
        {
            var uri = new Uri(string.Format(Settings.RestUrl+ "/{0}", id));

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"TodoItem successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }
    }
}
