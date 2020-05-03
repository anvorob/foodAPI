using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RecipeAPI.Models
{
    public partial class MealType
    {
        public MealType()
        {
            MealTypeToRecipe = new HashSet<MealTypeToRecipe>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<MealTypeToRecipe> MealTypeToRecipe { get; set; }
    }
}
