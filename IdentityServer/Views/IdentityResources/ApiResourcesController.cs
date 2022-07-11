using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdentityServer.Views.ApiResources
{
    public class IdentityResourcesController : Controller
    {
        ConfigurationDbContext _ConfigurationDbContext;

        public IdentityResourcesController(ConfigurationDbContext configurationDbContext)
        {
            _ConfigurationDbContext = configurationDbContext;

        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _ConfigurationDbContext.IdentityResources
                 .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .ToListAsync());
        }
    }
}
