using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using WebApplication1.services;
using WebApplication1.services.steam;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔧 Configuração de serviços
            builder.Services.AddControllers();

            // 🔐 Configuração JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
                        )
                    };
                });

            builder.Services.AddAuthorization();

            // 🍃 Configuração do MongoDB
            builder.Services.AddSingleton<IMongoClient>(serviceProvider => 
            {
                var connectionString = builder.Configuration.GetConnectionString("MongoDB") 
                                    ?? "mongodb://localhost:27017";
                return new MongoClient(connectionString);
            });

            // Registro do banco de dados MongoDB
            builder.Services.AddScoped<IMongoDatabase>(serviceProvider =>
            {
                var client = serviceProvider.GetRequiredService<IMongoClient>();
                return client.GetDatabase(builder.Configuration["MongoDB:DatabaseName"] 
                       ?? "steam_games_db");
            });

            // 💼 Registro dos serviços da aplicação
            builder.Services.AddHttpClient(); 
            
            // Serviço de autenticação
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<JwtService>();
            builder.Services.AddScoped<PasswordService>();
            
            // Serviço de jogos Steam
            builder.Services.AddScoped<SteamGameService>();

            // 📚 Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // 🌐 Configuração do pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}