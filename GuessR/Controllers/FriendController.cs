using GuessR.Data;
using GuessR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GuessR.Controllers
{
    public class FriendController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext databaseContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public FriendController(UserManager<IdentityUser> _userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ApplicationDbContext applicationDbContext,
            IWebHostEnvironment webHost)
        {
            this.userManager = _userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            databaseContext = applicationDbContext;
            webHostEnvironment = webHost;
        }

        // GET: Players
        public async Task<IActionResult> Index(string searchString)
        {
            if (databaseContext.Players == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            IQueryable<string> usersQuery = from p in databaseContext.Players
                                            orderby p.Name
                                            select p.Name;

            var friends = from friend in databaseContext.Players
                         select friend;

            if (!String.IsNullOrEmpty(searchString))
            {
                friends = friends.Where(s => s.Name!.Equals(searchString));
            }

            var friendsVM = new FriendsViewModel
            {
                friends = await friends.ToListAsync()
            };

            return View(friendsVM);
        }
    }
}
