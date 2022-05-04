using System;
using System.Collections.Generic;
using System.Text;

namespace customer_app.Models
{
    public class AuthenticationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccessToken_User { get; set; }
        public string PersonName { get; set; }
        public long Phone { get; set; }
    }
}
