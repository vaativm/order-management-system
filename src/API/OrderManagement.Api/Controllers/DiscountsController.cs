using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Application.Abstractions;
using OrderManagement.Api.Application.Exceptions;

namespace OrderManagement.Api.Controllers;

[Route("api/discounts")]
[ApiController]
public class DiscountsController : ControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountsController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [HttpGet("/{orderId}")]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderDiscount(Guid orderId)
    {
        try
        {
            decimal discount = await _discountService.CalculateTotalDiscountAsync(orderId);
            return Ok(discount);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

}
