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
        public async Task<IActionResult> GetProductsSold([FromQuery] int month,
                                                         [FromServices] IGetSupplierProductsSoldHandler getSupplierProductsSoldHandler)
        {
            var cmd = new GetSupplierProductsSoldCommand(month);
            return Ok(await getSupplierProductsSoldHandler.Handle(cmd));
        }
    }
}
