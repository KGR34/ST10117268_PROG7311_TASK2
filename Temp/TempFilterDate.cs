using System;
using System.ComponentModel.DataAnnotations;

namespace ST10117268_PROG7311_TASK2.Temp
{
    public class TempFilterDate
    {
        [Key]
        public DateTime StartDate{ get; set; }
        public DateTime EndDate{ get; set; }
    }
}
