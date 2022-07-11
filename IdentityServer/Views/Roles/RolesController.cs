using IdentityServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdentityServer.Views.Users
{
    public class RolesController : Controller
    {
        ApplicationDbContext _db;

        public RolesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Roles.ToListAsync());
        }
    }
}
