using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using STANDARDAPI.DTOs.Product;
using STANDARDAPI.Models.Product;
using STANDARDAPI.Services.Product;



namespace STANDARDAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _prodService;

        public ProductController(IProductService prodService)
        {
            _prodService = prodService;
        }

        [HttpGet("product")]
        [EnableQuery]
        public async Task<IActionResult> GetAllProduct()
        {
            return Ok(await _prodService.GetAllProduct());
        }

        [HttpGet("productId/{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            return Ok(await _prodService.GetProductById(productId));
        }

        [HttpGet("product/filter")]
        public async Task<IActionResult> GetProductWithFilter([FromQuery] GetProductFilterDto filter)
        {
            return Ok(await _prodService.GetProductWithFilter(filter));
        }

        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProduct(AddProductDto newProduct)
        {
            return Ok(await _prodService.AddProduct(newProduct));
        }

        [HttpPut("updateproduct/{productId}")]
        public async Task<IActionResult> UpdateProductById(int productId, UpdateProductDto updateProduct)
        {
            return Ok(await _prodService.UpdateProductById(productId, updateProduct));
        }

        [HttpDelete("deleteproduct/{productId}")]
        public async Task<IActionResult> DeleteProductById(int ProductId)
        {
            return Ok(await _prodService.DeleteProductById(ProductId));
        }

    }
}