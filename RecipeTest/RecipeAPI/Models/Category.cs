using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RecipeAPI.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryToRecipe = new HashSet<CategoryToRecipe>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<CategoryToRecipe> CategoryToRecipe { get; set; }
    }
}
