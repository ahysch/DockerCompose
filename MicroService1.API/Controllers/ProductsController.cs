using MicroService1.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroService1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public ProductsController(AppDbContext context, IConfiguration configuration, HttpClient httpClient)
        {
            _context = context;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        [HttpPost]
        public IActionResult Save()
        {
            _context.Products.Add(new Product { Name = "Lastik", Price = 125.50m });
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = await _httpClient.GetAsync($"{_configuration.GetSection("MicroServices")["MicroService2BaseUrl"]}/api/products ");

            var response = await request.Content.ReadAsStringAsync();

            return Ok(response);
        }
    }
}
