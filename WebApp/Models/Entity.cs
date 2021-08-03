using Newtonsoft.Json;
using System;

namespace WebApp.Models
{
    public abstract class Entity
    {
        /// <summary>
        /// Unique identifier for this item.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Data partition in database.
        /// </summary>
        public string Partition { get; set; }
    }
}
