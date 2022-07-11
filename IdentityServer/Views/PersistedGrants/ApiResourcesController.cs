using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdentityServer.Views.ApiResources
{
    public class PersistedGrantsController : Controller
    {
        PersistedGrantDbContext _ConfigurationDbContext;

        public PersistedGrantsController(PersistedGrantDbContext configurationDbContext)
        {
            _ConfigurationDbContext = configurationDbContext;

        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _ConfigurationDbContext.PersistedGrants
                .ToListAsync());
        }
    }
}
