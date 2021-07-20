using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerProFileAPI.DTOs.Product;
using CustomerProFileAPI.Services.Product;

namespace CustomerProFileAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductGroupController : ControllerBase
    {
        private readonly IProductService _prodService;

        //public ProductGroupController(IProductService prodService)
        //{
        //    _prodService = prodService;
        //}

        //[HttpGet("productgroup")]
        //public async Task<IActionResult> GetAllProductGroup()
        //{
        //    return Ok(await _prodService.GetAllProductGroup());
        //}

        //[HttpGet("productgroupId/{productId}")]
        //public async Task<IActionResult> GetProductGroupById(int productId)
        //{
        //    return Ok(await _prodService.GetProductGroupById(productId));
        //}

        //[HttpGet("productgroup/filter")]
        //public async Task<IActionResult> GetProductGroupWithFilter([FromQuery] GetProductGroupFilterDto filter)
        //{
        //    return Ok(await _prodService.GetProductGroupWithFilter(filter));
        //}

        //[HttpPost("addproductgroup")]
        //public async Task<IActionResult> AddProductGroup(AddProductGroupDto newProductGroup)
        //{
        //    return Ok(await _prodService.AddProductGroup(newProductGroup));
        //}

        //[HttpPut("updateproductgroup/{productGroupId}")]
        //public async Task<IActionResult> UpdateProductGroupById(int productGroupId, UpdateProductGroupDto updateProductGroup)
        //{
        //    return Ok(await _prodService.UpdateProductGroupById(productGroupId, updateProductGroup));
        //}

        //[HttpDelete("deleteproductgroup/{productGroupId}")]
        //public async Task<IActionResult> DeleteProductGroupById(int productGroupId)
        //{
        //    return Ok(await _prodService.DeleteProductGroupById(productGroupId));
        //}
    }
}