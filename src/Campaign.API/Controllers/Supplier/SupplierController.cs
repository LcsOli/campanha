using Microsoft.AspNetCore.Mvc;
using Campaign.API.Handlers.Supplier;
using Campaign.API.Commands.Supplier.Get;
using Microsoft.AspNetCore.Authorization;

namespace Campaign.API.Controllers.Supplier
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "supplier")]
        public async Task<IActionResult> GetProductsSold([FromQuery] DateTime initIn,
                                                         [FromQuery] DateTime endIn,
                                                         [FromQuery] int supplierId, 
                                                         [FromServices] IGetSupplierProductsSoldHandler getSupplierProductsSoldHandler)
        {
            var cmd = new GetSupplierProductsSoldCommand(supplierId, initIn, endIn);
            return Ok(await getSupplierProductsSoldHandler.Handle(cmd));
        }
    }
}
