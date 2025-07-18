using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

[BsonIgnoreExtraElements]
public class SteamGame
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int SteamAppId { get; set; }
    
    public string Name { get; set; }
    public string Type { get; set; }
    public string ShortDescription { get; set; }
    public string DetailedDescription { get; set; }
    
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime ReleaseDate { get; set; }
    
    public List<string> Developers { get; set; } = new();
    public List<string> Publishers { get; set; } = new();
    public List<string> Genres { get; set; } = new();
    public List<string> Categories { get; set; } = new();
    
    // Preço simplificado
    public string Price { get; set; } // Formato: "R$ 16,49"
    public decimal PriceValue { get; set; } // Valor numérico para cálculos
    public int DiscountPercent { get; set; }
    
    // Requisitos como strings HTML (ou pode limpar as tags se preferir)
    public string PcRequirements { get; set; }
    public string MacRequirements { get; set; }
    public string LinuxRequirements { get; set; }
    
    // Screenshots - apenas URLs
    public List<string> Screenshots { get; set; } = new();
    
    // Filmes - apenas URLs dos thumbnails
    public List<string> Movies { get; set; } = new();
    
    public int Recommendations { get; set; }
    
    // Metacritic simplificado
    public int? MetacriticScore { get; set; }
    public string MetacriticUrl { get; set; }
    
    public string HeaderImage { get; set; }
    public string Website { get; set; }
    
    // Campos úteis para busca/filtro
    public bool IsFree { get; set; }
    public bool OnWindows { get; set; }
    public bool OnMac { get; set; }
    public bool OnLinux { get; set; }
}