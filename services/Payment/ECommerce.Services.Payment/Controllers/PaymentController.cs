using ECommerce.Services.Payment.Models;
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Payment.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController : CustomBaseController
{
    [HttpPost]
    public IActionResult ReceivePayment(PaymentDto paymentDto)
    {
        return CreateActionResultInstance(Response<NoContent>.Success(200));
    }
}