using Microsoft.AspNetCore.Mvc;
using WebApplication1.models;
using WebApplication1.services.steam;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SteamGameController : ControllerBase
{
    private readonly SteamGameService _steamService;
    
    public SteamGameController(SteamGameService steamService)
    {
        _steamService = steamService;
    }

    [HttpPost("import-game/{appId}")]
    public async Task<IActionResult> ImportGame(int appId)
    {
        await _steamService.ImportGameAsync(appId);
        return Ok($"Jogo {appId} processado");
    }

    /*[HttpPost("import-all")]
    public async Task<IActionResult> ImportAll([FromQuery] int delayMs = 2000)
    {
        var appIds = await _steamService.GetSteamAppIdsAsync();
        var results = new List<string>();

        foreach (var appId in appIds)
        {
            try
            {
                await _steamService.ImportGameAsync(appId);
                results.Add($"Sucesso: {appId}");
                
                // Aguarda o tempo configurado entre requisições
                await Task.Delay(delayMs);
            }
            catch (Exception ex)
            {
                results.Add($"Erro no appId {appId}: {ex.Message}");
            }
        }

        return Ok(results);
    }*/
/*
    [HttpPost("import-range")]
    public async Task<IActionResult> ImportRange(
        [FromQuery] int start = 0,
        [FromQuery] int count = 100,
        [FromQuery] int delayMs = 2000)
    {
        var appIds = (await _steamService.GetSteamAppIdsAsync())
            .Skip(start)
            .Take(count)
            .ToList();

        var results = new List<string>();

        foreach (var appId in appIds)
        {
            try
            {
                await _steamService.ImportGameAsync(appId);
                results.Add($"Sucesso: {appId}");
                
                await Task.Delay(delayMs);
            }
            catch (Exception ex)
            {
                results.Add($"Erro no appId {appId}: {ex.Message}");
            }
        }

        return Ok(results);
    }*/

}
