using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseApp.Models
{
    public class User 
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string userName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string email { get; set; }
    }
}
