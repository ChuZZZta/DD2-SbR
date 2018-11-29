using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbr
{
    class Map
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("MapName")]
        public string MapName { get; set; }
        [JsonProperty("Lenght")]
        public int Lenght { get; set; }
        [JsonProperty("LapsNumber")]
        public int LapsNumber { get; set; }
        Map(int id,string name,int maplenght,int lapnumber)
        {
            Id = id;
            MapName = name;
            Lenght = maplenght;
            LapsNumber = lapnumber;
        }
       
    }
}
