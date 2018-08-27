using System.ComponentModel.DataAnnotations;
using Basket.Domain.Utilities;
using Newtonsoft.Json;

namespace Basket.Domain.Models.Basket
{
    public class CreateBasketModel
    {
        /// <summary>
        /// Id of the basket to be updated.
        /// </summary>
        /// <example>1</example>
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        [JsonConverter(typeof(IdToStringConverter))]
        public long CustomerId { get; set; }
    }
}
