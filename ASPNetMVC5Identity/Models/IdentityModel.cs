using ASPNetMVC5Identity.App_Start;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetMVC5Identity.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
            : base("DefaultConnection")
        {

        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
