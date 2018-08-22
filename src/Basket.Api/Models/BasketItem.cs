using System;

namespace Basket.Api.Models
{
    public class BasketItem
    {
        public long Id { get; private set; }
        public long ProductId { get; private set; }
        public int Quantity { get; private set; }

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

            ProductId = productId;
            // should probably make quantity an uint
            Quantity = quantity;
        }

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

