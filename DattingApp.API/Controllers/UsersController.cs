using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DattingApp.API.Data;
using System.Threading.Tasks;

namespace DattingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDattingRepository repo;

        public UsersController(IDattingRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await repo.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await repo.GetUser(id);
            return Ok(user);
        }

    }
}