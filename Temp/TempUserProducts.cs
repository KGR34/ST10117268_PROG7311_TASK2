using System;
using System.ComponentModel.DataAnnotations;

namespace ST10117268_PROG7311_TASK2.Temp
{
    public class TempUserProducts
    {
        [Key]
        public int UsersProductId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string ProductType { get; set; }
        public DateTime ProductDate { get; set; }
    }
}
