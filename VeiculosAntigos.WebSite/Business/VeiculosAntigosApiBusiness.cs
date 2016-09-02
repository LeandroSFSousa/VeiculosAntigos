using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace VeiculosAntigos.WebSite.Business
{
    public class VeiculosAntigosApiBusiness<TEntity> where TEntity : class, new()
    {
        private string _baseAddressApi;
        private string _controllerApi;

        public VeiculosAntigosApiBusiness(string baseAddressApi, string controllerApi)
        {
            _baseAddressApi = baseAddressApi;
            _controllerApi = controllerApi;
        }

        public async Task<IEnumerable<TEntity>> GetAllItems() 
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressApi);
                HttpResponseMessage response = await client.GetAsync("api/" + _controllerApi);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<IEnumerable<TEntity>>();
                    return result;
                }
            }
            return null;
        }
    }
}