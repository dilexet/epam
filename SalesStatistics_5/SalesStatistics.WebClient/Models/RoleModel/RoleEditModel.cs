using System.Collections.Generic;
using SalesStatistics.WebClient.Identity;

namespace SalesStatistics.WebClient.Models.RoleModel
{
    public class RoleEditModel
    {
        public AppRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
}