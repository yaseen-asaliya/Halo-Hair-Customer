using System;
using System.Collections.Generic;
using System.Text;

namespace customer_app.Models
{
    public class AuthenticationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CustomerAccessToken { get; set; }
        public string CustomerName { get; set; }
        public long Phone { get; set; }
    }
}
