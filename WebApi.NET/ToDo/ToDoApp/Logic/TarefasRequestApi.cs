using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Model;

namespace ToDoApp.Logic
{
    class TarefasRequestApi
    {
        public static async Task<List<Tarefa>> ListarAsync()
        {
            var client = new HttpClient { BaseAddress = new Uri( App.BaseUrl ) };

            client.DefaultRequestHeaders.Add("Accept", "application/jason");
            client.DefaultRequestHeaders.Add("Authorization", "bearer " + MyLocalStorage.GetFromLocalStorage("token"));

            var response = await client.GetAsync("api/Tarefa");

            try
            {
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var lista = JsonConvert.DeserializeObject<List<Tarefa>>(json);

                return lista;
            }
            catch (Exception)
            {
                throw;
            }


        }

        internal async static Task GravarTarefa(Tarefa model)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(App.BaseUrl)
            };

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "bearer " + MyLocalStorage.GetFromLocalStorage("token"));           

            var json = JsonConvert.SerializeObject(model);

            var dados = new StringContent(
                json, Encoding.UTF8, "application/json"
                );

            var response = await client.PostAsync("api/Tarefa",dados);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal async static Task AlterarTarefa(Tarefa model)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(App.BaseUrl)
            };

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "bearer " + MyLocalStorage.GetFromLocalStorage("token"));

            var json = JsonConvert.SerializeObject(model);

            var dados = new StringContent(
                json, Encoding.UTF8, "application/json"
                );

            var response = await client.PutAsync("api/Tarefa/" + model.Id, dados);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
