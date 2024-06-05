using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class User : IdentityUser
{
    public User()
    {
        UserBooks = new HashSet<UserBook>();
    }
    public virtual ICollection<UserBook> UserBooks { get; set; }
}
