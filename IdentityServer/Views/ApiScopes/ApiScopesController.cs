using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdentityServer.Views.ApiResources
{
    public class ApiScopesController : Controller
    {
        ConfigurationDbContext _ConfigurationDbContext;

        public ApiScopesController(ConfigurationDbContext configurationDbContext)
        {
            _ConfigurationDbContext = configurationDbContext;

        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _ConfigurationDbContext.ApiScopes
                 .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .ToListAsync());
        }

        public async Task<IActionResult> AddEdit(long id)
        {
            return View(await _ConfigurationDbContext.ApiScopes
                 .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .FirstOrDefaultAsync(x=>x.Id==id));
        }


    }

    public class apiscope2 : IdentityServer4.EntityFramework.Entities.ApiScope { }
}
