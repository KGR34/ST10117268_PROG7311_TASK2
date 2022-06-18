using Microsoft.AspNetCore.Mvc.Rendering;
using ST10117268_PROG7311_TASK2.Models;
using System.Collections.Generic;
using System.Linq;

namespace ST10117268_PROG7311_TASK2.Temp
{
    public static class PopulateCmp
    {
        public static List<SelectListItem> listFarmerNames = new List<SelectListItem>();
        public static List<SelectListItem> listProductType = new List<SelectListItem>();
        public static List<SelectListItem> listRoles = new List<SelectListItem>()
        {
            new SelectListItem {Text = "Employee", Value = "Employee" },
            new SelectListItem {Text = "Farmer" , Value = "Farmer"},
        };

    }
}
