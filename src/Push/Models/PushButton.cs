namespace WebPush.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Bouton Push.
    /// </summary>
    public class PushButton
    {
        /// <summary>
        /// Obtient ou défini l'identifiant unique du bouton Push.
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}