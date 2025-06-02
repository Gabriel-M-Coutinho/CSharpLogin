using System.ComponentModel.DataAnnotations;

namespace WebApplication1.models.dailyguess.steam;

public class GameDeveloper
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }

    public GameDeveloper(string name)
    {
        Name = name;
    }
}