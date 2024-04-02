
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
            string genWord = "";
            app.MapGet("/GenerateWord", (HttpContext httpContext) =>
            {
                Random rnd = new Random();
                int num = rnd.Next(0, summaries.Length);
                genWord = summaries[num];
                return genWord;
            })
            .WithName("GetGenerateWord")
            .WithOpenApi();

            app.MapPost("/CheckWord", (string word) =>
            {
                Word checkWord = new Word();
                string result = "";
                if(word != genWord)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        for (int j = 0; j < genWord.Length; j++)
                        {
                            if (word[i] == genWord[j] && i == j)
                            {
                                checkWord.correctPositions.Add(i);
                            }
                            else if (word[i] == genWord[j])
                            {
                                checkWord.correctLetters.Add(word[i]);
                            }
                        }
                    }
                    result = "Correct position = ";
                    for (int i = 0; i < checkWord.correctPositions.Count; i++)
                    {
                        result += checkWord.correctPositions[i] + ", ";
                    }
                    result += "\nCorrect letters = ";
                    for (int i = 0; i < checkWord.correctLetters.Count; i++)
                    {
                        result += checkWord.correctLetters[i] + ", ";
                    }
                }
                else
                {
                    result = "Correct";
                }
                return result;
            })
            .WithName("PostCheckWord")
            .WithOpenApi();

            app.Run();
        }
    }
}
