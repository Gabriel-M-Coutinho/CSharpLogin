using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebApplication1.models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {


        [Authorize]
        [HttpGet("test")]
        public ActionResult<string> TestRoute()
        {
            return Ok("Rota de teste funcionando!");
        }


        [HttpGet("getuser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {

            return Ok(); // Retorna o usuário se encontrado
        }



    }
}