using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INFOEDITORAPI.Services.Product;

namespace INFOEDITORAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductAuditTypeController : ControllerBase
    {
        private readonly IProductService _prodService;

        public ProductAuditTypeController(IProductService prodService)
        {
            _prodService = prodService;
        }

        [HttpGet("productaudittypes")]
        public async Task<IActionResult> GetAllAuditType()
        {
            return Ok(await _prodService.GetAllAuditType());
        }
    }
}