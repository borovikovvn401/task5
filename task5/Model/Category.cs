using System;
using System.Collections.Generic;

namespace task5.Model
{
    public partial class Category
    {
        public Category()
        {
            Dishes = new HashSet<Dish>();
        }

        public int IdCategory { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Dish> Dishes { get; set; }
    }
}
