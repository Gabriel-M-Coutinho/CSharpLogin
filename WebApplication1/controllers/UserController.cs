using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication1.dtos;
using WebApplication1.models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;

       
        public UserController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }


        [HttpGet("test")]
        public ActionResult<string> TestRoute()
        {
            return Ok("Rota de teste funcionando!");
        }

        // Rota que retorna um usuário específico do banco de dados
        [HttpGet("getuser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            // Busca o usuário pelo ID no banco de dados
            var user = await _userDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(); // Retorna 404 se o usuário não for encontrado
            }

            return Ok(user); // Retorna o usuário se encontrado
        }


        [HttpPost("adduser")]
        public async Task<ActionResult<User>> AddUser([FromBody]UserDTO user)
        {
            User newUser = new User();
            newUser.Email = user.Email;
            newUser.Password = user.Password;
            
            _userDbContext.Users.Add(newUser);
            _userDbContext.SaveChanges();

            // Retorna o usuário recém-criado
            return CreatedAtAction(nameof(GetUser), new { id = newUser.id}, user);
        }
    }
}