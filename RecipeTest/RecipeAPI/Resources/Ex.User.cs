using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeAPI.Resources
{
    public class ExUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }
    }
}
