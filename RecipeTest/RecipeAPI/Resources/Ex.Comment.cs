using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeAPI.Resources
{
    public class ExComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string User { get; set; }
        public int UserID { get; set; }
        public string RecipeTitle { get; set; }
        public int RecipeId { get; set; }
        public int? Rating { get; set; }
    }
}
