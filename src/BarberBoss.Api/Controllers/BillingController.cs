using BarberBoss.Application.UseCases.Billings.GetAll;
using BarberBoss.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillingController : ControllerBase
{
    [HttpPost]
    public IActionResult Register()
    {
        return Ok("Cadastro");
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseBillingsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllBilling([FromServices] IGetAllBillingUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Billings.Count == 0)
        {
            return NoContent();
        }

        return Ok(response);
    }
}
