using System;
using System.ComponentModel.DataAnnotations;
using WebApplication1.models.dailyguess;
using WebApplication1.models.dailyguess.steam;

namespace WebApplication1.Models
{
    public class SteamGameDTO
    {
        public string Name { get; set; }
        public Dificulty Dificulty { get; set; }

        [Required]
        public List<string> Genres { get; set; } = new();

        [Required]
        public List<string> Categories { get; set; } = new();

        public List<string> PublisherNames { get; set; } = new();
        public List<string> DeveloperNames { get; set; } = new();

        public DateTime ReleaseDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }



}