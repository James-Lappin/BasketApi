using System.ComponentModel.DataAnnotations;

namespace Basket.Api.Basket
{
    public class CreateBasketModel
    {
        /// <summary>
        /// Id of the basket to be updated.
        /// </summary>
        /// <example>1</example>
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public long CustomerId { get; set; }
    }
}
