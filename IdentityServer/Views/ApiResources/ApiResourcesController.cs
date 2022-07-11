using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdentityServer.Views.ApiResources
{
    public class ApiResourcesController : Controller
    {
        ConfigurationDbContext _ConfigurationDbContext;

        public ApiResourcesController(ConfigurationDbContext configurationDbContext)
        {
            _ConfigurationDbContext = configurationDbContext;

        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _ConfigurationDbContext.ApiResources
                .Include(x => x.Scopes)
               .Include(x => x.Secrets)
                 .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .ToListAsync());
        }
    }
}
