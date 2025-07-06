using System.Text.Json;
using MongoDB.Driver;

public class SteamGameService
{
    private readonly IMongoCollection<SteamGame> _games;
    private readonly HttpClient _httpClient;

    public SteamGameService(IMongoDatabase database, HttpClient httpClient)
    {
        _games = database.GetCollection<SteamGame>("steam_games");
        _httpClient = httpClient;
    }

    public async Task ImportGameAsync(int appId)
    {
        try
        {
            var response = await _httpClient.GetStringAsync(
                $"https://store.steampowered.com/api/appdetails?appids={appId}");
            
            using var doc = JsonDocument.Parse(response);
            var root = doc.RootElement;
            var appNode = root.GetProperty(appId.ToString());
            
            if (!appNode.GetProperty("success").GetBoolean())
            {
                Console.WriteLine($"Jogo {appId} não encontrado ou não disponível");
                return;
            }

            var data = appNode.GetProperty("data");
            var game = new SteamGame
            {
                SteamAppId = appId,
                Name = data.GetProperty("name").GetString(),
                Type = data.GetProperty("type").GetString(),
                ShortDescription = data.GetProperty("short_description").GetString(),
                DetailedDescription = data.GetProperty("detailed_description").GetString(),
                HeaderImage = data.GetProperty("header_image").GetString(),
                Website = data.GetProperty("website").GetString() ?? string.Empty,
                IsFree = data.GetProperty("is_free").GetBoolean(),
                Recommendations = data.GetProperty("recommendations").GetProperty("total").GetInt32()
            };

            // Processar data de lançamento
            if (DateTime.TryParse(data.GetProperty("release_date").GetProperty("date").GetString(), 
                out var releaseDate))
            {
                game.ReleaseDate = releaseDate;
            }

            // Processar plataformas
            var platforms = data.GetProperty("platforms");
            game.OnWindows = platforms.GetProperty("windows").GetBoolean();
            game.OnMac = platforms.GetProperty("mac").GetBoolean();
            game.OnLinux = platforms.GetProperty("linux").GetBoolean();

            // Processar preço (com verificação de existência)
            if (data.TryGetProperty("price_overview", out var price))
            {
                game.Price = price.GetProperty("final_formatted").GetString();
                game.PriceValue = price.GetProperty("final").GetDecimal() / 100;
                game.DiscountPercent = price.GetProperty("discount_percent").GetInt32();
            }

            // Processar Metacritic (com verificação de existência)
            if (data.TryGetProperty("metacritic", out var metacritic))
            {
                game.MetacriticScore = metacritic.GetProperty("score").GetInt32();
                game.MetacriticUrl = metacritic.GetProperty("url").GetString();
            }

            // Processar arrays com verificação
            game.Developers = data.TryGetProperty("developers", out var devs) ? 
                devs.EnumerateArray().Select(x => x.GetString()).ToList() : new List<string>();

            game.Publishers = data.TryGetProperty("publishers", out var pubs) ? 
                pubs.EnumerateArray().Select(x => x.GetString()).ToList() : new List<string>();

            game.Genres = data.TryGetProperty("genres", out var genres) ? 
                genres.EnumerateArray().Select(x => x.GetProperty("description").GetString()).ToList() : new List<string>();

            game.Categories = data.TryGetProperty("categories", out var cats) ? 
                cats.EnumerateArray().Select(x => x.GetProperty("description").GetString()).ToList() : new List<string>();

            // Processar screenshots
            game.Screenshots = data.TryGetProperty("screenshots", out var screens) ? 
                screens.EnumerateArray().Select(x => x.GetProperty("path_full").GetString()).ToList() : new List<string>();

            // Processar movies (só thumbnails)
            game.Movies = data.TryGetProperty("movies", out var movies) ? 
                movies.EnumerateArray().Select(x => x.GetProperty("thumbnail").GetString()).ToList() : new List<string>();

            // Processar requisitos com tratamento especial para arrays vazios
            game.PcRequirements = ProcessRequirements(data, "pc_requirements");
            game.MacRequirements = ProcessRequirements(data, "mac_requirements");
            game.LinuxRequirements = ProcessRequirements(data, "linux_requirements");

            await _games.ReplaceOneAsync(
                g => g.SteamAppId == appId,
                game,
                new ReplaceOptions { IsUpsert = true });
                
            Console.WriteLine($"✅ {appId} - {game.Name} importado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro no jogo {appId}: {ex.Message}");
        }
    }

    private string ProcessRequirements(JsonElement data, string fieldName)
    {
        if (!data.TryGetProperty(fieldName, out var requirements))
            return string.Empty;

        // Caso especial para arrays vazios (como no Skyrim para Mac/Linux)
        if (requirements.ValueKind == JsonValueKind.Array)
            return string.Empty;

        // Caso normal com objeto {minimum: "...", recommended: "..."}
        return requirements.TryGetProperty("minimum", out var min) ? 
            min.GetString() : string.Empty;
    }
}