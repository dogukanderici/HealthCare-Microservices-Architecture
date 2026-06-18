using Core.IdentityServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.IdentityServer.Context
{
    public class IdentityServerDBContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityServerDBContext(DbContextOptions<IdentityServerDBContext> options) : base(options)
        {

        }
    }
}
