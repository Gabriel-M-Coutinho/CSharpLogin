using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.models.dailyguess.steam;

[Table("categories")] 
public class GameCategory
{
    public long Id{get;set;}
    public string Name{get;set;}
    
    GameCategory(){}
    
    public GameCategory(string name)
    {
        Name = name;
    }
    
}