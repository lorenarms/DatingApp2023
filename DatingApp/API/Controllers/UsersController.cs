using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")] // localhost:5001/api/users

	public class UsersController
	{
		private readonly DataContext _context;
		public UsersController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
		{
			var users = await _context.Users.ToListAsync();

			return users;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<AppUser>> GetUser(int id)
		{
			var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

			if (user == null)
			{
				return new BadRequestResult();
			}

			return user;
		}
	}
}
