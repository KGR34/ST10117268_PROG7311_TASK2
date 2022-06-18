using System;
using System.Collections.Generic;

#nullable disable

namespace ST10117268_PROG7311_TASK2.Models
{
    public partial class Product
    {
        public Product()
        {
            UsersProducts = new HashSet<UsersProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public virtual ICollection<UsersProduct> UsersProducts { get; set; }
    }
}
