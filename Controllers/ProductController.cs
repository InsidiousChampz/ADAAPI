using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using STANDARDAPI.DTOs.Product;
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
        public async Task<IActionResult> GetAllProduct()
        {
            return Ok(await _prodService.GetAllProduct());
        }

        [HttpGet("productId/{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            return Ok(await _prodService.GetProductById(productId));
        }

        [HttpGet("productgroup")]
        public async Task<IActionResult> GetAllProductGroup()
        {
            return Ok(await _prodService.GetAllProductGroup());
        }

        [HttpGet("productgroupId/{productId}")]
        public async Task<IActionResult> GetProductGroupById(int productId)
        {
            return Ok(await _prodService.GetProductGroupById(productId));
        }

        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProduct(AddProductDto newProduct)
        {
            return Ok(await _prodService.AddProduct(newProduct));
        }

        [HttpPost("addproductgroup")]
        public async Task<IActionResult> AddProductGroup(AddProductGroupDto newProductGroup)
        {
            return Ok(await _prodService.AddProductGroup(newProductGroup));
        }

        [HttpPut("updateproduct/{productId}")]
        public async Task<IActionResult> UpdateProductById(int productId, UpdateProductDto updateProduct)
        {
            return Ok(await _prodService.UpdateProductById(productId, updateProduct));
        }

        [HttpPut("updateproductgroup/{productGroupId}")]
        public async Task<IActionResult> UpdateProductGroupById(int productGroupId, UpdateProductGroupDto updateProductGroup)
        {
            return Ok(await _prodService.UpdateProductGroupById(productGroupId, updateProductGroup));
        }

        [HttpDelete("deleteproduct/{productId}")]
        public async Task<IActionResult> DeleteProductById(int ProductId)
        {
            return Ok(await _prodService.DeleteProductById(ProductId));
        }
        [HttpDelete("deleteproductgroup/{productGroupId}")]
        public async Task<IActionResult> DeleteProductGroupById(int productGroupId)
        {
            return Ok(await _prodService.DeleteProductGroupById(productGroupId));
        }


    }
}