using System.ComponentModel.DataAnnotations;
using Basket.Domain.Utilities;
using Newtonsoft.Json;

namespace Basket.Domain.Models.Basket
{
    public class UpdateBasketModel
    {
        /// <summary>
        /// Id of the basket to be updated.
        /// </summary>
        /// <example>483639240849096704</example>
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        [JsonConverter(typeof(IdToStringConverter))]
        public long BasketId { get; set; }

        /// <summary>
        /// Id of the product to be added, updated or removed.
        /// </summary>
        /// /// <example>10</example>
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        [JsonConverter(typeof(IdToStringConverter))]
        public long ProductId { get; set; }

        /// <summary>
        /// The quatity of product to be in the basket. A quantity of 0 will remove the item from the basket.
        /// </summary>
        /// <example>3</example>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value equal to or bigger than 0")]
        public int Quantity { get; set; }
    }
}
