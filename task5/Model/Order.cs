using System;
using System.Collections.Generic;

namespace task5.Model
{
    public partial class Order
    {
        public Order()
        {
            OderDishes = new HashSet<OderDish>();
        }

        public int IdOrder { get; set; }
        public int UserUserid { get; set; }
        public DateTime DateTime { get; set; }
        public int Table { get; set; }
        public string? Details { get; set; }
        public int StatusStatusid { get; set; }

        public string getID => "Номер заказа: " + IdOrder;


        public virtual Status StatusStatus { get; set; } = null!;
        public virtual User UserUser { get; set; } = null!;
        public virtual ICollection<OderDish> OderDishes { get; set; }
    }
}
