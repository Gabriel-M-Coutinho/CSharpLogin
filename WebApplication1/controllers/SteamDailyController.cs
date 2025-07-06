
using Microsoft.AspNetCore.Mvc;

using WebApplication1.models.dailyguess.steam;
using WebApplication1.services.steam;

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



    
 

}