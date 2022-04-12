using System;
using System.Collections.Generic;
using System.Text;

namespace customer_app.Models
{
    public class Authentication
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NameSalon { get; set; }
        public string Name { get; set; }
        public long Phone { get; set; }
        public string Location { get; set; }
    }
}
