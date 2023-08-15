using ExperimentASP.NET.Models;
using ExperimentASP.NET.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExperimentASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        public JsonFileProductService ProductService { get; }

        [HttpGet]
        public IEnumerable<Product>? Get()
        {
            return ProductService.GetProducts();
        }
    }
}
