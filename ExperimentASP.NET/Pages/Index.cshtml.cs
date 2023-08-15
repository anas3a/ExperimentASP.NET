using ExperimentASP.NET.Models;
using ExperimentASP.NET.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExperimentASP.NET.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public JsonFileProductService JsonFileProductService;

        public IEnumerable<Product>? Products { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, JsonFileProductService jsonFileProductService)
        {
            _logger = logger;
            JsonFileProductService = jsonFileProductService;
        }

        public void OnGet()
        {
            Products = JsonFileProductService.GetProducts();
        }
    }
}
