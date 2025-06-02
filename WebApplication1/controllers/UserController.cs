using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

       
        public UserController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpGet("test")]
        public ActionResult<string> TestRoute()
        {
            return Ok("Rota de teste funcionando!");
        }


        [HttpGet("getuser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            // Busca o usuário pelo ID no banco de dados
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(); // Retorna 404 se o usuário não for encontrado
            }

            return Ok(user); // Retorna o usuário se encontrado
        }



    }
}