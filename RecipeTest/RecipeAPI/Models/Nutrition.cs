using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RecipeAPI.Models
{
    public partial class Nutrition
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Code { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
        public int? Recipe { get; set; }
        [JsonIgnore]
        public virtual Recipe RecipeNavigation { get; set; }
    }
}
