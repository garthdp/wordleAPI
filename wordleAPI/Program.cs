
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

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var randomWords = new[]
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
                int num = rnd.Next(0, randomWords.Length);
                genWord = randomWords[num];
                return genWord;
            })
            .WithName("GetGenerateWord")
            .WithOpenApi();

            app.MapPost("/CheckWord", (string words) =>
            {
                List<char> correctLetters = new List<char>();
                List<char> correctPosition = new List<char>();
                string word = words.ToLower();
                string gWord = genWord.ToLower();
                string result = "";
                if(word.Length < 5 || word.Length > 5)
                {
                    result = "Error";
                }
                else
                {
                    if (word != gWord)
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            if (word[i] == gWord[i])
                            {
                                correctPosition.Add(word[i]);
                                word.Remove(i);
                            }
                            else if (gWord.Contains(word[i]))
                            {
                                correctLetters.Add(word[i]);
                            }
                        }
                        if (correctPosition.Count != 0 || correctLetters.Count != 0)
                        {
                            result = "Green letters=";
                            for (int i = 0; i < correctPosition.Count; i++)
                            {
                                result += correctPosition[i] + ",";
                            }
                            result += ";";
                            result += "\nYellow letters=";
                            for (int i = 0; i < correctLetters.Count; i++)
                            {
                                result += correctLetters[i] + ",";
                            }
                        }
                        else
                        {
                            result = "Nothing correct";
                        }
                    }
                    else
                    {
                        result = "Correct";
                    }
                }
                return result;
            })
            .WithName("PostCheckWord")
            .WithOpenApi();

            app.Run();
        }
    }
}
