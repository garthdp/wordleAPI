
namespace wordleAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var summaries = new[]
            {
                "Apple", "Mango", "Beach", "Chair", "Dance", "Earth", "Fairy", "Ghost", "Happy", "Igloo"
                , "Jeans", "Lemon", "Music", "Ocean", "Peach", "Queen"
                , "River", "Smile", "Tiger", "Umbra", "Vital", "Water"
                , "Youth", "Zebra", "Alarm", "Brave", "Cloud", "Dream", "Fruit", "Green"
            };

            app.MapGet("/makeWord", (HttpContext httpContext) =>
            {
                Random rnd = new Random();
                int num = rnd.Next(0, summaries.Length);
                return summaries[num];
            })
            .WithName("MakeWord")
            .WithOpenApi();

            app.MapPost("/checkWord", word =>
            {
                Random rnd = new Random();
                int num = rnd.Next(0, summaries.Length);
                return summaries[num];
            })
            .WithName("CheckWord")
            .WithOpenApi();

            app.Run();
        }
    }
}
