using System.Text.Json;
using WebApplication1.Data;
using WebApplication1.models;
using WebApplication1.Models;

using WebApplication1.models.dailyguess.steam;

namespace WebApplication1.services.steam;
public class SteamGameService
{
    private readonly AppDbContext _steamDbContext;
    private readonly HttpClient _httpClient;

    
    public SteamGameService(AppDbContext context,HttpClient httpClient)
    {
        _steamDbContext = context;
        _httpClient = httpClient;

    }

    public SteamGame CreateGame(SteamGameDTO gameDto)
    {
        var game = new SteamGame
        {
            Name = gameDto.Name,
            Dificulty = gameDto.Dificulty,
            ReleaseDate = gameDto.ReleaseDate,
            UpdatedAt = gameDto.UpdatedAt,
            DeletedAt = gameDto.DeletedAt,
            CreatedAt = DateTime.Now
        };

        foreach (var genreName in gameDto.Genres)
        {
            var genre = GetOrCreateGameGenre(genreName);
            game.Genres.Add(genre);
        }

        foreach (var categoryName in gameDto.Categories)
        {
            var category = GetOrCreateGameCategory(categoryName);
            game.Categories.Add(category);
        }

        foreach (var publisherName in gameDto.PublisherNames)
        {
            var publisher = GetOrCreateGamePublisher(publisherName);
            game.Publishers.Add(publisher);
        }

        foreach (var developerName in gameDto.DeveloperNames)
        {
            var developer = GetOrCreateGameDeveloper(developerName);
            game.Developer.Add(developer);
        }

        _steamDbContext.SteamGames.Add(game);
        _steamDbContext.SaveChanges();

        return game;
    }

    // 🔽 Métodos com lógica de verificação embutida 🔽

    public async Task<List<int>> GetSteamAppIdsAsync()
    {
        // Obter a resposta da API como string
        var response = await _httpClient.GetStringAsync("https://api.steampowered.com/ISteamApps/GetAppList/v2");

        // Desserializar a resposta JSON para a classe apropriada
        var result = JsonSerializer.Deserialize<SteamAppListResponse>(response);

        // Garantir que a resposta foi desserializada corretamente
        return result?.applist?.apps?.Select(a => a.appid).ToList() ?? new List<int>();
    }
    
    
    
    
    public GameGenre GetOrCreateGameGenre(string name)
    {
        var genre = _steamDbContext.Genres.FirstOrDefault(g => g.Name == name);
        if (genre == null)
        {
            genre = new GameGenre(name);
            _steamDbContext.Genres.Add(genre);
            _steamDbContext.SaveChanges();
        }
        return genre;
    }

    public GameCategory GetOrCreateGameCategory(string name)
    {
        var category = _steamDbContext.Categories.FirstOrDefault(c => c.Name == name);
        if (category == null)
        {
            category = new GameCategory(name);
            _steamDbContext.Categories.Add(category);
            _steamDbContext.SaveChanges();
        }
        return category;
    }

    public GamePublisher GetOrCreateGamePublisher(string name)
    {
        var publisher = _steamDbContext.Publishers.FirstOrDefault(p => p.Name == name);
        if (publisher == null)
        {
            publisher = new GamePublisher(name);
            _steamDbContext.Publishers.Add(publisher);
            _steamDbContext.SaveChanges();
        }
        return publisher;
    }

    public GameDeveloper GetOrCreateGameDeveloper(string name)
    {
        var developer = _steamDbContext.Developers.FirstOrDefault(d => d.Name == name);
        if (developer == null)
        {
            developer = new GameDeveloper(name);
            _steamDbContext.Developers.Add(developer);
            _steamDbContext.SaveChanges();
        }
        return developer;
    }
}



public class SteamAppListResponse
{
    public AppList applist { get; set; }
}

public class AppList
{
    public List<SteamApp> apps { get; set; }
}

public class SteamApp
{
    public int appid { get; set; }
    public string name { get; set; }
}