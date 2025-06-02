using System.ComponentModel.DataAnnotations;

namespace WebApplication1.models.dailyguess.steam;

public class GameGenre
{
    [Key]
    public  long Id { get; set; }
    public  string Name { get; set; }

    public GameGenre() {}
    public GameGenre(string name)
    {
        Name = name;
    }
}