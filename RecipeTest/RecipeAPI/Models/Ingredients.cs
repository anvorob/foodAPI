using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RecipeAPI.Models
{
    public partial class Ingredients
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public string Units { get; set; }
        public int? Recipe { get; set; }
        [JsonIgnore]
        public virtual Recipe RecipeNavigation { get; set; }
    }
}
