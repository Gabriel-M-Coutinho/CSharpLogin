using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WebApplication1.models
{
    public class SteamDailyGuess
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 

        [BsonElement("gameId")]
        public int GameId { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("cap")]
        public int Cap { get; set; } // máximo de tentativas

        [BsonElement("scored")]
        public int Scored { get; set; } // tentativas usadas

        [BsonElement("finished")]
        public bool Finished { get; set; }

        [BsonElement("score")]
        public long Score { get; set; }

        [BsonElement("dificulty")]
        public Dificulty Dificulty { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("updatedAt")]
        [BsonIgnoreIfNull]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("deletedAt")]
        [BsonIgnoreIfNull]
        public DateTime? DeletedAt { get; set; }

        public SteamDailyGuess()
        {
            // Construtor vazio necessário
        }
    }
}