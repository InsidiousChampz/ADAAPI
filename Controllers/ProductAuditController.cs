using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using STANDARDAPI.DTOs.Product;
using STANDARDAPI.Services.Product;

namespace STANDARDAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductAuditController : ControllerBase
    {
        private readonly IProductService _prodService;

        public ProductAuditController(IProductService prodService)
        {
            _prodService = prodService;
        }

        [HttpGet("productaudit")]
        [EnableQuery]
        public async Task<IActionResult> GetAllProductAudit()
        {
            return Ok(await _prodService.GetAllProductAudit());
        }

        [HttpGet("productAuditId/{productAuditId}")]
        public async Task<IActionResult> GetProductAuditById(int productAuditId)
        {
            return Ok(await _prodService.GetProductAuditById(productAuditId));
        }

        [HttpGet("productaudit/filter")]
        public async Task<IActionResult> GetProductAuditWithFilter([FromQuery] GetProductAuditFilterDto filter)
        {
            return Ok(await _prodService.GetProductAuditWithFilter(filter));
        }

        [HttpPost("addproductaudit")]
        public async Task<IActionResult> AddProductAudit(AddProductAuditDto newAudit)
        {
            return Ok(await _prodService.AddProductAudit(newAudit));
        }

    }
}