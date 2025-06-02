using System.ComponentModel.DataAnnotations;

namespace WebApplication1.models.dailyguess.steam;

public class GameDeveloperDTO
{
    [Required]
    public string Name { get; set; }
}