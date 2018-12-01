using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbr.Models
{
    public class Map
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("MapName")]
        public string MapName { get; set; }
        [JsonProperty("Lenght")]
        public int Lenght { get; set; }
        [JsonProperty("LapsNumber")]
        public int LapsNumber { get; set; }       
    }
}
