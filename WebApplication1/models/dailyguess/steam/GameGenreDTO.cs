using System.ComponentModel.DataAnnotations;

namespace WebApplication1.models.dailyguess.steam;

public class GameGenreDTO
{
    [Required]
    public string Name { get; set; }
}