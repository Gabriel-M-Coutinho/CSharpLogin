using MongoDB.Driver;
using WebApplication1.models;

namespace WebApplication1.services.steam;

public class SteamDealyService
{
    private readonly IMongoCollection<SteamGame> _games;
    private readonly IMongoCollection<SteamDailyGuess> _steamDailyGuesses;

    public SteamDealyService(IMongoDatabase database)
    {
        _games = database.GetCollection<SteamGame>("steam_games");
        _steamDailyGuesses = database.GetCollection<SteamDailyGuess>("steam_daily_guesses");
    }
    
    public Dificulty GetDificulty(SteamGame game) => game.Recommendations switch
    {
        <= 1000     => Dificulty.VERY_HARD,
        <= 50000    => Dificulty.HARD,
        <= 100000   => Dificulty.NORMAL,
        <= 500000   => Dificulty.EASY,
        _           => Dificulty.VERY_EASY
    };
    
    
    
    
}