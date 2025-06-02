    using System.ComponentModel.DataAnnotations;
    using WebApplication1.models.dailyguess;
    using WebApplication1.models.dailyguess.steam;

    namespace WebApplication1.models;

    public class SteamGame
    {    
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
        public Dificulty Dificulty { get; set; }
        
        public List<GamePublisher> Publishers { get; set; } = new();
        public List<GameDeveloper> Developer { get; set; } = new();
        
        public int MetacriticScore { get; set; }
        
        public int Recomendations { get; set; }

        public List<GameGenre> Genres { get; set; }= new();

        public List<GameCategory> Categories { get; set; } = new();

        public DateTime ReleaseDate { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public SteamGame()
        {
            CreatedAt = DateTime.Now;
        }

        public SteamGame(string? name, Dificulty dificulty, List<GamePublisher> publishers, List<GameDeveloper> developer, int metacriticScore, int recomendations, List<GameGenre> genres, List<GameCategory> categories, DateTime releaseDate)
        {
            Name = name;
            Dificulty = dificulty;
            Publishers = publishers;
            Developer = developer;
            MetacriticScore = metacriticScore;
            Recomendations = recomendations;
            Genres = genres;
            Categories = categories;
            ReleaseDate = releaseDate;
            CreatedAt = DateTime.Now;
        }

        public void AddPublisher(GamePublisher publisher)
        {
            Publishers.Add(publisher);
        }

        public void AddGenre(GameGenre genre)
        {
            Genres.Add(genre);
        }
        
        public void addDeveloper(GameDeveloper developer)
        {
            Developer.Add(developer);
        }

        public void addCategory(GameCategory category)
        {
            Categories.Add(category);
        }

        public void addGenre(GameGenre genre)
        {
            Genres.Add(genre);
        }
    }