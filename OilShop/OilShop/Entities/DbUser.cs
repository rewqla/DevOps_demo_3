using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OilShop.Entities
{
    public class DbUser : IdentityUser<long>
    {
        public string FullName { get; set; }
        public virtual ICollection<DbUserRole> UserRoles { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
