using System;
using Basket.Api.Utilities;
using Newtonsoft.Json;

namespace Basket.Api.Models
{
    /// <summary>
    /// A product item within a basket. 
    /// </summary>
    /// <remarks>
    /// I have made an assumption that there is another service, the product service, that contains all the products being sold on the site.
    /// This is the instance of that product within a basket
    /// </remarks>
    public class BasketItem
    {
        /// <summary>
        /// Id of the BasketItem
        /// </summary>
        [JsonConverter(typeof(IdToStringConverter))]
        public long Id { get; private set; }

        /// <summary>
        /// Id of the product that this basket item represents
        /// </summary>
        public long ProductId { get; private set; }

        /// <summary>
        /// Quantity of the product in the basket
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Constructor to create a basket item
        /// </summary>
        /// <param name="productId">Id of product this basket item represents.</param>
        /// <param name="quantity">Quantity of item to be added. This can be updated via UpdateQuantity</param>
        public BasketItem(long productId, int quantity)
        {
            if (productId <= 0)
            {
                throw new ArgumentException(nameof(productId));
            }

            if (quantity <= 0)
            {
                throw new ArgumentException(nameof(quantity));
            }

            Id = IdUtilities.GenerateId();
            ProductId = productId;
            // should probably make quantity an uint
            Quantity = quantity;
        }


        /// <summary>
        /// Updates the quantity of the product in the basket
        /// </summary>
        /// <param name="newQuantity">The value you want the quantity to be updated too</param>
        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
            {
                throw new ArgumentException(nameof(newQuantity));
            }

            // we may want to check stocks or if it is valid to update to the new value
            Quantity = newQuantity;
        }
    }
}

