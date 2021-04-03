using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
namespace BridgeMonitor.Models
{
    public class BridgeClosure
    {

        [JsonProperty("boat_name")]
        public string BoatName { get; set; }

        [JsonProperty("closing_type")]
        public string ClosingType { get; set; }

        [JsonProperty("closing_date")]
        public DateTime ClosingDate { get; set; }

        [JsonProperty("reopening_date")]
        public DateTime ReopeningDate { get; set; }
    }
}