using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdentityServer.Views.Home
{
    public class HomeController : Controller
    {
        ConfigurationDbContext _ConfigurationDbContext;

        public HomeController(ConfigurationDbContext configurationDbContext)
        {
            _ConfigurationDbContext = configurationDbContext;
        }

        public async Task<IActionResult> IndexAsync()
        {
           
            return View(await _ConfigurationDbContext.Clients.ToListAsync());
        }
    }
}
