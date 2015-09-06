using ASPNetMVC5Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetMVC5Identity.App_Start
{
    public class AppUser : IdentityUser
    {
        public string Country { get; set; }
    }

    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store)
            : base(store)
        {
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var userManager = new AppUserManager(new UserStore<AppUser>(context.Get<AppDbContext>()));
            // Configure validation logic for usernames
            userManager.UserValidator = new UserValidator<AppUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                //RequireNonLetterOrDigit = true,
                //RequireDigit = true,
                //RequireLowercase = true,
                //RequireUppercase = true,
            };

            // Configure user lockout defaults
            userManager.UserLockoutEnabledByDefault = true;
            userManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            userManager.MaxFailedAccessAttemptsBeforeLockout = 5;

            userManager.ClaimsIdentityFactory = new AppUserClaimsIdentityFactory();
            return userManager;
        }
       
    }

    public class AppUserClaimsIdentityFactory : ClaimsIdentityFactory<AppUser>
    {

        public override async Task<ClaimsIdentity> CreateAsync(UserManager<AppUser, string> manager, AppUser user, string authenticationType)
        {
            var identity = await base.CreateAsync(manager, user, authenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Country, user.Country));

            return identity;
        }


    }
}
