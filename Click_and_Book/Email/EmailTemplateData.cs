using Newtonsoft.Json;
using System;

namespace Click_and_Book.Email
{
    public class EmailTemplateData
    {
        [JsonProperty("action_url")]
        public string ActionUrl { get; set; }
        [JsonProperty("number")]
        public int Number { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("owner")]
        public string Owner { get; set; }
        [JsonProperty("ownerEmail")]
        public string OwnerEmail { get; set; }
        [JsonProperty("apartmantName")]
        public string ApartmentName { get; set; }
        [JsonProperty("reservationPrice")]
        public decimal ReservationPrice { get; set; }
    }
}