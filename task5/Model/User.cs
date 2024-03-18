using System;
using System.Collections.Generic;

namespace task5.Model
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int IdUsers { get; set; }
        public string Fio { get; set; } = null!;
        public int Role { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual Role RoleNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
