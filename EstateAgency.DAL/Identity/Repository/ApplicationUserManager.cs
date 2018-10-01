
using EstateAgency.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace EstateAgency.DAL.Repositories
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }
    }
}
