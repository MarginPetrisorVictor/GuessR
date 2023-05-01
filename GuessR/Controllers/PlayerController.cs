using GuessR.Data;
using GuessR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using System.IO;
using Microsoft.AspNetCore.Hosting;
using GuessR.Data.Migrations;

namespace GuessR.Controllers
{
	//[Route("Account")]
	public class PlayerController : Controller
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly IConfiguration _configuration;
		private readonly ApplicationDbContext databaseContext;
		private readonly IWebHostEnvironment webHostEnvironment;

		public PlayerController(UserManager<IdentityUser> _userManager,
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

		//[HttpPost]
		//[Route("Player")]
		public async Task<IActionResult> RegisterAdmin([FromBody] Register registerModel)
		{
		//	var userExists = await userManager.FindByEmailAsync(registerModel.Email);
		//	if (userExists == null)
		//		return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

			IdentityUser user = new IdentityUser()
			{
				Email = registerModel.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = registerModel.UserName
			};

			// Create user
			var result = await userManager.CreateAsync(user, registerModel.Password);
			if (!result.Succeeded)
				return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

			// Checking roles in database and creating if not exists
			if (!await roleManager.RoleExistsAsync(ApplicationUserRoles.Admin))
				await roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.Admin));
			if (!await roleManager.RoleExistsAsync(ApplicationUserRoles.User))
				await roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.User));

			// Add role to user
			if (!string.IsNullOrEmpty(registerModel.Role) && registerModel.Role == ApplicationUserRoles.Admin)
			{
				await userManager.AddToRoleAsync(user, ApplicationUserRoles.Admin);
			}
			else
			{
				await userManager.AddToRoleAsync(user, ApplicationUserRoles.User);
			}
			if (ModelState.IsValid)
			{

				var player = new Player()
				{
					UserId = user.Id,
					Name = user.UserName
				};

				await databaseContext.AddAsync(player);
				await databaseContext.SaveChangesAsync();
			}

			return Ok(new Response { Status = "Success", Message = "Player created successfully!" });
		}

		//metoda pentru a face upload la o imagine din calculator.
		
		private string UploadFile(Player player)
		{
			string uniqueFileName = null;
			
			if(player.ProfilePicture != null)
			{
				string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
				uniqueFileName = Guid.NewGuid().ToString() +"_"+player.ProfilePicture.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using(var fileStream = new FileStream(filePath, FileMode.Create))
				{
					player.ProfilePicture.CopyTo(fileStream);
				}
			}
			return uniqueFileName;
		}

		public async Task<IActionResult> Create(int playerID, Player player)
        {
			string uniqueFileName = UploadFile(player);
            var player1 = await databaseContext.Players.FindAsync(playerID);
            player1.ImageUrl = uniqueFileName;

            databaseContext.Players.Update(player1);
            await databaseContext.SaveChangesAsync();

            return RedirectToAction(nameof(Details));
		}


		public IActionResult Details()
		{
			var userName = User.Identity?.Name;
			var player = databaseContext.Players.Where(p => p.Name == userName).Select(p => new Player
			{
				Id = p.Id,
				User = p.User,
				Name = p.Name,
				Score = p.Score,
				SuccessPercentage = p.SuccessPercentage,
				Rank = p.Rank,
				TotalPlayedGames = p.TotalPlayedGames,
				TotalScore = p.TotalScore,
				ImageUrl = p.ImageUrl,
				ProfilePicture = p.ProfilePicture
			}).FirstOrDefault(); //verific Player-ul care are acelasi nume in contul inregistrat pe platforma.


			return View(player); // aici returnez modelul pentru pagina 'Details.cshtml'
		}
	}

}
