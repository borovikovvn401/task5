using System;
using System.Collections.Generic;

namespace task5.Model
{
    public partial class OderDish
    {
        public int OrderId { get; set; }
        public int DishId { get; set; }
        public int Amount { get; set; }

        public virtual Dish Dish { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
