using System;
using System.Collections.Generic;

#nullable disable

namespace ST10117268_PROG7311_TASK2.Models
{
    public partial class User
    {
        public User()
        {
            UsersProducts = new HashSet<UsersProduct>();
        }

        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public virtual ICollection<UsersProduct> UsersProducts { get; set; }
    }
}
