
using Microsoft.AspNetCore.Mvc;


namespace WebApplication1.Controllers;
[ApiController] 
[Route("api/[controller]")]
public class SteamDailyController: ControllerBase
{
    private readonly SteamGameService _steamGameService;

    public SteamDailyController(SteamGameService steamGameService)
    {
        _steamGameService = steamGameService;
    }

    
    /*
     
    */
    
 

}