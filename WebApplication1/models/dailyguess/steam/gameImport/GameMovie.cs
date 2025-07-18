using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.models.dailyguess.steam;

public class GameMovie
{     
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public Dictionary<string, string> Webm { get; set; }
        public Dictionary<string, string> Mp4 { get; set; }
    
    
}