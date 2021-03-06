﻿using System;
using System.Collections.Generic;

namespace RecipeAPI.Models
{
    public partial class Diet
    {
        public Diet()
        {
            DietToRecipe = new HashSet<DietToRecipe>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DietToRecipe> DietToRecipe { get; set; }
    }
}
