﻿using Basket.Domain.Models.Basket;
using Basket.Domain.Models.Domain;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Basket.Client
{
    public class BasketClient : IBasketClient
    {
        private readonly HttpClient _client;

        private BasketClient(HttpClient client)
        {
            _client = client;
        }

        public static BasketClient Create(string baseAddress)
        {
            // would have to auth here as well
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            return new BasketClient(httpClient);
        }

        public async Task<BasketOfItems> CreateBasket(CreateBasketModel model)
        {
            var payload = JsonConvert.SerializeObject(model);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/v1/basket/", content);
            if (!response.IsSuccessStatusCode)
            {
                // figure this out later
            }

            var value = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BasketOfItems>(value);
        }

        public async Task<BasketOfItems> GetBasket(long basketId)
        {
            var response = await _client.GetAsync($"api/v1/basket/{basketId}");
            if (!response.IsSuccessStatusCode)
            {
                // figure this out later
            }

            return JsonConvert.DeserializeObject<BasketOfItems>(await response.Content.ReadAsStringAsync());
        }

        public async Task<BasketOfItems> AddItemToBasket(UpdateBasketModel model)
        {
            var payload = JsonConvert.SerializeObject(model);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("api/v1/basket", content);
            if (!response.IsSuccessStatusCode)
            {
                // figure this out later
            }

            return JsonConvert.DeserializeObject<BasketOfItems>(await response.Content.ReadAsStringAsync());
        }

        public async Task<BasketOfItems> RemoveItem(long basketId, long productId)
        {
            var model = new UpdateBasketModel
            {
                BasketId = basketId,
                ProductId = productId,
                Quantity = 0
            };
            return await AddItemToBasket(model);
        }

        public async Task<BasketOfItems> ClearBasket(long basketId)
        {
            var response = await _client.PostAsync($"api/v1/basket/clearbasket/{basketId}", null);
            if (!response.IsSuccessStatusCode)
            {
                // figure this out later
            }

            return JsonConvert.DeserializeObject<BasketOfItems>(await response.Content.ReadAsStringAsync());
        }
    }

    public interface IBasketClient
    {
        Task<BasketOfItems> CreateBasket(CreateBasketModel model);
        Task<BasketOfItems> GetBasket(long basketId);
        Task<BasketOfItems> AddItemToBasket(UpdateBasketModel basketId);
        Task<BasketOfItems> RemoveItem(long basketId, long productId);
        Task<BasketOfItems> ClearBasket(long basketId);
    }
}
