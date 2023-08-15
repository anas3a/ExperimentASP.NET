using ExperimentASP.NET.Models;
using System.Text.Json;

namespace ExperimentASP.NET.Services
{
    public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        public IEnumerable<Product>? GetProducts()
        {
            using StreamReader jsonFileReader = File.OpenText(JsonFileName);
            return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                               new JsonSerializerOptions
                               {
                                   PropertyNameCaseInsensitive = true
                               });
        }

        public void AddRating(string productId, int rating)
        {
            IEnumerable<Product>? products = GetProducts();

            if (products == null)
            {
                return;
            }

            Product? query = products.FirstOrDefault(x => x.Id == productId);

            if (query == null)
            {
                return;
            }

            if (query.Ratings == null)
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                List<int> ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();
            }

            using var outputStream = File.OpenWrite(JsonFileName);
            JsonSerializer.Serialize<IEnumerable<Product>>(
                               new Utf8JsonWriter(outputStream, new JsonWriterOptions
                               {
                                   SkipValidation = true,
                                   Indented = true
                               }),
                               products);
        }
    }
}
