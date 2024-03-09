
using AmanTaskAPI.Models;
using AmanTaskAPI.Repositories.MailRepository;
using AmanTaskAPI.Repositories.MessageRepository;
using AmanTaskAPI.Repositories.ReceiverRepository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace AmanTaskAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);


            #region SQL
            builder.Services.AddDbContext<AmanTaskContext>(db =>
            db.UseSqlServer(
                builder.Configuration.GetConnectionString("defultConnection")
                )
            );
            #endregion

            #region JSON Serialize
            builder.Services.AddMvc()
                .AddJsonOptions(option =>
                {
                    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            #endregion

            #region CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
            #endregion

            #region DI
            builder.Services.AddScoped<AmanTaskContext>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IMailRepository, MailRepository>();
            builder.Services.AddScoped<IReceiverRepository, ReceiverRepository>();
            #endregion

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            WebApplication? app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseCors("AllowAll");

            app.Run();
        }
    }
}