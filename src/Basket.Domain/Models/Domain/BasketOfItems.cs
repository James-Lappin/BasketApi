using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Basket.Domain.Utilities;
using Newtonsoft.Json;

namespace Basket.Domain.Models.Domain
{
    /// <summary>
    /// A basket. 
    /// </summary>
    public class BasketOfItems
    {
        // used for serialization....
        public BasketOfItems() { }

        /// <summary>
        /// Constructor to create a basket for a given customer
        /// </summary>
        /// <param name="customerId">Id of the customer to create basket for.</param> 
        public BasketOfItems(long customerId)
        {
            // Could get the database to generate the id.
            // However I think it is a bit more DDD to get the app to.
            Id = IdUtilities.GenerateId();
            CustomerId = customerId;
            // Dont know if this should be a list, for now it will do.
            BasketItems = new List<BasketItem>();
        }

        /// <summary>
        /// Id of the BasketItem
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonConverter(typeof(IdToStringConverter))]
        public long Id { get; set; }

        /// <summary>
        /// Id of the customer whose basket this is for
        /// </summary>
        public long CustomerId { get; set; }

        /// <summary>
        /// List of all items in this basket
        /// </summary>
        public List<BasketItem> BasketItems { get; set; }

        /// <summary>
        /// If a product is not in the basket, this adds the item.
        /// If a product is in the basket, this updates the product quantity.
        /// Removes the product from the basket if the quantity specified is 0.
        /// </summary>
        /// <param name="productId">Id of the product to be changed.</param>
        /// <param name="quantity">Quantity to be changed. If 0 this will remove the product from basket</param>
        public void AddUpdateOrRemoveItem(long productId, int quantity)
        {
            if (quantity == 0)
            {
                RemoveItem(productId);
                return;
            }

            var itemInBasket = BasketItems.FirstOrDefault(x => x.ProductId.Equals(productId));
            if (itemInBasket != null)
            {
                // update item in basket
                itemInBasket.UpdateQuantity(quantity);
                return;
            }

            var basketItem = new BasketItem(productId, quantity);
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

        /// <summary>
        /// Removes all products from the basket
        /// </summary>
        public void ClearBasket() => BasketItems = new List<BasketItem>();
    }
}

