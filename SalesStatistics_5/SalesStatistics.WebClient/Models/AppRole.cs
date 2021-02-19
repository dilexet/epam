using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SalesStatistics.WebClient.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole()
        { }

        public AppRole(string name)
            : base(name)
        { }
    }
    
    public class RoleEditModel
    {
        public AppRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}