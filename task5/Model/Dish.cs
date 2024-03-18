using System;
using System.Collections.Generic;

namespace task5.Model
{
    public partial class Dish
    {
        public Dish()
        {
            OderDishes = new HashSet<OderDish>();
        }

        public int Iddish { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Category { get; set; }
        public string Compound { get; set; } = null!;
        public byte[]? Image { get; set; }

        public string getCompound => "Состав: " + Compound;
        public string getCategory => "Категория: " + CategoryNavigation.Name;
        public string getPrice => "Стоимость: " + Price + " руб.";

        public virtual Category CategoryNavigation { get; set; } = null!;
        public virtual ICollection<OderDish> OderDishes { get; set; }
    }
}
