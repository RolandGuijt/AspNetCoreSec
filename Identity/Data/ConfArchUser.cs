using System;
using Microsoft.AspNetCore.Identity;

namespace AspNetSecurity_m3.Data
{
    public class ConfArchUser : IdentityUser
    {
        public DateTime BirthDate { get; set; }
    }
}
