using System;
using System.Collections.Generic;

namespace RecipeAPI.Models
{
    public partial class DietToRecipe
    {
        public int RecipeId { get; set; }
        public int DietId { get; set; }

        public virtual Diet Diet { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
