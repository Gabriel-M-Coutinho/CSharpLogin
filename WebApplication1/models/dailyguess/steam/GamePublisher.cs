using System.ComponentModel.DataAnnotations;

namespace WebApplication1.models.dailyguess.steam;

public class GamePublisher
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }

    public GamePublisher(string name)
    {
        Name = name;
    }
}