using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace customer_app
{
    public interface IAuth
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        Task<string> SignUpWithEmailAndPassword(string email, string password);
        bool IsSigIn();
        bool IsSigOut();
        Task ResetPassword(string Email);

    }
}
