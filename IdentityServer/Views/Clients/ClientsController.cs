using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdentityServer.Views.Clients
{
    public class ClientsController : Controller
    {
        ConfigurationDbContext _ConfigurationDbContext;

        public ClientsController(ConfigurationDbContext configurationDbContext)
        {
            _ConfigurationDbContext = configurationDbContext;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var lst = await _ConfigurationDbContext.Clients
                    .Include(x => x.ClientSecrets)
                    .Include(x => x.RedirectUris)
                    .Include(x => x.PostLogoutRedirectUris)
                    .Include(x => x.AllowedScopes)
                    .Include(x => x.IdentityProviderRestrictions)
                    .Include(x => x.Claims)
                    .Include(x => x.AllowedCorsOrigins)
                    .Include(x => x.Properties)
                    .Include(x=>x.AllowedGrantTypes)
                .ToListAsync();
            return View(await _ConfigurationDbContext.Clients.ToListAsync());
        }
    }
}
