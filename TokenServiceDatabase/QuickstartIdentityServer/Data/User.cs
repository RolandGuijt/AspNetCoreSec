using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuickstartIdentityServer.Data
{
    public class User
    {
        public int Id { get; set; }
        public Guid SubjectId { get; set; }
        [StringLength(50)]
        [Required]
        public string UserName { get; set; }
        [StringLength(50)]
        [Required]
        public string Password { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Website { get; set; }
        public bool IsActive { get; set; }
        public List<UserExternalProvider> UserExternalProviders { get; set; }
    }
}
