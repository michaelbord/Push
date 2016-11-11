namespace WebPush.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Evènement de pushing.
    /// </summary>
    public class PushingItem
    {
        /// <summary>
        /// Obtient l'indentifiant unique du client.
        /// </summary>
        [JsonProperty("customer_id")]
        public int CustomerId { get; internal set; }

        /// <summary>
        /// Obtient la date de l'évènement.
        /// </summary>
        [JsonProperty("date")]
        public DateTime Date { get; internal set; }

        /// <summary>
        /// Obtient l'indentifiant unique du bouton Push.
        /// </summary>
        [JsonProperty("push_button_id")]
        public Guid PushButtonId { get; internal set; }
    }
}