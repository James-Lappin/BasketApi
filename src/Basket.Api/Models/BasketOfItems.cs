using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Basket.Api.Utilities;

namespace Basket.Api.Models
{
    public class BasketOfItems
    {
        public BasketOfItems(long customerId)
        {
            // Could get the database to generate the id.
            // However I think it is a bit more DDD to get the app to.
            Id = IdUtilities.GenerateId();
            CustomerId = customerId;
            // Dont know if this should be a list, for now it will do.
            BasketItems = new List<BasketItem>();
        }

        public long Id { get; private set; }
        public long CustomerId { get; private set; }
        public List<BasketItem> BasketItems { get; private set; }

        public void AddUpdateOrRemoveItem(long itemId, int quantity)
        {
            if (quantity == 0)
            {
                RemoveItem(itemId);
                return;
            }

            var itemInBasket = BasketItems.FirstOrDefault(x => x.ProductId.Equals(itemId));
            if (itemInBasket != null)
            {
                // update item in basket
                itemInBasket.UpdateQuantity(quantity);
                return;
            }

            var basketItem = new BasketItem(itemId, quantity);
            BasketItems.Add(basketItem);
        }

        private void RemoveItem(long itemId)
        {
            var itemInBasket = BasketItems.FirstOrDefault(x => x.ProductId.Equals(itemId));
            if (itemInBasket != null)
            {
                BasketItems.Remove(itemInBasket);
            }
        }

        public void ClearBasket() => BasketItems = new List<BasketItem>();
    }
}

